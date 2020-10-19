using Clases;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios
{
    public class Perfil_Services : Generales_Services
    {
        public static List<Perfil> Lista_Perfiles()
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "select");

                    var query = db.Query<Perfil>("SP_Perfil", parametro, commandType: CommandType.StoredProcedure);

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Insertar_Perfil(Perfil obj)
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "insert");
                    parametro.Add("@descripcion", obj.descripcion);
                    parametro.Add("@estado", obj.estado);

                    db.Execute("SP_Perfil", parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update_Perfil(Perfil obj)
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "update");
                    parametro.Add("@id_perfil", obj.id_perfil);
                    parametro.Add("@descripcion", obj.descripcion);
                    parametro.Add("@estado", obj.estado);

                    db.Execute("SP_Perfil", parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
