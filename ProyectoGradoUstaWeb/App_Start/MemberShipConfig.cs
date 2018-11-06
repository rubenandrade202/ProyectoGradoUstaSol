using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using System.Configuration;

namespace ProyectoGradoUstaWeb
{
    public class MemberShipConfig
    {
        public static void Initialize()
        {
            var appSettings = ConfigurationManager.AppSettings;
            //Datos de configuración especificos de Membership, son para ser utilizados una unica vez y en resumen se encargan de crear las tablas de administración membership.
            var initializeOrder = Convert.ToBoolean(appSettings["InitializeMembershipTables"]);
            var userTableName = appSettings["UserTablesMemberShip"].ToString();
            var idFieldUsers = appSettings["IdUserTableMemberShip"].ToString();
            var userNameFieldUsers = appSettings["UserNameUserTablesMemberShip"].ToString();
            var conectionStringMemberShip = appSettings["ConnectionStringMemberShip"].ToString();
            //Este comando es necesario siempre, salvo que cuando ya se han creado se realizara solo la conexión sin crear las tablas de membership. Eso lo
            //define la variable initializeOrder      
            WebSecurity.InitializeDatabaseConnection(conectionStringMemberShip, userTableName, idFieldUsers, userNameFieldUsers, initializeOrder);
            //En su primera ejcución se creara el usuario super administrador con la contraseña aca predefinida.
            if(initializeOrder)
            {
                WebSecurity.CreateUserAndAccount("superadmin", "erpAX2014*sharepoint");
            }
        }
    }
}