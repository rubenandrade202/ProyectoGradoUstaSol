using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class ReporteBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx ctxDomain;
        #endregion
        
        #region [CONSTRUCTOR]
        public ReporteBl()
        {
            ctxDomain = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        public IQueryable<MovimientoCajaIgRpt> GetReporteMovimiento(DateTime fechaInicial, DateTime fechaFinal)
        {
            if(fechaInicial == default(DateTime) || fechaFinal == default(DateTime))
            {
                fechaInicial = DateTime.Now.Date.AddYears(-3);
                fechaFinal = DateTime.Now.AddDays(1).Date;                
            }

            return (from movimientos in ctxDomain.MovimientosCajaProyectoUsta
                    join tipoMovimiento in ctxDomain.TiposMovimientoCajaProyectoUsta on movimientos.IdTipoMovimiento equals tipoMovimiento.Id
                    group new { movimientos, tipoMovimiento } by new { NombreMovimiento = tipoMovimiento.Nombre, FechaMovimiento = DbFunctions.TruncateTime(movimientos.FechaRegsitro) } into agrupado
                    where agrupado.Key.FechaMovimiento >= fechaInicial && agrupado.Key.FechaMovimiento <= fechaFinal
                    select new MovimientoCajaIgRpt
                    {
                        FechaMovimiento = (DateTime)agrupado.Key.FechaMovimiento,
                        NombreMovimiento = agrupado.Key.NombreMovimiento,
                        Total = agrupado.Sum(x => x.movimientos.Valor)
                    }).OrderByDescending(x => x.FechaMovimiento).AsQueryable();
        }

        public IQueryable<VentasIgRpt> GetReporteVentas(DateTime fechaInicial, DateTime fechaFinal)
        {
            if (fechaInicial == default(DateTime) || fechaFinal == default(DateTime))
            {
                fechaInicial = DateTime.Now.Date.AddDays(-15);
                fechaFinal = DateTime.Now.AddDays(1).Date;
            }

            return (from ventas in ctxDomain.VentasProyectoUsta
                        join vendedores in ctxDomain.UsuariosProyectoUsta on ventas.IdVendedor equals vendedores.Id
                        join productos in ctxDomain.ProductosProyectoUsta on ventas.IdProducto equals productos.Id into agrupacionUno
                        from newSource in agrupacionUno.DefaultIfEmpty()
                        join clientes in ctxDomain.ClientesProyectoUsta on ventas.IdCliente equals clientes.Id into agrupacionDos
                        from newSourceDos in agrupacionDos.DefaultIfEmpty()
                        //join proveedoresProducto in ctxDomain.ProductoProveedorProyectoUsta on newSource.Id equals proveedoresProducto.IdProducto into agrupacionTres
                        //from newSourceTres in agrupacionTres.DefaultIfEmpty()
                        //join proveedores in ctxDomain.ProveedoresProyectoUsta on newSourceTres.IdProveedor equals proveedores.Id into agrupacionCuatro
                        //from newSourceCuatro in agrupacionCuatro.DefaultIfEmpty()
                        where ventas.FechaRegistro >= fechaInicial && ventas.FechaRegistro <= fechaFinal
                        select new VentasIgRpt
                        {
                            Cantidad = ventas.Cantidad,
                            ClienteNombre = (newSourceDos.Nombre == null) ? "ANY" : newSourceDos.Nombre,
                            FechaRegistro = ventas.FechaRegistro,
                            IdProducto = ventas.IdProducto,
                            NombreProducto = (ventas.IdProducto == 150000) ? "GENERICO 50" :
                                            (ventas.IdProducto == 150001) ? "GENERICO 100" :
                                            (ventas.IdProducto == 150002) ? "GENERICO 200" :
                                            (ventas.IdProducto == 150003) ? "GENERICO 500" :
                                            newSource.Nombre,
                            Total = ventas.Total,
                            VendedorNombre = vendedores.UserName//,
                            //ProveedorNombre = newSourceCuatro.Nombre == null ? "SIN PROVEEDOR" : newSourceCuatro.Nombre
                        }).OrderByDescending(x => x.FechaRegistro).AsQueryable();                                   
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
