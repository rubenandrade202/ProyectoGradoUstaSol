using ProyectoGradoUstaCommon;
using ProyectoGradoUstaUtility;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class VentaBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx ctxDomain;
        #endregion

        #region [CONSTRUCTOR]
        public VentaBl()
        {
            ctxDomain = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        #endregion

        #region [ADD]
        /// <summary>
        /// Metodo principal para registrar una venta en el sistema.
        /// Funciona como entrada una unica entidad
        /// Retorna un objeto estandar de ResponseBasicVm
        /// Funciona con una transacción, en caso de haber una excepción el proceso no se realiza parcialmente 
        /// </summary>
        /// <param name="candidate">View Model VentaAddVm con estructura de propiedades que representan lo siguiente
        /// (short) IdCliente => Id del cliente al que se le registra la venta (Nullable)
        /// (int) Abono => Abono parcial o total del total de la venta para cuando es un credito, la venta total se trata como credito o no credito y no por linea individual (nullable)
        /// (bool) EsCredito => flag que se coloca en true cuando es una venta que tiene un credito economico parcial o total.
        /// List<BasicDependantIntVm> Venta => colección de items vendidos representados en la entidad BasicDependantIntVm que cada propiedad representa:
        /// Id => IdProducto representando el id del item vendido
        /// IdParent => Cantidad de items de la linea vendida
        /// Value => total de la linea vendida        
        /// Adicional los siguientes ID no registrados en BD en la tabla de productos representan ventas genericas y se guardan con el id correspondiente
        /// en la tabla ventas y son:
        /// 150000 venta generica 50
        /// 150001 venta generica 100
        /// 150002 venta generica 200
        /// 150003 venta generica 500  
        /// </param>
        /// <param name="idVendedor">El id del vendedor que procesa la venta</param>
        /// <returns></returns>
        public ResponseBasicVm Add(VentaAddVm candidate, int idVendedor)
        {
		    var rp = new ResponseBasicVm();
            using (var dbContextTransaction = ctxDomain.Database.BeginTransaction())
            {
			    try
			    {                                     
                    var lstRecordVenta = new List<VentasProyectoUsta>();//colección destinada a guardar records de venta en DB de ventas                    								
                    //proyección a las entidades correspondientes Entity	
				    candidate.Venta.ForEach(x =>
                    {
                        lstRecordVenta.Add(new VentasProyectoUsta()
                        {
                            IdProducto = x.Id,
                            Cantidad = (short)x.IdParent,
                            FechaRegistro = DateTime.Now,
                            Total = x.Value,
                            IdCliente = candidate.IdCliente,
                            IdVendedor = idVendedor
                        });
                    });
										
                    //Registros reales a procesar
                    if (lstRecordVenta.Count > 0)
                    {
                        //Se obtienen unicamente los id de los productos a ingresar
                        var idsCurrentVentaProducts = lstRecordVenta.Select(y => y.IdProducto).ToList();
                        var totalVentaRegistrada = lstRecordVenta.Sum(x => x.Total);
                        var totalIngresoDineroCaja = candidate.EsCredito ? candidate.Abono : totalVentaRegistrada;
                        ClientesProyectoUsta clienteCredito = null;                      

                        //Se agregan los registros reales proyectados a la tabla ventas
                        ctxDomain.VentasProyectoUsta.AddRange(lstRecordVenta);
                        rp.MessageOk.Add("Se ingresaron los registros vendidos");

	                    //Se remueven las unidades correspondientes vendidas por cada item del inventario                        
                        var currentProductsInventario = ctxDomain.ProductosProyectoUsta.Where(x => idsCurrentVentaProducts.Contains(x.Id)).ToList();
                        currentProductsInventario.ForEach(x =>
                        {
                            x.CantidadActual -= lstRecordVenta.Where(y => y.IdProducto == x.Id).FirstOrDefault().Cantidad;
                        });
                                                                                                
                        //Se ingresan los datos relacionados con credito del cliente alterando los valores a deber y su log respectivo
                        if ((totalVentaRegistrada != totalIngresoDineroCaja) && candidate.IdCliente > 0 && candidate.EsCredito)
                        {                                                                                                              
                            var newLogCredito = new LogCreditosProyectoUsta();
                            newLogCredito.FechaRegistro = DateTime.Now;
                            newLogCredito.IdCliente = candidate.IdCliente;
                            newLogCredito.IdUsuario = idVendedor;
                            newLogCredito.Valor = totalVentaRegistrada - candidate.Abono;
                            ctxDomain.LogCreditosProyectoUsta.Add(newLogCredito);

                            clienteCredito = ctxDomain.ClientesProyectoUsta.Where(x => x.Id == candidate.IdCliente).FirstOrDefault();
                            if (clienteCredito != null)
                            {
                                clienteCredito.CupoEmpleado += (totalVentaRegistrada - candidate.Abono);                                                          
                                rp.MessageOk.Add("Saldo de credito asignado al cliente!");
                            }
                            else
                            {
                                rp.Success = false;
                                rp.MessageOk.Clear();
                                rp.MessageBad.Add("El cliente no se encuentra registrado en el sistema. El CREDITO NO SE CARGARA, COBRE EN EFECTIVO!!. COMUNIQUELO AL ADMINISTRADOR.");
                                dbContextTransaction.Rollback();                                                                                               
                            }
                        }      
                        
                        if(totalIngresoDineroCaja > 0)
                        {
                            var newMovimientoCaja = new MovimientosCajaProyectoUsta()
                            {
                                IdTipoMovimiento = 1,//"INGRESO VENTA"
                                Valor = totalIngresoDineroCaja,
                                FechaRegsitro = DateTime.Now
                            };
                            ctxDomain.MovimientosCajaProyectoUsta.Add(newMovimientoCaja);
                            rp.MessageOk.Add("Se ha afectado el efectivo de la caja!");
                        }      
                                                                                                                                                    
                        ctxDomain.SaveChanges();
                        dbContextTransaction.Commit();
                        rp.Success = true;
                        Logging.NotifyMovementGeneral();
                        if(clienteCredito != null)
                        {
                            Logging.NotifyMovementCreditClient((totalVentaRegistrada - candidate.Abono), clienteCredito.CupoEmpleado, clienteCredito.Email, clienteCredito.Nombre);
                        }     
                        if(candidate.ToPrint)
                        {

                        }
                    }
                    else
                    {
                        //Log de el mal comportamiento evidenciado ACA **TODO
                        rp.Success = false;
                        rp.MessageOk.Clear();
                        rp.MessageBad.Add("No hay registros que ingresar!");
                    }								    
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    rp.Success = false;
                    rp.MessageOk.Clear();
                    rp.MessageBad.Add(e.ToString());
                }
            }
            return rp;
        }
        #endregion

        #region [UPDATE]
        #endregion

        #region [DELETE]
        #endregion

        #region [CHECK]
        #endregion
    }
}
