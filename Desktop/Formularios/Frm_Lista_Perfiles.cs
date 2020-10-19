using Clases;
using Servicios;
using System;
using System.Data;
using System.Linq;
using static Desktop.Generales;

namespace Desktop.Formularios
{
    public partial class Frm_Lista_Perfiles : Desktop.Formularios.Frm_Plantilla
    {
        public Frm_Lista_Perfiles()
        {
            InitializeComponent();
        }

        public void listado()
        {
         dg.DataSource = Perfil_Services.Lista_Perfiles().ToList();
        }

        private void Frm_Lista_Perfiles_Load(object sender, EventArgs e)
        {
            listado();
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            var form = WindowsManagement.GetWindow<Frm_Perfil>();
            if (form == null) form = new Frm_Perfil();
            form.ShowDialog();
            listado();
        }

        public void pasar_datos()
        {
            //pasar los datos al formulario hijo
            if (dg.Rows.Count == 0)
            {
                return;
            }
            Perfil obj = new Perfil();
            Perfil obj_sel = (Perfil)dg.CurrentRow.DataBoundItem;
            Frm_Perfil frm = new Frm_Perfil();

            obj.descripcion = obj_sel.descripcion;
            obj.estado = obj_sel.estado;
            obj.id_perfil = obj_sel.id_perfil;

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
            dg.DataSource = Perfil_Services.Lista_Perfiles().Where(a => a.descripcion.Contains(txt_buscar.Text)).ToList();
        }
    }
}
