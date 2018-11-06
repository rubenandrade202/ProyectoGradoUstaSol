using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus.Negocio
{
    public sealed class CajaBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion

        #region [CONSTRUCTOR]
        public CajaBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        public IQueryable<GmCajaVm> Get()
        {
            var dateReferenceLow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var dateReferenceHigh = dateReferenceLow.AddDays(1);
           
            return (from caja in domainCtx.MovimientosCajaProyectoUsta
                    join tipoMovimiento in domainCtx.TiposMovimientoCajaProyectoUsta on caja.IdTipoMovimiento equals tipoMovimiento.Id
                    where caja.FechaRegsitro > dateReferenceLow && 
                    caja.FechaRegsitro < dateReferenceHigh
                    group caja by new {caja.IdTipoMovimiento, tipoMovimiento.Nombre} into agrupacion
                    select new GmCajaVm
                    {
                        //Id = agrupacion.Key. caja.Id,
                        IdTipoMovimiento = agrupacion.Key.IdTipoMovimiento,//caja.IdTipoMovimiento,
                        Valor = agrupacion.Sum(x => x.Valor),  //caja.Valor,
                        //FechaRegistro = caja.FechaRegsitro,
                        NombreTipoMovimiento = agrupacion.Key.Nombre
                    }).OrderBy(x => x.NombreTipoMovimiento).AsQueryable(); 
        }

        #endregion

        #region [ADD]
        /// <summary>
        /// BasicVm structure
        /// Id = Id del movimiento
        /// Value = Valor del movimiento
        ///Id Nombre
        ///1=INGRESO VENTA
        ///2=INGRESO ABONO CREDITO
        ///3=INGRESO INVERSIÓN
        ///4=EGRESO PAGOS SERVICIOS-ARRIENDOS
        ///5=EGRESO CONSIGNACIÓN
        ///6=EGRESO URGENCIA
        ///7=EGRESO PAGO EMPLEADOS
        ///8=EGRESO PAGO PEDIDOS
        ///9=INGRESO INICIO CAJA
        ///10=EGRESO ENTREGA CAJA
        ///11=EGRESO DEVOLUCIÓN
        /// </summary>
        /// <param name="candidateRecord"></param>
        /// <returns></returns>
        public ResponseBasicVm RegistrarMovimiento(BasicIntVm candidateRecord)
        {
            var rp = new ResponseBasicVm();
            try
            {
                switch(candidateRecord.Id)
                {
                    case 1:
                        candidateRecord.Value = candidateRecord.Value > 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 2:
                        candidateRecord.Value = candidateRecord.Value > 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 3:
                        candidateRecord.Value = candidateRecord.Value > 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 4:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 5:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 6:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 7:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 8:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 9:
                        candidateRecord.Value = candidateRecord.Value > 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 10:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                    case 11:
                        candidateRecord.Value = candidateRecord.Value < 0 ? candidateRecord.Value : candidateRecord.Value * -1;
                        break;
                }

                domainCtx.MovimientosCajaProyectoUsta.Add(new MovimientosCajaProyectoUsta()
                {
                    IdTipoMovimiento = (short)candidateRecord.Id,
                    Valor = candidateRecord.Value,
                    FechaRegsitro = DateTime.Now                   
                });
                domainCtx.SaveChanges();
                rp.MessageOk.Add("Se ha afectado el efectivo de la caja");
                rp.Success = true;               
            }
            catch (Exception e)
            {
                rp.MessageOk.Clear();
                rp.MessageBad.Add(e.ToString());
                rp.Success = false;
            }
            return rp;
        }

        public ResponseBasicVm RegistrarDevolucion(int idProducto)
        {
            var rp = new ResponseBasicVm();
            var flagProceso = true;
            try
            {
                var producto = domainCtx.ProductosProyectoUsta.Where(x => x.Id == idProducto).FirstOrDefault();
                if(producto != null)
                {
                    //actualiza cantidad en inventario
                    producto.CantidadActual++;

                    //borra la cantidad de la ultima venta
                    var venta = domainCtx.VentasProyectoUsta.Where(x => x.IdProducto == producto.Id).OrderByDescending(x => x.FechaRegistro).FirstOrDefault();
                    if(venta != null)
                    {
                       if(venta.Cantidad > 1)
                        {
                            venta.Cantidad--;
                        }
                        else
                        {
                            domainCtx.VentasProyectoUsta.Remove(venta);
                        }
                    }   
                    else
                    {
                        flagProceso = false;
                        rp.Success = false;
                        rp.MessageBad.Add("No se encuentran ventas registradas para el producto devuelto");
                    }

                    //saca el dinero de la caja
                    domainCtx.MovimientosCajaProyectoUsta.Add(new MovimientosCajaProyectoUsta()
                    {
                        FechaRegsitro = DateTime.Now,
                        IdTipoMovimiento = 11,
                        Valor = producto.Precio * -1
                    });                    

                    //notifica de la devolución por correo TODO

                    if(flagProceso)
                    {
                        domainCtx.SaveChanges();
                        rp.Success = true;
                        rp.MessageOk.Add("Se ha registrado la devolución");
                    }                    
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("El id de producto enviado al servidor no corresponde con ninguno en BD. Contacte al administrador.");
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());                
            }
            return rp;
        }
        #endregion
    }
}
