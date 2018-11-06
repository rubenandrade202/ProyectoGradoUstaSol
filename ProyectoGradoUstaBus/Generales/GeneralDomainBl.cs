using ProyectoGradoUstaCommon;
using ProyectoGradoUstaSecurity;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class GeneralDomainBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        List<BasicVm> DatosPrueba = new List<BasicVm>()
        {
            new BasicVm(){Id=1, Value="1"},
            new BasicVm(){Id=2, Value="2"},
            new BasicVm(){Id=3, Value="3"},
            new BasicVm(){Id=4, Value="4"},
            new BasicVm(){Id=5, Value="5"},
            new BasicVm(){Id=6, Value="6"},
            new BasicVm(){Id=7, Value="7"},
            new BasicVm(){Id=8, Value="8"},
            new BasicVm(){Id=9, Value="9"},
            new BasicVm(){Id=10, Value="10"}            
        };

        #endregion
        
        #region [CONSTRUCTOR]
        public GeneralDomainBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        public IQueryable<BasicVm> GetPruebaBorrar()
        {                     
            return DatosPrueba.Select(x => new BasicVm() { Id = x.Id, Value = x.Value }).OrderBy(x => x.Value).AsQueryable();           
        }

        public IQueryable<BasicVm> GetTiposUbicacion()
        {
            return domainCtx.TipoDeUbicacionProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<BasicVm> GetUbicacionesStock()
        {
            return domainCtx.UbicacionesProyectoUsta.Where(x => x.IdTipoUbicacion == 2 && x.IdEstado == true).Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<BasicVm> GetUbicacionesNegocio()
        {
            return domainCtx.UbicacionesProyectoUsta.Where(x => x.IdTipoUbicacion == 1 && x.IdEstado == true).Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<BasicVm> GetProveedores()
        {
            return domainCtx.ProveedoresProyectoUsta.Where(x => x.IdEstado == true).Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<ProductosProyectoUsta> GetProductos()
        {
            return domainCtx.ProductosProyectoUsta.Where(x => x.Activo == true).OrderBy(x => x.Nombre).AsQueryable();//.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<BasicVm> GetClientes()
        {
            return domainCtx.ClientesProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).OrderBy(x => x.Value).AsQueryable();
        }

        public IQueryable<BasicVm> GetTiposMovimiento()
        {
            //return domainCtx.TiposMovimientoCajaProyectoUsta.Select(x => new BasicVm() { Id = x.Id, Value = x.Nombre }).Where(x => x.Id != 1 || x.Id != 2 || x.Id != 8).OrderBy(x => x.Value).AsQueryable();
            return (from tipos in domainCtx.TiposMovimientoCajaProyectoUsta
                    where tipos.Id != 1 && tipos.Id != 2 && tipos.Id != 8
                    select new BasicVm
                    {
                        Id = tipos.Id,
                        Value = tipos.Nombre
                    }).OrderBy(x => x.Value).AsQueryable();

        }


        #endregion

        #region [ADD]
        #endregion

        #region [UPDATE]
        #endregion

        #region [DELETE]
        #endregion

        #region [CHECK]
        #endregion
    }
}
