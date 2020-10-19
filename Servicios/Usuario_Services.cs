using Clases;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Servicios
{
    public class Usuario_Services : Generales_Services
    {
        public static List<Usuario> Lista_Usuarios()
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "select");

                    var query = db.Query<Usuario>("SP_Usuario", parametro, commandType: CommandType.StoredProcedure);

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Insertar_Usuario(Usuario obj)
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "insert");
                    parametro.Add("@id_perfil", obj.id_perfil);
                    parametro.Add("@nombre", obj.nombre);
                    parametro.Add("@usuario", obj.usuario);
                    parametro.Add("@clave", obj.clave);
                    parametro.Add("@estado", obj.estado);
                    parametro.Add("@adicionado_por", obj.adicionado_por);
                    parametro.Add("@fecha_adicion", obj.fecha_adicion);

                    db.Execute("SP_Usuario", parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update_Usuario(Usuario obj)
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "update");
                    parametro.Add("@id_usuario", obj.id_usuario);
                    parametro.Add("@id_perfil", obj.id_perfil);
                    parametro.Add("@nombre", obj.nombre);
                    parametro.Add("@usuario", obj.usuario);
                    parametro.Add("@clave", obj.clave);
                    parametro.Add("@estado", obj.estado);

                    db.Execute("SP_Usuario", parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update_Clave(Usuario obj)
        {
            try
            {
                using (var db = new SqlConnection(cadena_conexion))
                {
                    db.Open();

                    var parametro = new DynamicParameters();
                    parametro.Add("@accion", "update_clave");
                    parametro.Add("@id_usuario", obj.id_usuario);
                    parametro.Add("@clave", obj.clave);

                    db.Execute("SP_Usuario", parametro, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
