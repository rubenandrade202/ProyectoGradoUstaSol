using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class UbicacionesBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion
        
        #region [CONSTRUCTOR]
        public UbicacionesBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion
        
        #region [GET]
        public IQueryable<UbicacionesMainIgVm> Get()
        {
            try
            {          
                return (from ubicaciones in domainCtx.UbicacionesProyectoUsta
                        join tipoUbicaciones in domainCtx.TipoDeUbicacionProyectoUsta on ubicaciones.IdTipoUbicacion equals tipoUbicaciones.Id
                        join usuarios in domainCtx.UsuariosProyectoUsta on ubicaciones.IdUsuarioCreador equals usuarios.Id
                        join usuariosLj in domainCtx.UsuariosProyectoUsta on ubicaciones.IdUsuarioModificador equals usuariosLj.Id into cardinalidadLj
                        from cardinalidad in cardinalidadLj.DefaultIfEmpty()
                        select new UbicacionesMainIgVm
                        {
                            Id = ubicaciones.Id,
                            Descripcion = ubicaciones.Descripcion,
                            FechaModificado = ubicaciones.FechaModificado,
                            FechaRegistro = ubicaciones.FechaRegistro,
                            IdTipoUbicacion = tipoUbicaciones.Id,
                            NombreTipoUbicacion = tipoUbicaciones.Nombre,
                            NombreUbicacion = ubicaciones.Nombre,
                            NombreUsuarioCreador = usuarios.UserName,
                            NombreUsuarioModificador = cardinalidad.UserName,
                            UrlImagen = ubicaciones.UrlImagen
                        }).OrderBy(x => x.NombreUbicacion).AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
        #endregion

        #region [ADD]
        public ResponseBasicVm Add(List<UbicacionesAddVm> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstCandidatesEntity = new List<UbicacionesProyectoUsta>();
                var currentRecords = domainCtx.UbicacionesProyectoUsta.Select(x => new BasicVm() { Id = x.IdTipoUbicacion, Value = x.Nombre }).ToList();
                var ownExcludingCandidates = new List<BasicVm>();
                var entityExcludingCandidates = new List<BasicVm>();

                //Capitalize nombres
                candidates = candidates.Select(x => new UbicacionesAddVm() {Nombre = x.Nombre.Trim().ToUpper(), Descripcion = x.Descripcion, IdTipoUbicacion = x.IdTipoUbicacion, UrlImagen = x.UrlImagen }).ToList();  //.ForEach(x => x.Nombre.Trim().ToUpper());

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new { x.IdTipoUbicacion, x.Nombre }).Where(x => x.Count() > 1)
                                            .Select(x => new BasicVm() { Id = x.Key.IdTipoUbicacion, Value = x.Key.Nombre }).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => x.IdTipoUbicacion == y.Id && y.Value == x.Nombre));

                // Records excluidos por preexistencia en BD                
                entityExcludingCandidates = candidates.Where(x => currentRecords.Exists(y => x.IdTipoUbicacion == y.Id && x.Nombre == y.Value))
                    .Select(z => new BasicVm() {Id= z.IdTipoUbicacion, Value = z.Nombre }).ToList();
                                                        
                //limpia candidatos por inconsistencia preexistencia en DB
                candidates.RemoveAll(x => entityExcludingCandidates.Exists(y => x.IdTipoUbicacion == y.Id && x.Nombre == y.Value));

                candidates.ForEach(x =>
                {
                    var nuevaUbicacion = new UbicacionesProyectoUsta();
                    nuevaUbicacion.Descripcion = x.Descripcion;
                    nuevaUbicacion.FechaRegistro = DateTime.Now;
                    nuevaUbicacion.IdEstado = true;
                    nuevaUbicacion.IdTipoUbicacion = x.IdTipoUbicacion;
                    nuevaUbicacion.IdUsuarioCreador = idUser;
                    nuevaUbicacion.Nombre = x.Nombre;
                    nuevaUbicacion.UrlImagen = x.UrlImagen;
                    lstCandidatesEntity.Add(nuevaUbicacion);
                });

                if(lstCandidatesEntity.Count > 0)
                {
                    domainCtx.UbicacionesProyectoUsta.AddRange(lstCandidatesEntity);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    lstCandidatesEntity.ForEach(x => rp.MessageOk.Add(x.Nombre + " ingresado."));
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ingresaron registros");
                }

                if(ownExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    ownExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido por que se enviaron dos registros similares"));
                }

                if(entityExcludingCandidates.Count > 0)
                {
                    rp.Alert = true;
                    entityExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido porque existe un registro con el mismo nombre en la ubicacion seleccionada"));
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
        public ResponseBasicVm Update(List<UbicacionesUpdateVm> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstDuplicateName = new List<BasicVm>();
                var ownExcludingCandidates = new List<BasicVm>();

                //Capitalize nombres
                candidates = candidates.Select(x => new UbicacionesUpdateVm() { Id = x.Id, NewNombre = x.NewNombre.Trim().ToUpper(), NewDescripcion = x.NewDescripcion, NewIdTipoUbicacion = x.NewIdTipoUbicacion, NewUrlImagen = x.NewUrlImagen }).ToList();  

                var actualRecords = domainCtx.UbicacionesProyectoUsta.ToList();

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new { x.NewIdTipoUbicacion, x.NewNombre }).Where(x => x.Count() > 1)
                                            .Select(x => new BasicVm() { Id = x.Key.NewIdTipoUbicacion, Value = x.Key.NewNombre }).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => x.NewIdTipoUbicacion == y.Id && y.Value == x.NewNombre));


                //Records excluidos por existir en BD una configuración similar            
                lstDuplicateName = (from existentes in actualRecords
                            join candidatos in candidates on new { existentes.IdTipoUbicacion, existentes.Nombre } equals new { IdTipoUbicacion = candidatos.NewIdTipoUbicacion, Nombre = candidatos.NewNombre }
                            group new { existentes, candidatos } by new { existentes.IdTipoUbicacion, existentes.Nombre } into agrupacion
                            where agrupacion.Count() > 1
                            select new BasicVm
                            {
                                Id = agrupacion.Key.IdTipoUbicacion,
                                Value = agrupacion.Key.Nombre
                            }).ToList();
                
                //se limpian candidatos errones para entrar
                candidates.RemoveAll(x => lstDuplicateName.Exists(y => x.NewIdTipoUbicacion == y.Id && x.NewNombre == y.Value));

                if(candidates.Count > 0)
                {
                    foreach (UbicacionesUpdateVm candidate in candidates)
                    {
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().IdTipoUbicacion = candidate.NewIdTipoUbicacion;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().IdUsuarioModificador = idUser;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Nombre = candidate.NewNombre;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().Descripcion = candidate.NewDescripcion;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().FechaModificado = DateTime.Now;
                        actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault().UrlImagen = candidate.NewUrlImagen;
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
                    ownExcludingCandidates.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido por que se enviaron dos registros similares"));
                }

                if (lstDuplicateName.Count > 0)
                {
                    rp.Alert = true;
                    lstDuplicateName.ForEach(x => rp.MessageAlert.Add(x.Value + " excluido porque existe un registro con el mismo nombre en la ubicacion seleccionada"));
                }                      
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public ResponseBasicVm Update(UbicacionesUpdateVm candidate, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //capitalize name
                candidate.NewNombre = candidate.NewNombre.ToUpper();
                var actualRecords = domainCtx.UbicacionesProyectoUsta.ToList();
                var currentRecord = actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault();

                if (currentRecord != null)
                {
                    if ((currentRecord.Nombre == candidate.NewNombre && currentRecord.IdTipoUbicacion == candidate.NewIdTipoUbicacion) ||
                        (actualRecords.Where(x => x.Nombre == candidate.NewNombre && x.IdTipoUbicacion == x.IdTipoUbicacion).FirstOrDefault() == null) //||
                        //(actualRecords.Where(x => x.Nombre == candidate.NewNombre).FirstOrDefault().Id == candidate.Id))
                        )
                    {
                        currentRecord.Descripcion = candidate.NewDescripcion;
                        currentRecord.FechaModificado = DateTime.Now;
                        currentRecord.IdTipoUbicacion = candidate.NewIdTipoUbicacion;
                        currentRecord.IdUsuarioModificador = idUser;
                        currentRecord.Nombre = candidate.NewNombre;
                        currentRecord.UrlImagen = candidate.NewUrlImagen;                       
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
                    rp.MessageBad.Add("El ID de enviado no existe en DB");
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
                var lstExcludingDependant = new List<BasicVm>();


                //chequeo existencias en DB
                var actualRecords = domainCtx.UbicacionesProyectoUsta.Where(x => candidates.Contains(x.Id)).ToList();
                if(actualRecords.Count > 0)
                {
                    //chequeo de dependencias
                    //lstExcludingDependant

                    //limpiar candidates quitando los dependientes
                    //actualRecords = actualRecords.RemoveAll(x => lstExcludingDependant.Exists(y => x.Id == y.Id));

                    if(candidates.Count > 0)
                    {
                        domainCtx.UbicacionesProyectoUsta.RemoveRange(actualRecords);
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
                    if(lstExcludingDependant.Count > 0)
                    {
                        rp.Alert = true;
                        lstExcludingDependant.ForEach(x => rp.MessageAlert.Add(x.Value +  " no eliminado por dependencia"));
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
