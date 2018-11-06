using ProyectoGradoUstaSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
    public sealed class GeneralesBl
    {
        #region [FIELDS]
        ProyectoUstaEntities securityCtx;
        #endregion

        #region [CONSTRUCTOR]
        public GeneralesBl()
        {
            securityCtx = new ProyectoUstaEntities();
        }
        #endregion

        #region [GET]
        public IQueryable<IdiomasProyectoUsta> GetIdiomas()
        {
            return (from idiomas in securityCtx.IdiomasProyectoUsta
                    select idiomas).OrderBy(x => x.IdIdioma).AsQueryable();
        }
        #endregion
    }
}
