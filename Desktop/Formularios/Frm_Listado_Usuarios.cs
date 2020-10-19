using Clases;
using Servicios;
using System;
using System.Data;
using System.Linq;
using static Desktop.Generales;

namespace Desktop.Formularios
{
    public partial class Frm_Listado_Usuarios : Desktop.Formularios.Frm_Plantilla
    {
        public Frm_Listado_Usuarios()
        {
            InitializeComponent();
        }

        public void listado()
        {
            dg.DataSource = Usuario_Services.Lista_Usuarios().ToList();
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            var form = WindowsManagement.GetWindow<Frm_Usuario>();
            if (form == null) form = new Frm_Usuario();
            form.ShowDialog();
            listado();
        }

        private void Frm_Listado_Usuarios_Load(object sender, EventArgs e)
        {
            listado();
        }

        public void pasar_datos()
        {
            if (dg.Rows.Count == 0)
            {
                return;
            }
            Usuario obj = new Usuario();
            Usuario obj_sel = (Usuario)dg.CurrentRow.DataBoundItem;
            Frm_Usuario frm = new Frm_Usuario();

            obj.id_usuario = obj_sel.id_usuario;
            obj.id_perfil = obj_sel.id_perfil;
            obj.nombre = obj_sel.nombre;
            obj.usuario = obj_sel.usuario ;
            obj.clave = obj_sel.clave;
            obj.estado = obj_sel.estado;
            
            frm.recibir_datos(obj);
            frm.ShowDialog();
            listado();
        }

        private void dg_DoubleClick(object sender, EventArgs e)
        {
            pasar_datos();
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            dg.DataSource = Usuario_Services.Lista_Usuarios().Where(a => a.nombre.Contains(txt_buscar.Text) ||
                                                                         a.usuario.Contains(txt_buscar.Text) ||
                                                                         a.perfil.Contains(txt_buscar.Text)).ToList();
        }
    }
}
