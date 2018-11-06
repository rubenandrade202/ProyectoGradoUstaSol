using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class CreditosBl
    {
        #region [FIELDS]
        ProyectoUstaDomainCtx domainCtx;
        #endregion

        #region [CONSTRUCTOR]
        public CreditosBl()
        {
            domainCtx = new ProyectoUstaDomainCtx();
        }
        #endregion

        #region [GET]
        public IQueryable<LogCreditosIgGrid> GetLogCreditos()
        {
            return (from logCreditos in domainCtx.LogCreditosProyectoUsta
                    join clientes in domainCtx.ClientesProyectoUsta on logCreditos.IdCliente equals clientes.Id
                    join vendedores in domainCtx.UsuariosProyectoUsta on logCreditos.IdUsuario equals vendedores.Id
                    select new LogCreditosIgGrid
                    {
                        Cliente = clientes.Nombre,
                        FechaRegistro = logCreditos.FechaRegistro,
                        Id = logCreditos.Id,
                        Valor = logCreditos.Valor,
                        Vendedor = vendedores.UserName
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
