using Clases;
using Servicios;
using System;

namespace Desktop.Formularios
{
    public partial class Frm_Perfil : Desktop.Formularios.Frm_Plantilla
    {
        public Frm_Perfil()
        {
            InitializeComponent();
        }

        public void recibir_datos(Perfil obj)
        {
            txt_id.Text = obj.id_perfil.ToString();
            txt_descripcion.Text = obj.descripcion;
            cb_estado.Text = obj.estado;
        }

        public void insert_or_update()
        {
            Perfil obj = new Perfil();
            obj.descripcion = txt_descripcion.Text.Trim();
            obj.estado = cb_estado.Text == "Activo" ? "A" : "I";
            string mensaje = "";

            if(string.IsNullOrEmpty(txt_id.Text))
            {
                Perfil_Services.Insertar_Perfil(obj);
                mensaje = "Datos guardados correctamente";
            }
            else
            {
                obj.id_perfil = int.Parse(txt_id.Text);
                Perfil_Services.Update_Perfil(obj);
                mensaje = "Datos actualizados correctamente";
            }

            Generales.Mensaje_Informacion(mensaje);
            this.Close();
        }

        private void Frm_Perfil_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                lbl_titulo.Text = "Crear Perfil";
                cb_estado.Text = "Activo";
                cb_estado.Enabled = false;
            }
            else
            {
                lbl_titulo.Text = "Editar Perfil";
                cb_estado.Enabled = true;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_descripcion.Text))
            {
                Generales.Mensaje_Informacion("La descripción es obligatoria");
                return;
            }
            if (string.IsNullOrEmpty(cb_estado.Text))
            {
                Generales.Mensaje_Informacion("El estado es obligatorio");
                return;
            }

            insert_or_update();
        }
    }
}
