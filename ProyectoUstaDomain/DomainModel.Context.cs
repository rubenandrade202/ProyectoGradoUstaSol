﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoUstaDomain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProyectoUstaDomainCtx : DbContext
    {
        public ProyectoUstaDomainCtx()
            : base("name=ProyectoUstaDomainCtx")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TipoDeUbicacionProyectoUsta> TipoDeUbicacionProyectoUsta { get; set; }
        public virtual DbSet<UbicacionesProyectoUsta> UbicacionesProyectoUsta { get; set; }
        public virtual DbSet<UsuariosProyectoUsta> UsuariosProyectoUsta { get; set; }
        public virtual DbSet<ProveedoresProyectoUsta> ProveedoresProyectoUsta { get; set; }
        public virtual DbSet<ProductoCodigoDeBarrasProyectoUsta> ProductoCodigoDeBarrasProyectoUsta { get; set; }
        public virtual DbSet<ProductosProyectoUsta> ProductosProyectoUsta { get; set; }
        public virtual DbSet<ProductoProveedorProyectoUsta> ProductoProveedorProyectoUsta { get; set; }
        public virtual DbSet<VentasProyectoUsta> VentasProyectoUsta { get; set; }
        public virtual DbSet<MovimientosCajaProyectoUsta> MovimientosCajaProyectoUsta { get; set; }
        public virtual DbSet<TiposMovimientoCajaProyectoUsta> TiposMovimientoCajaProyectoUsta { get; set; }
        public virtual DbSet<LogCreditosProyectoUsta> LogCreditosProyectoUsta { get; set; }
        public virtual DbSet<ProductosAPedirProyectoUsta> ProductosAPedirProyectoUsta { get; set; }
        public virtual DbSet<LogPedidosProyectoUsta> LogPedidosProyectoUsta { get; set; }
        public virtual DbSet<ClientesProyectoUsta> ClientesProyectoUsta { get; set; }
        public virtual DbSet<ProductosBufferAInventarioProyectoUsta> ProductosBufferAInventarioProyectoUsta { get; set; }
    }
}
