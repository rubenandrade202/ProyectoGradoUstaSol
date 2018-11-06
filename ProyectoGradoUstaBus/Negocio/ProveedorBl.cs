using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class ProveedorBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion
        
        #region [CONSTRUCTOR]
        public ProveedorBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion
        
        #region [GET]
        public IQueryable<ProveedoresProyectoUsta> Get()
        {            
            return domainCtx.ProveedoresProyectoUsta.Where(x => x.IdEstado == true).OrderBy(x => x.Nombre).AsQueryable();                        
        }
        #endregion

        #region [ADD]
        public ResponseBasicVm Add(List<ProveedorAddVm> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstCandidatesEntity = new List<ProveedoresProyectoUsta>();
                List<BasicVm> currentRecords = domainCtx.ProveedoresProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).ToList();
                var ownExcludingCandidates = new List<string>();
                var entityExcludingCandidates = new List<string>();

                //Capitalize nombres
                candidates = candidates.Select(x => new ProveedorAddVm() 
                { 
                    Nombre = x.Nombre.Trim().ToUpper(), Descripcion = x.Descripcion, Nit = x.Nit, Telefono = x.Telefono,
                    Visita_Lunes = x.Visita_Lunes, Visita_Martes = x.Visita_Martes, Visita_Miercoles = x.Visita_Miercoles,
                    Visita_Jueves = x.Visita_Jueves, Visita_Viernes = x.Visita_Viernes, Visita_Sabado = x.Visita_Sabado,
                    Visita_Domingo = x.Visita_Domingo, Entrega_Lunes = x.Entrega_Lunes, Entrega_Martes = x.Entrega_Martes,
                    Entrega_Miercoles = x.Entrega_Miercoles, Entrega_Jueves = x.Entrega_Jueves, Entrega_Viernes = x.Entrega_Viernes,
                    Entrega_Sabado = x.Entrega_Sabado, Entrega_Domingo = x.Entrega_Domingo
                }).ToList();  

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new {x.Nombre}).Where(x => x.Count() > 1).Select(x => x.Key.Nombre).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => x.Nombre == y));

                // Records excluidos por preexistencia en BD                
                entityExcludingCandidates = candidates.Where(x => currentRecords.Exists(y => x.Nombre == y.Value)).Select(z => z.Nombre).ToList();

                //limpia candidatos por inconsistencia preexistencia en DB
                candidates.RemoveAll(x => entityExcludingCandidates.Exists(y => x.Nombre == y));

                candidates.ForEach(x =>
                {
                    var nuevoProveedor = new ProveedoresProyectoUsta();
                    nuevoProveedor.Descripcion = x.Descripcion;
                    nuevoProveedor.FechaRegistro = DateTime.Now;
                    nuevoProveedor.IdEstado = true;
                    nuevoProveedor.Nit = x.Nit;
                    nuevoProveedor.IdUsuarioCreador = idUser;
                    nuevoProveedor.Nombre = x.Nombre;
                    nuevoProveedor.Telefono = x.Telefono;
                    nuevoProveedor.Visita_Lunes = x.Visita_Lunes;
                    nuevoProveedor.Visita_Martes = x.Visita_Martes;
                    nuevoProveedor.Visita_Miercoles = x.Visita_Miercoles;
                    nuevoProveedor.Visita_Jueves = x.Visita_Jueves;
                    nuevoProveedor.Visita_Viernes = x.Visita_Viernes;
                    nuevoProveedor.Visita_Sabado = x.Visita_Sabado;
                    nuevoProveedor.Visita_Domingo = x.Visita_Domingo;
                    nuevoProveedor.Entrega_Lunes = x.Entrega_Lunes;
                    nuevoProveedor.Entrega_Martes = x.Entrega_Martes;
                    nuevoProveedor.Entrega_Miercoles = x.Entrega_Miercoles;
                    nuevoProveedor.Entrega_Jueves = x.Entrega_Jueves;
                    nuevoProveedor.Entrega_Viernes = x.Entrega_Viernes;
                    nuevoProveedor.Entrega_Sabado = x.Entrega_Sabado;
                    nuevoProveedor.Entrega_Domingo = x.Entrega_Domingo;
                    lstCandidatesEntity.Add(nuevoProveedor);
                });

                if (lstCandidatesEntity.Count > 0)
                {
                    domainCtx.ProveedoresProyectoUsta.AddRange(lstCandidatesEntity);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    lstCandidatesEntity.ForEach(x => rp.MessageOk.Add(x.Nombre + " ingresado."));
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ingresaron registros");
                }

                if (ownExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    ownExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x + " excluido por que se enviaron dos registros similares"));
                }

                if (entityExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    entityExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x + " excluido porque existe un registro con el mismo nombre en la ubicacion seleccionada"));
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
        public ResponseBasicVm Update(List<ProveedorUpdateVm> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstDuplicateName = new List<string>();
                var ownExcludingCandidates = new List<string>();

                //Capitalize nombres
                candidates = candidates.Select(x => new ProveedorUpdateVm()
                {
                    Id = x.Id,
                    NewNombre = x.NewNombre.Trim().ToUpper(), NewDescripcion = x.NewDescripcion, NewNit = x.NewNit, NewTelefono = x.NewTelefono,
                    NewVisita_Lunes = x.NewVisita_Lunes, NewVisita_Martes = x.NewVisita_Martes, NewVisita_Miercoles = x.NewVisita_Miercoles,
                    NewVisita_Jueves = x.NewVisita_Jueves, NewVisita_Viernes = x.NewVisita_Viernes, NewVisita_Sabado = x.NewVisita_Sabado,
                    NewVisita_Domingo = x.NewVisita_Domingo, NewEntrega_Lunes = x.NewEntrega_Lunes, NewEntrega_Martes = x.NewEntrega_Martes,
                    NewEntrega_Miercoles = x.NewEntrega_Miercoles, NewEntrega_Jueves = x.NewEntrega_Jueves, NewEntrega_Viernes = x.NewEntrega_Viernes,
                    NewEntrega_Sabado = x.NewEntrega_Sabado, NewEntrega_Domingo = x.NewEntrega_Domingo
                }).ToList();  

                var actualRecords = domainCtx.ProveedoresProyectoUsta.ToList();

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new { x.NewNombre }).Where(x => x.Count() > 1)
                                            .Select(x => x.Key.NewNombre).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => x.NewNombre == y));

                //records excluidos por preexistencia de nombre en DB
                var existantNames = actualRecords.Select(x =>x.Nombre).ToList();
                existantNames.AddRange(candidates.Select(x => x.NewNombre ));
                lstDuplicateName = existantNames.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
              
                candidates.RemoveAll(x => lstDuplicateName.Exists(y => x.NewNombre == y));

                if (candidates.Count > 0)
                {
                    foreach (ProveedorUpdateVm candidate in candidates)
                    {                        
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().IdUsuarioModificador = idUser;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().FechaModificado = DateTime.Now;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Descripcion = candidate.NewDescripcion;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Nombre = candidate.NewNombre;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Telefono = candidate.NewTelefono;                        
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Nit = candidate.NewNit;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Lunes = candidate.NewVisita_Lunes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Martes = candidate.NewVisita_Martes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Miercoles = candidate.NewVisita_Miercoles;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Jueves = candidate.NewVisita_Jueves;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Viernes = candidate.NewVisita_Viernes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Sabado = candidate.NewVisita_Sabado;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Visita_Domingo = candidate.NewVisita_Domingo;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Lunes = candidate.NewEntrega_Lunes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Martes = candidate.NewEntrega_Martes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Miercoles = candidate.NewEntrega_Miercoles;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Jueves = candidate.NewEntrega_Jueves;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Viernes = candidate.NewEntrega_Viernes;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Sabado = candidate.NewEntrega_Sabado;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Entrega_Domingo = candidate.NewEntrega_Domingo;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Nit = candidate.NewNit;
                    }
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    candidates.ForEach(x => rp.MessageOk.Add(x.NewNombre + " actualizado"));
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se agregaron registros!");
                }

                if (ownExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    ownExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x + " excluido por que se enviaron dos registros similares"));
                }

                if (lstDuplicateName.Count > 0)
                {
                    rp.Alert = true;
                    lstDuplicateName.ForEach(x => rp.MessageAlert.Add(x + " excluido porque existe un registro con el mismo nombre en la ubicacion seleccionada"));
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public ResponseBasicVm Update(ProveedorUpdateVm candidate, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //capitalize name
                candidate.NewNombre = candidate.NewNombre.ToUpper();

                var actualRecords = domainCtx.ProveedoresProyectoUsta.ToList();
                var currentRecord = actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault();
                if(currentRecord != null)
                {
                    if ((currentRecord.Nombre == candidate.NewNombre) ||
                        (actualRecords.Where(x => x.Nombre == candidate.NewNombre).FirstOrDefault() == null) ||
                        (actualRecords.Where(x => x.Nombre == candidate.NewNombre).FirstOrDefault().Id == candidate.Id))
                    {
                        currentRecord.Descripcion = candidate.NewDescripcion;
                        currentRecord.Entrega_Domingo = candidate.NewEntrega_Domingo;
                        currentRecord.Entrega_Jueves = candidate.NewEntrega_Jueves;
                        currentRecord.Entrega_Lunes = candidate.NewEntrega_Lunes;
                        currentRecord.Entrega_Martes = candidate.NewEntrega_Martes;
                        currentRecord.Entrega_Miercoles = candidate.NewEntrega_Miercoles;
                        currentRecord.Entrega_Sabado = candidate.NewEntrega_Sabado;
                        currentRecord.Entrega_Viernes = candidate.NewEntrega_Viernes;
                        currentRecord.FechaModificado = DateTime.Now;
                        currentRecord.IdUsuarioModificador = idUser;
                        currentRecord.Nit = candidate.NewNit;
                        currentRecord.Nombre = candidate.NewNombre;
                        currentRecord.Telefono = candidate.NewTelefono;
                        currentRecord.Visita_Domingo = candidate.NewVisita_Domingo;
                        currentRecord.Visita_Jueves = candidate.NewVisita_Jueves;
                        currentRecord.Visita_Lunes = candidate.NewVisita_Lunes;
                        currentRecord.Visita_Martes = candidate.NewVisita_Martes;
                        currentRecord.Visita_Miercoles = candidate.NewVisita_Miercoles;
                        currentRecord.Visita_Sabado = candidate.NewVisita_Sabado;
                        currentRecord.Visita_Viernes = candidate.NewVisita_Viernes;

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
        #endregion
        
        #region [DELETE]
        public ResponseBasicVm Delete(List<short> candidates)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstExcludingDependant = new List<short>();

                //chequeo existencias en DB
                var actualRecords = domainCtx.ProveedoresProyectoUsta.Where(x => candidates.Contains(x.Id)).ToList();
                if (actualRecords.Count > 0)
                {
                    //chequeo de dependencias
                    //lstExcludingDependant

                    //limpiar candidates quitando los dependientes
                    actualRecords.RemoveAll(x => lstExcludingDependant.Exists(y => x.Id == y));
                                       
                    if (candidates.Count > 0)
                    {
                        domainCtx.ProveedoresProyectoUsta.RemoveRange(actualRecords);
                        domainCtx.SaveChanges();
                        rp.Success = true;
                        actualRecords.ForEach(x => rp.MessageOk.Add(x.Nombre + " eliminado."));
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("No se han ingresado registros!");
                    }

                    //avisar de dependientes
                    if (lstExcludingDependant.Count > 0)
                    {
                        rp.Alert = true;
                        lstExcludingDependant.ForEach(x => rp.MessageAlert.Add(x + " no eliminado por dependencia"));
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
        #endregion
    }
}
