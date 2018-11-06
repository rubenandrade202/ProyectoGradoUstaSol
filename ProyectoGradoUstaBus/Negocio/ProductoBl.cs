using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class ProductoBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion
        
        #region [CONSTRUCTOR]
        public ProductoBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion
        
        #region [GET]
        public IQueryable<ProductoIgMainVm> Get(string cbFilter, string pvFilter)
        {         
            //dos nulos          
            if( (cbFilter == null || cbFilter == string.Empty) && (pvFilter == null || pvFilter == string.Empty))
            {
                return (from productos in domainCtx.ProductosProyectoUsta
                        from ubicacionesStock in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionStock == x.Id).DefaultIfEmpty()
                        from ubicacionesNegocio in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionNegocio == x.Id).DefaultIfEmpty()
                        from usuariosRegistro in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioRegistro == x.Id).DefaultIfEmpty()
                        from usuariosModificacion in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioModificador == x.Id).DefaultIfEmpty()
                        where productos.Activo == true
                        select new ProductoIgMainVm
                        {
                            CantidadActual = productos.CantidadActual,
                            CantidadUmbral = productos.CantidadUmbral,
                            FechaModificado = productos.FechaModificado,
                            FechaRegistro = productos.FechaRegistro,
                            Id = productos.Id,
                            IdUbicacionNegocio = ubicacionesNegocio.Id,
                            NombreUbicacionNegocio = ubicacionesNegocio.Nombre,
                            IdUbicacionStock = ubicacionesStock.Id,
                            NombreUbicacionStock = ubicacionesStock.Nombre,
                            IdUsuarioModificador = usuariosModificacion.Id,
                            NombreUsuarioModificador = usuariosModificacion.UserName,
                            IdUsuarioRegistro = usuariosRegistro.Id,
                            NombreUsuarioRegistro = usuariosRegistro.UserName,
                            Nombre = productos.Nombre,
                            Precio = productos.Precio,
                            Proveedores = (from proveedores in domainCtx.ProveedoresProyectoUsta
                                           join proveedoresProducto in domainCtx.ProductoProveedorProyectoUsta on proveedores.Id equals proveedoresProducto.IdProveedor
                                           where proveedoresProducto.IdProducto == productos.Id
                                           select new BasicDependantDecimalVm
                                           {
                                               Id = proveedores.Id,
                                               IdParent = proveedoresProducto.PrecioCompra,
                                               Value = proveedores.Nombre
                                           }).ToList(),
                            CodigosBarras = (from codigosProductos in domainCtx.ProductoCodigoDeBarrasProyectoUsta
                                             where codigosProductos.IdProducto == productos.Id
                                             select codigosProductos.CodigoDeBarras).ToList()
                        }).OrderBy(x => x.Nombre).AsQueryable();
            }
            //cbFilter ok pvfilter nulo
            else if( (cbFilter != null || cbFilter != String.Empty) && (pvFilter == null || pvFilter == String.Empty) )
            {
                var partialQuery = 
                 (from productos in domainCtx.ProductosProyectoUsta
                        from ubicacionesStock in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionStock == x.Id).DefaultIfEmpty()
                        from ubicacionesNegocio in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionNegocio == x.Id).DefaultIfEmpty()
                        from usuariosRegistro in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioRegistro == x.Id).DefaultIfEmpty()
                        from usuariosModificacion in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioModificador == x.Id).DefaultIfEmpty()
                        where productos.Activo == true
                        select new ProductoIgMainVm
                        {
                            CantidadActual = productos.CantidadActual,
                            CantidadUmbral = productos.CantidadUmbral,
                            FechaModificado = productos.FechaModificado,
                            FechaRegistro = productos.FechaRegistro,
                            Id = productos.Id,
                            IdUbicacionNegocio = ubicacionesNegocio.Id,
                            NombreUbicacionNegocio = ubicacionesNegocio.Nombre,
                            IdUbicacionStock = ubicacionesStock.Id,
                            NombreUbicacionStock = ubicacionesStock.Nombre,
                            IdUsuarioModificador = usuariosModificacion.Id,
                            NombreUsuarioModificador = usuariosModificacion.UserName,
                            IdUsuarioRegistro = usuariosRegistro.Id,
                            NombreUsuarioRegistro = usuariosRegistro.UserName,
                            Nombre = productos.Nombre,
                            Precio = productos.Precio,
                            Proveedores = (from proveedores in domainCtx.ProveedoresProyectoUsta
                                           join proveedoresProducto in domainCtx.ProductoProveedorProyectoUsta on proveedores.Id equals proveedoresProducto.IdProveedor
                                           where proveedoresProducto.IdProducto == productos.Id
                                           select new BasicDependantDecimalVm
                                           {
                                               Id = proveedores.Id,
                                               IdParent = proveedoresProducto.PrecioCompra,
                                               Value = proveedores.Nombre
                                           }).ToList(),
                            CodigosBarras = (from codigosProductos in domainCtx.ProductoCodigoDeBarrasProyectoUsta
                                             where codigosProductos.IdProducto == productos.Id
                                             select codigosProductos.CodigoDeBarras).ToList()
                        }).OrderBy(x => x.Nombre).ToList();                
                return partialQuery.Where(x => x.CodigosBarras.Contains(cbFilter)).AsQueryable();
            }
            //cbfilter nulo - pvfilter ok
            else if ((cbFilter == null || cbFilter == string.Empty) && (pvFilter != null && pvFilter != String.Empty))
            {
                var partialQuery =
                 (from productos in domainCtx.ProductosProyectoUsta
                  from ubicacionesStock in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionStock == x.Id).DefaultIfEmpty()
                  from ubicacionesNegocio in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionNegocio == x.Id).DefaultIfEmpty()
                  from usuariosRegistro in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioRegistro == x.Id).DefaultIfEmpty()
                  from usuariosModificacion in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioModificador == x.Id).DefaultIfEmpty()
                  where productos.Activo == true
                  select new ProductoIgMainVm
                  {
                      CantidadActual = productos.CantidadActual,
                      CantidadUmbral = productos.CantidadUmbral,
                      FechaModificado = productos.FechaModificado,
                      FechaRegistro = productos.FechaRegistro,
                      Id = productos.Id,
                      IdUbicacionNegocio = ubicacionesNegocio.Id,
                      NombreUbicacionNegocio = ubicacionesNegocio.Nombre,
                      IdUbicacionStock = ubicacionesStock.Id,
                      NombreUbicacionStock = ubicacionesStock.Nombre,
                      IdUsuarioModificador = usuariosModificacion.Id,
                      NombreUsuarioModificador = usuariosModificacion.UserName,
                      IdUsuarioRegistro = usuariosRegistro.Id,
                      NombreUsuarioRegistro = usuariosRegistro.UserName,
                      Nombre = productos.Nombre,
                      Precio = productos.Precio,
                      Proveedores = (from proveedores in domainCtx.ProveedoresProyectoUsta
                                     join proveedoresProducto in domainCtx.ProductoProveedorProyectoUsta on proveedores.Id equals proveedoresProducto.IdProveedor
                                     where proveedoresProducto.IdProducto == productos.Id
                                     select new BasicDependantDecimalVm
                                     {
                                         Id = proveedores.Id,
                                         IdParent = proveedoresProducto.PrecioCompra,
                                         Value = proveedores.Nombre
                                     }).ToList(),
                      CodigosBarras = (from codigosProductos in domainCtx.ProductoCodigoDeBarrasProyectoUsta
                                       where codigosProductos.IdProducto == productos.Id
                                       select codigosProductos.CodigoDeBarras).ToList()
                  }).OrderBy(x => x.Nombre).ToList();
                return partialQuery.Where(x => x.Proveedores.Any( y => y.Value.Contains(pvFilter))).AsQueryable();
            }
            //los dos filtros con data
            else 
            {
                var partialQuery =
                 (from productos in domainCtx.ProductosProyectoUsta
                  from ubicacionesStock in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionStock == x.Id).DefaultIfEmpty()
                  from ubicacionesNegocio in domainCtx.UbicacionesProyectoUsta.Where(x => productos.IdUbicacionNegocio == x.Id).DefaultIfEmpty()
                  from usuariosRegistro in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioRegistro == x.Id).DefaultIfEmpty()
                  from usuariosModificacion in domainCtx.UsuariosProyectoUsta.Where(x => productos.IdUsuarioModificador == x.Id).DefaultIfEmpty()
                  where productos.Activo == true
                  select new ProductoIgMainVm
                  {
                      CantidadActual = productos.CantidadActual,
                      CantidadUmbral = productos.CantidadUmbral,
                      FechaModificado = productos.FechaModificado,
                      FechaRegistro = productos.FechaRegistro,
                      Id = productos.Id,
                      IdUbicacionNegocio = ubicacionesNegocio.Id,
                      NombreUbicacionNegocio = ubicacionesNegocio.Nombre,
                      IdUbicacionStock = ubicacionesStock.Id,
                      NombreUbicacionStock = ubicacionesStock.Nombre,
                      IdUsuarioModificador = usuariosModificacion.Id,
                      NombreUsuarioModificador = usuariosModificacion.UserName,
                      IdUsuarioRegistro = usuariosRegistro.Id,
                      NombreUsuarioRegistro = usuariosRegistro.UserName,
                      Nombre = productos.Nombre,
                      Precio = productos.Precio,
                      Proveedores = (from proveedores in domainCtx.ProveedoresProyectoUsta
                                     join proveedoresProducto in domainCtx.ProductoProveedorProyectoUsta on proveedores.Id equals proveedoresProducto.IdProveedor
                                     where proveedoresProducto.IdProducto == productos.Id
                                     select new BasicDependantDecimalVm
                                     {
                                         Id = proveedores.Id,
                                         IdParent = proveedoresProducto.PrecioCompra,
                                         Value = proveedores.Nombre
                                     }).ToList(),
                      CodigosBarras = (from codigosProductos in domainCtx.ProductoCodigoDeBarrasProyectoUsta
                                       where codigosProductos.IdProducto == productos.Id
                                       select codigosProductos.CodigoDeBarras).ToList()
                  }).OrderBy(x => x.Nombre).ToList();
                var prePartial = partialQuery.Where(x => x.CodigosBarras.Contains(cbFilter)).AsQueryable();
                return prePartial.Where(x => x.Proveedores.Any(y => y.Value.Contains(pvFilter))).AsQueryable();
            }
        }

        public IQueryable<BasicDependantVm> GetProveedoresProducto(int idProducto)
        {
            return (from proveedorProducto in domainCtx.ProductoProveedorProyectoUsta
                    join proveedor in domainCtx.ProveedoresProyectoUsta on proveedorProducto.IdProveedor equals proveedor.Id
                    where proveedorProducto.IdProducto == idProducto
                    select new BasicDependantVm
                    {
                        Id = proveedor.Id,
                        Value = proveedor.Nombre,
                        IdParent = (int)proveedorProducto.PrecioCompra
                    }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<string> GetCodeBarProducto(int idProducto)
        {
            return domainCtx.ProductoCodigoDeBarrasProyectoUsta.Where(x => x.IdProducto == idProducto).Select(x => x.CodigoDeBarras).OrderBy(x => x).AsQueryable();
        }

        public IQueryable<ProductoFaltanteProveedorIgVm> GetProductosProveedorFaltantes(bool umbralFilter)
        {
            SqlParameter filterPrm = new SqlParameter("@filterPrm", umbralFilter);
            return ((domainCtx as IObjectContextAdapter).ObjectContext).ExecuteStoreQuery<ProductoFaltanteProveedorIgVm>("GetProductosFaltantesProyectoUsta @filterPrm", filterPrm).ToList().AsQueryable();                         
        }
        #endregion

        #region [ADD]
        public ResponseBasicVm Add(List<ProductoAddVm> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //var lstCandidatesEntity = new List<ProductosProyectoUsta>();
                var currentRecords = domainCtx.ProductosProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).ToList();
                var ownExcludingCandidates = new List<BasicVm>();
                var entityExcludingCandidates = new List<BasicVm>();
                var flagInsert = false;

                //Capitalize nombres
                candidates = candidates.Select(x => new ProductoAddVm(){ Nombre = x.Nombre.Trim().ToUpper(), CantidadActual = x.CantidadActual, CantidadUmbral= x.CantidadUmbral, IdUbicacionNegocio = x.IdUbicacionNegocio, IdUbicacionStock = x.IdUbicacionStock, Precio = x.Precio, CodigosBarras= x.CodigosBarras, Proveedores = x.Proveedores}).ToList();

                //Records excluidos por mal envio
                ownExcludingCandidates = candidates.GroupBy(x => new { x.Nombre }).Where(x => x.Count() > 1)
                                            .Select(x => new BasicVm() { Value = x.Key.Nombre }).ToList();

                //limpia candidatos por inconsistencias enviadas
                candidates.RemoveAll(x => ownExcludingCandidates.Exists(y => y.Value == x.Nombre));

                // Records excluidos por preexistencia en BD                
                entityExcludingCandidates = candidates.Where(x => currentRecords.Exists(y => x.Nombre == y.Value)).Select(z => new BasicVm() { Id= 0, Value = z.Nombre }).ToList();

                //limpia candidatos por inconsistencia preexistencia en DB
                candidates.RemoveAll(x => entityExcludingCandidates.Exists(y => x.Nombre == y.Value));

                candidates.ForEach(x =>
                {
                    flagInsert = true;
                    var nuevoProducto = new ProductosProyectoUsta();
                    nuevoProducto.Activo = true;
                    nuevoProducto.CantidadActual = x.CantidadActual;
                    nuevoProducto.CantidadUmbral = x.CantidadUmbral;
                    nuevoProducto.FechaRegistro = DateTime.Now;
                    nuevoProducto.IdUbicacionNegocio = x.IdUbicacionNegocio;
                    nuevoProducto.IdUbicacionStock = x.IdUbicacionStock;
                    nuevoProducto.IdUsuarioRegistro = idUser;
                    nuevoProducto.Nombre = x.Nombre;
                    nuevoProducto.Precio = x.Precio;

                    domainCtx.ProductosProyectoUsta.Add(nuevoProducto);
                    domainCtx.SaveChanges();

                    rp.Success = true;
                    rp.MessageOk.Add(x.Nombre + " ingresado.");

                    if(x.Proveedores.Count > 0)
                    {
                        var lstCandidatesProductoProveedor = new List<ProductoProveedorProyectoUsta>();
                        foreach (var item in x.Proveedores)
	                    {
		                    var nuevoProductoProveedor = new ProductoProveedorProyectoUsta();
                            nuevoProductoProveedor.FechaRegistro = DateTime.Now;
                            nuevoProductoProveedor.IdProducto = nuevoProducto.Id;
                            nuevoProductoProveedor.IdProveedor = (short)item.Id;
                            nuevoProductoProveedor.IdUsuarioRegistro = idUser;
                            nuevoProductoProveedor.PrecioCompra = item.IdParent;
                            lstCandidatesProductoProveedor.Add(nuevoProductoProveedor);
	                    }
                        if(lstCandidatesProductoProveedor.Count > 0)
                        {
                            domainCtx.ProductoProveedorProyectoUsta.AddRange(lstCandidatesProductoProveedor);
                            domainCtx.SaveChanges();
                        }                      
                    }

                    if (x.CodigosBarras.Count > 0)
                    {
                        var lstCandidatesProductoCodigosBarras= new List<ProductoCodigoDeBarrasProyectoUsta>();
                        foreach (var item in x.CodigosBarras)
                        {
                            var nuevoProductoCodigoBarras = new ProductoCodigoDeBarrasProyectoUsta();
                            nuevoProductoCodigoBarras.CodigoDeBarras = item;
                            nuevoProductoCodigoBarras.IdProducto = nuevoProducto.Id;                            
                            lstCandidatesProductoCodigosBarras.Add(nuevoProductoCodigoBarras);
                        }
                        if (lstCandidatesProductoCodigosBarras.Count > 0)
                        {
                            domainCtx.ProductoCodigoDeBarrasProyectoUsta.AddRange(lstCandidatesProductoCodigosBarras);
                            domainCtx.SaveChanges();
                        }
                    }
                });

                if(!flagInsert)
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
        public ResponseBasicVm Update(ProductoUpdateVm candidate, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                //capitalize name
                candidate.NewNombre = candidate.NewNombre.ToUpper();
                var actualRecords = domainCtx.ProductosProyectoUsta.ToList();
                var currentRecord = actualRecords.Where(x => x.Id == candidate.Id).FirstOrDefault();
                if (currentRecord != null)
                {                    
                    if ((currentRecord.Nombre == candidate.NewNombre) ||
                        (actualRecords.Where(x => x.Nombre == candidate.NewNombre).FirstOrDefault() == null) ||
                        (actualRecords.Where(x => x.Nombre == candidate.NewNombre).FirstOrDefault().Id == candidate.Id))
                    {
                        var lstProductoProvider = new List<ProductoProveedorProyectoUsta>();
                        var lstProductoCodeBar = new List<ProductoCodigoDeBarrasProyectoUsta>();

                        currentRecord.CantidadActual = candidate.NewCantidadActual;
                        currentRecord.CantidadUmbral = candidate.NewCantidadUmbral;
                        currentRecord.FechaModificado = DateTime.Now;
                        currentRecord.IdUbicacionNegocio = candidate.NewIdUbicacionNegocio;
                        currentRecord.IdUbicacionStock = candidate.NewIdUbicacionStock;
                        currentRecord.IdUsuarioModificador = idUser;
                        currentRecord.Nombre = candidate.NewNombre;
                        currentRecord.Precio = candidate.NewPrecio;
                        currentRecord.Activo = true;
                        
                        var dependantCodeBar = domainCtx.ProductoCodigoDeBarrasProyectoUsta.Where(x => x.IdProducto == candidate.Id).ToList();
                        domainCtx.ProductoCodigoDeBarrasProyectoUsta.RemoveRange(dependantCodeBar);
                        candidate.NewCodigosBarras.ForEach(x =>
                        {
                            var newCodeBarProducto = new ProductoCodigoDeBarrasProyectoUsta();
                            newCodeBarProducto.CodigoDeBarras = x;
                            newCodeBarProducto.IdProducto = candidate.Id;
                            lstProductoCodeBar.Add(newCodeBarProducto);
                        });

                        if(lstProductoCodeBar.Count > 0)
                        {
                            domainCtx.ProductoCodigoDeBarrasProyectoUsta.AddRange(lstProductoCodeBar);
                        }


                        var dependantProviders = domainCtx.ProductoProveedorProyectoUsta.Where(x => x.IdProducto == candidate.Id).ToList();
                        domainCtx.ProductoProveedorProyectoUsta.RemoveRange(dependantProviders);
                        candidate.NewProveedores.ForEach(x =>
                            {
                                var newProductoProvider = new ProductoProveedorProyectoUsta();
                                newProductoProvider.IdProducto = candidate.Id;
                                newProductoProvider.FechaRegistro = DateTime.Now;
                                newProductoProvider.IdProveedor = (short)x.Id;
                                newProductoProvider.IdUsuarioRegistro = idUser;
                                newProductoProvider.PrecioCompra = x.IdParent;
                                lstProductoProvider.Add(newProductoProvider);
                            });

                        if(lstProductoProvider.Count > 0)
                        {
                            domainCtx.ProductoProveedorProyectoUsta.AddRange(lstProductoProvider);
                        }
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

        public ResponseBasicVm UpdateProductoProveedor(List<int> productos, List<int> proveedores, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var idProductsToWork = domainCtx.ProductosProyectoUsta.Where(x => productos.Contains(x.Id)).Select(y => y.Id).ToList();
                if(idProductsToWork.Count > 0)
                {
                    var currentRelations = domainCtx.ProductoProveedorProyectoUsta.Select(x => new BasicIntVm() { Id = x.IdProducto, Value = x.IdProveedor }).ToList();
                    var newRelations = new List<ProductoProveedorProyectoUsta>();
                    foreach (var idProducto in productos)
                    {
                        foreach (var idProveedor in proveedores)
                        {
                            var existeRelacion = currentRelations.Where(x => x.Id == idProducto && x.Value == idProveedor).FirstOrDefault();
                            if(existeRelacion == null)
                            {
                                newRelations.Add(new ProductoProveedorProyectoUsta()
                                {
                                    FechaRegistro = DateTime.Now,
                                    IdProducto = idProducto,
                                    IdProveedor = (short)idProveedor,
                                    IdUsuarioRegistro = idUsuarioRegistro,
                                    PrecioCompra = 0
                                });
                            }
                        }
                    }
                    if(newRelations.Count > 0)
                    {
                        domainCtx.ProductoProveedorProyectoUsta.AddRange(newRelations);
                        domainCtx.SaveChanges();
                        rp.Success = true;
                        rp.MessageOk.Add("Relaciones realizadas");
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("No se relacionaron los productos con el proveedor porque ya existe la relación");
                    }
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se encontraron productos con los id enviados");
                }                
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public ResponseBasicVm UpdateGeneric(List<ProductosProyectoUsta> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var idCandidates = candidates.Select(x => x.Id).Distinct().ToList();
                var actualRecords = domainCtx.ProductosProyectoUsta.Where(x => idCandidates.Contains(x.Id)).ToList();
                if(actualRecords.Count > 0)
                {
                    actualRecords.ForEach(x =>
                    {
                        var bufferData = candidates.Where(y => y.Id == x.Id).FirstOrDefault();
                        x.CantidadActual = bufferData.CantidadActual;
                        x.CantidadUmbral = bufferData.CantidadUmbral;// != 0 ? bufferData.CantidadUmbral : x.CantidadUmbral;
                        x.FechaModificado = DateTime.Now;
                        x.IdUsuarioModificador = idUser;
                    });
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Se han actualizado los registros");
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No existen registros en BD con los valores enviados");
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());                
            }
            return rp;
        }

        /// <summary>
        /// BasicInt Vm Use Description
        /// Id: IdProducto
        /// Value: IdProveedor
        /// </summary>
        /// <param name="productosproveedor"></param>
        /// <param name="idUsuarioRegistro"></param>
        /// <returns></returns>
        public ResponseBasicVm UpdateDesasociarProductoProveedor(List<BasicIntVm> productosproveedor, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var currentRelations = domainCtx.ProductoProveedorProyectoUsta.ToList();
                var recordsToDelete = new List<ProductoProveedorProyectoUsta>();
                foreach (BasicIntVm item in productosproveedor)
                {
                    var existantRecord = currentRelations.Where(x => x.IdProducto == item.Id && x.IdProveedor == item.Value).FirstOrDefault();
                    if(existantRecord != null)
                    {
                        recordsToDelete.Add(existantRecord);
                    }
                }
                if(recordsToDelete.Count > 0)
                {
                    var currentProductosAPedir = domainCtx.ProductosAPedirProyectoUsta.ToList();
                    var lstProductosAPedirOldIdsToDelete = new List<ProductosAPedirProyectoUsta>();
                    if(currentProductosAPedir.Count > 0)
                    {
                        recordsToDelete.ForEach(x =>
                        {
                            var currentOldRecordToDelete = currentProductosAPedir.Where(y => y.IdProducto == x.IdProducto && y.IdProveedor == x.IdProveedor).FirstOrDefault();
                            if(currentOldRecordToDelete != null)
                            {
                                lstProductosAPedirOldIdsToDelete.Add(currentOldRecordToDelete);
                            }
                        });
                    }

                    domainCtx.ProductosAPedirProyectoUsta.RemoveRange(lstProductosAPedirOldIdsToDelete);
                    domainCtx.ProductoProveedorProyectoUsta.RemoveRange(recordsToDelete);
                    domainCtx.SaveChanges();
                    rp.Success = true;
                    rp.MessageOk.Add("Relaciones eliminadas");
                    if(lstProductosAPedirOldIdsToDelete.Count > 0)
                    {
                        rp.MessageOk.Add("Por la desasociación se han eliminado productos ya pedidos, vuelva a programar su pedido en ellos");
                    }
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No existen relaciones coincidentes en la BD");
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
        public ResponseBasicVm Delete(List<int> candidates, int idUser)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var lstEntitiesDependant = new List<ProductosProyectoUsta>();
                var flagEliminacion = false;

                //chequeo existencias en DB
                var actualRecords = domainCtx.ProductosProyectoUsta.Where(x => candidates.Contains(x.Id)).ToList();
                if (actualRecords.Count > 0)
                {
                    var idsContextProducts = actualRecords.Select(x => x.Id).ToList();
                    var idsDependantBarCodes = domainCtx.ProductoCodigoDeBarrasProyectoUsta.Where(x => idsContextProducts.Contains(x.IdProducto)).Select(y => y.IdProducto).ToList();                    
                    //chequeo de dependencias, se pasan a nueva lista los dependientes
                    //inicialmente codigos de barras mas adelante mirar que validaciones importantes se deben tener en cuenta
                    lstEntitiesDependant = actualRecords.Where(x => idsDependantBarCodes.Contains(x.Id)).ToList();                    
                    //limpiar candidates quitando los dependientes
                    actualRecords.RemoveAll(x => lstEntitiesDependant.Exists(y => x.Id == y.Id));

                    //Se eliminan los registros que no tienen ningun registro en el sistema
                    if (actualRecords.Count > 0)
                    {
                        flagEliminacion = true;
                        var actualIdsRecordsToDelete = actualRecords.Select(y => y.Id).ToList();
                        var dependantProductoProvider = domainCtx.ProductoProveedorProyectoUsta.Where(x => actualIdsRecordsToDelete.Contains(x.IdProducto)).ToList();

                        domainCtx.ProductosProyectoUsta.RemoveRange(actualRecords);
                        if (dependantProductoProvider.Count > 0)
                        {
                            domainCtx.ProductoProveedorProyectoUsta.RemoveRange(dependantProductoProvider);
                        }
                        
                        domainCtx.SaveChanges();                        
                        actualRecords.ForEach(x => rp.MessageOk.Add(x.Nombre + " eliminado."));
                    }                   

                    //Estos dependientes se les cambia el estado a inactivo porque tienen historial en el sistema
                    if (lstEntitiesDependant.Count > 0)
                    {
                        flagEliminacion = true;
                        lstEntitiesDependant.ForEach(x =>
                            {
                                x.Activo = false;
                                x.IdUsuarioModificador = idUser;
                            });
                        domainCtx.SaveChanges();
                        lstEntitiesDependant.ForEach(x => rp.MessageOk.Add(x.Nombre + " desactivado por dependencia"));
                    }

                    //Se eliminan los registros relacionados con pedidos programados
                    var recordsPedidosRelated = domainCtx.ProductosAPedirProyectoUsta.Where(x => idsContextProducts.Contains(x.IdProducto)).ToList();
                    domainCtx.ProductosAPedirProyectoUsta.RemoveRange(recordsPedidosRelated);
                    domainCtx.SaveChanges();

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
        /// <summary>
        /// If exists TRUE
        /// If not exist false
        /// </summary>
        /// <param name="codeBar"></param>
        /// <returns></returns>
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
                        rp.Data = record;
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
        #endregion
    }
}
