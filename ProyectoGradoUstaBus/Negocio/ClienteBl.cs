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
    public sealed class ClienteBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion

        #region [CONSTRUCTOR]
        public ClienteBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        public IQueryable<ClientesProyectoUsta> Get()
        {
            return domainCtx.ClientesProyectoUsta.OrderBy(x => x.Nombre).AsQueryable();
        }
        #endregion

        #region [ADD]
        public ResponseBasicVm Add(List<ClientesProyectoUsta> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //var lstCandidatesEntity = new List<ProductosProyectoUsta>();
                var currentRecords = domainCtx.ClientesProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).ToList();
                var ownExcludingCandidates = new List<BasicVm>();
                var entityExcludingCandidates = new List<BasicVm>();
                var flagInsert = false;

                //Capitalize nombres and positive numbers
                candidates = candidates.Select(x => new ClientesProyectoUsta()
                {
                    Nombre = x.Nombre.Trim().ToUpper(), CupoAsignado = x.CupoAsignado < 0 ? x.CupoAsignado*(-1): x.CupoAsignado,
                    CupoEmpleado= x.CupoEmpleado, FechaRegistro = x.FechaRegistro,
                    IdUsuarioRegistro = x.IdUsuarioRegistro, Email = x.Email,
                    Telefono = x.Telefono
                }).ToList();

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new { x.Nombre }).Where(x => x.Count() > 1)
                                            .Select(x => new BasicVm() { Value = x.Key.Nombre }).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => y.Value == x.Nombre));

                // Records excluidos por preexistencia en BD                
                entityExcludingCandidates = candidates.Where(x => currentRecords.Exists(y => x.Nombre == y.Value)).Select(z => new BasicVm() { Id = 0, Value = z.Nombre }).ToList();

                //limpia candidatos por inconsistencia preexistencia en DB
                candidates.RemoveAll(x => entityExcludingCandidates.Exists(y => x.Nombre == y.Value));

                candidates.ForEach(x =>
                {
                    flagInsert = true;
                    var nuevoCliente = new ClientesProyectoUsta();
                    nuevoCliente.Telefono = x.Telefono;
                    nuevoCliente.Email = x.Email;
                    nuevoCliente.CupoAsignado = x.CupoAsignado;
                    nuevoCliente.FechaRegistro = DateTime.Now;
                    nuevoCliente.CupoEmpleado = 0;
                    nuevoCliente.IdUsuarioRegistro = idUser;
                    nuevoCliente.Nombre = x.Nombre;

                    domainCtx.ClientesProyectoUsta.Add(nuevoCliente);
                    domainCtx.SaveChanges();

                    rp.Success = true;
                    rp.MessageOk.Add(x.Nombre + " ingresado.");                    
                });

                if (!flagInsert)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ingresaron registros");
                }

                if (ownExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    ownExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido por que se enviaron dos registros similares"));
                }

                if (entityExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    entityExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido porque existe un producto con el mismo nombre en BD. INTENTE INGRESANDO EL CODIGO DE BARRAS DEL PRODUCTO EN EL CAMPO CODIGO DE BARRAS"));
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

        #region [UPDATE]
        public ResponseBasicVm Update(ClientesProyectoUsta candidate, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //capitalize name and validation positive
                candidate.Nombre = candidate.Nombre.ToUpper();
                candidate.CupoAsignado = candidate.CupoAsignado < 0 ? candidate.CupoAsignado * -1 : candidate.CupoAsignado;
                var actualRecords = domainCtx.ClientesProyectoUsta.ToList();
                var currentRecord = actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault();
                if (currentRecord != null)
                {
                    if ((currentRecord.Nombre == candidate.Nombre) ||
                        (actualRecords.Where(x => x.Nombre == candidate.Nombre).FirstOrDefault() == null) ||
                        (actualRecords.Where(x => x.Nombre == candidate.Nombre).FirstOrDefault().Id == candidate.Id))
                    {
                        
                        currentRecord.CupoAsignado = candidate.CupoAsignado;
                        //currentRecord.CupoEmpleado = candidate.CupoEmpleado;
                        currentRecord.FechaModificado = DateTime.Now;
                        currentRecord.Email = candidate.Email;
                        currentRecord.Telefono = candidate.Telefono;
                        currentRecord.IdUsuarioModificado = idUser;
                        currentRecord.Nombre = candidate.Nombre;                        
                                                                     
                        domainCtx.SaveChanges();
                        rp.Success = true;
                        rp.MessageOk.Add("Registro actualizado.");
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("El nombre ya existe en DB");
                    }
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("El ID de elemento no existe en DB");
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public ResponseBasicVm UpdateCuentaCliente(int idCliente, int valorConsignado, int idUsuarioSistema)
        {
            var rp = new ResponseBasicVm();
            using (var dbTrans = domainCtx.Database.BeginTransaction())
            {
                try
                {
                    var record = domainCtx.ClientesProyectoUsta.Where(x => x.Id == idCliente).FirstOrDefault();
                    if (record != null)
                    {
                        //Si el valor es igual al que se debe se borra el log de creditos del cliente
                        if(valorConsignado == record.CupoEmpleado)
                        {
                            var currentLogRecords = domainCtx.LogCreditosProyectoUsta.Where(x => x.IdCliente == idCliente).ToList();
                            domainCtx.LogCreditosProyectoUsta.RemoveRange(currentLogRecords);
                        }
                                  
                        //Se actualiza la deuda del cliente              
                        record.CupoEmpleado -= valorConsignado;
                        record.IdUsuarioModificado = idUsuarioSistema;
                        record.FechaModificado = DateTime.Now;                                                
                        
                        var newMovimientoCaja = new MovimientosCajaProyectoUsta();
                        newMovimientoCaja.FechaRegsitro = DateTime.Now;
                        newMovimientoCaja.IdTipoMovimiento = 2;//Abono de credito
                        newMovimientoCaja.Valor = valorConsignado;
                        domainCtx.MovimientosCajaProyectoUsta.Add(newMovimientoCaja);                        

                        domainCtx.SaveChanges();
                        rp.Success = true;
                        rp.MessageOk.Add("Saldo afectado exitosamente.");
                        Logging.NotifyMovementPayClient(valorConsignado, record.CupoEmpleado, record.Email, record.Nombre);
                        Logging.NotifyMovementGeneral();                                      
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("El cliente no existe en BD");
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    rp.MessageOk.Clear();
                    rp.Success = false;
                    rp.MessageBad.Add(e.ToString());
                }
            }                          
            return rp;            
        }
        #endregion

        #region [DELETE]
        public ResponseBasicVm Delete(List<int> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstEntitiesDependant = new List<ClientesProyectoUsta>();
                var flagEliminacion = false;

                //chequeo existencias en DB
                var actualRecords = domainCtx.ClientesProyectoUsta.Where(x => candidates.Contains(x.Id)).ToList();
                if (actualRecords.Count > 0)
                {
                    //limpieza de posibles dependencias                

                    //Se eliminan los registros que no tienen ningun registro en el sistema
                    if (actualRecords.Count > 0)
                    {
                        flagEliminacion = true;
                        var actualIdsRecordsToDelete = actualRecords.Select(y => y.Id).ToList();                        
                        domainCtx.ClientesProyectoUsta.RemoveRange(actualRecords);                       
                        domainCtx.SaveChanges();
                        actualRecords.ForEach(x => rp.MessageOk.Add(x.Nombre + " eliminado."));
                    }
                    
                    if (flagEliminacion)
                    {
                        rp.Success = true;
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("No se han ingresado registros!");
                    }

                }
                else
                {
                    rp.Success = false;
                    candidates.ForEach(x => rp.MessageBad.Add(x.ToString() + " id no se encuentra registrado en DB"));
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

        #region [CHECK]
        public ResponseBasicVm CheckNameAvailability(string name)
        {
            var rp = new ResponseBasicVm();
            try
            {
                name = name.ToUpper();
                var record = domainCtx.ClientesProyectoUsta.Where(x => x.Nombre == name).FirstOrDefault();
                if(record == null)
                {
                    rp.Success = true;
                    rp.MessageOk.Add("El nombre esta disponible");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("El nombre se encuentra en uso");
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }

            return rp;
        }
        /*
         public ResponseBasicVm CheckCodeBar(string codeBar)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var record = (from productos in domainCtx.ProductosProyectoUsta
                              join codigoBarraProducto in domainCtx.ProductoCodigoDeBarrasProyectoUsta on productos.Id equals codigoBarraProducto.IdProducto                             
                              where codigoBarraProducto.CodigoDeBarras == codeBar
                              select new ProductoUpdateVm
                              {
                                  Activo = productos.Activo,
                                  Id = productos.Id,
                                  NewCantidadActual = productos.CantidadActual,
                                  NewCantidadUmbral = productos.CantidadUmbral,
                                  NewIdUbicacionNegocio = productos.IdUbicacionNegocio,
                                  NewIdUbicacionStock = productos.IdUbicacionStock,
                                  NewNombre = productos.Nombre,
                                  NewPrecio = productos.Precio,
                                  NewCodigosBarras = (from productoCodeBar in domainCtx.ProductoCodigoDeBarrasProyectoUsta
                                                      where productoCodeBar.IdProducto == productos.Id
                                                      select productoCodeBar.CodigoDeBarras).ToList(),                                  
                                  NewProveedores = (from proveedores in domainCtx.ProveedoresProyectoUsta
                                                    join proveedoresProducto in domainCtx.ProductoProveedorProyectoUsta on proveedores.Id equals proveedoresProducto.IdProveedor
                                                    where proveedoresProducto.IdProducto == productos.Id
                                                    select new BasicDependantDecimalVm
                                                    {
                                                        Id = proveedores.Id,
                                                        IdParent = proveedoresProducto.PrecioCompra,
                                                        Value = proveedores.Nombre
                                                    }).ToList()
                              }).FirstOrDefault();

                if (record != null)
                {
                    if(record.Activo)
                    {
                        rp.Success = true;
                        rp.MessageOk.Add("El codigo existe en un producto activo");
                    }
                    else
                    {
                        rp.Success = true;
                        rp.Alert = true;
                        rp.MessageAlert.Add("El codigo de barras esta asociado a un producto inactivo, desea activarlo y editarlo?");
                        rp.Data = record;
                    }
                }   
                else
                {
                    rp.MessageBad.Add("El codigo no existe en BD");
                }                                                                                
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;            
        }
         */
        #endregion
    }
}
