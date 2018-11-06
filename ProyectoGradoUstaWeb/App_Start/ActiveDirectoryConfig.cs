using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public class ActiveDirectoryConfig
    {
        public static bool ValidationsAD(string userName, string passWord)
        {
            var result = false;
            var pathAd = ConfigurationManager.AppSettings["PathAd"];

            //Validación correcta configuración
            if (pathAd == null || pathAd == string.Empty)
            {
                //informar a administrador 
                result = false;
            }
            else
            {
                pathAd = @"LDAP://" + pathAd + " /DC=" + pathAd.Split('.')[0] + ",DC=" + pathAd.Split('.')[1];
                //chequear disponibilidad del servicio AD
                if (ActiveDirectoryConfig.CheckAdAvailability(pathAd))
                {
                    var dirSearcher = new DirectorySearcher(new DirectoryEntry(pathAd, userName, passWord));
                    dirSearcher.PropertiesToLoad.Add("ADsPath");
                    try
                    {
                        result = dirSearcher.FindOne() != null ? true : false;
                    }
                    catch
                    {
                        result = false;
                    }
                }
                else
                {
                    //informar a administrador del tipo de error
                    result = false;
                }
            }

            return result;
        }

        private static bool CheckAdAvailability(string domainName)
        {
            //TO DO Validate
            return true;
        }

    }
}