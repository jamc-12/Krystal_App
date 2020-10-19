using System;

namespace Clases
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public int id_perfil { get; set; }
        public string perfil { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string estado { get; set; }
        public string adicionado_por { get; set; }
        public DateTime fecha_adicion { get; set; }
    }
}
