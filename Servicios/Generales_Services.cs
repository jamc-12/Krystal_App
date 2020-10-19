using System.Configuration;

namespace Servicios
{
    public class Generales_Services
    {
        public static string cadena_conexion = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
    }
}
