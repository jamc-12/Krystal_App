using Clases;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Desktop.Formularios
{
    public partial class Frm_Usuario : Desktop.Formularios.Frm_Plantilla
    {
        public Frm_Usuario()
        {
            InitializeComponent();
            lista_perfil();
        }

        public void lista_perfil()
        {
            List<Perfil> list = new List<Perfil>();
            list = Perfil_Services.Lista_Perfiles().Where(a=>a.estado.Equals("Activo")).OrderBy(a=>a.descripcion).ToList();
            list.Insert(0, new Perfil { id_perfil = 0, descripcion = "" });

            cb_perfil.DataSource = list.ToList();
            cb_perfil.DisplayMember = "descripcion";
            cb_perfil.ValueMember = "id_perfil";
            cb_perfil.SelectedItem = null;
        }

        public void recibir_datos(Usuario obj)
        {
            txt_id.Text = obj.id_usuario.ToString();
            cb_perfil.SelectedValue = obj.id_perfil;
            txt_nombre.Text = obj.nombre;
            txt_usuario.Text = obj.usuario;
            txt_clave.Text = Generales.Desencriptar_Clave(obj.clave);
            cb_estado.Text = obj.estado;
        }

        public void insert_or_update()
        {
            Usuario obj = new Usuario();
            obj.id_perfil  = int.Parse(cb_perfil.SelectedValue.ToString());
            obj.nombre = txt_nombre.Text.Trim();
            obj.usuario  = txt_usuario.Text.Trim();
            obj.clave = Generales.Encriptar_Clave(txt_clave.Text.Trim());
            obj.estado = cb_estado.Text == "Activo" ? "A" : "I";
            obj.adicionado_por = "JMENA";
            obj.fecha_adicion = DateTime.Now;
            string mensaje = "";

            if (string.IsNullOrEmpty(txt_id.Text))
            {
                Usuario_Services.Insertar_Usuario(obj);
                mensaje = "Datos guardados correctamente";
            }
            else
            {
                obj.id_usuario = int.Parse(txt_id.Text);
                Usuario_Services.Update_Usuario(obj);
                mensaje = "Datos actualizados correctamente";
            }

            Generales.Mensaje_Informacion(mensaje);
            this.Close();
        }

        private void Frm_Usuario_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                lbl_titulo.Text = "Crear Usuario";
                cb_estado.Text = "Activo";
                cb_estado.Enabled = false;
            }
            else
            {
                lbl_titulo.Text = "Editar Usuario";
                cb_estado.Enabled = true;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb_perfil.Text))
            {
                Generales.Mensaje_Informacion("El perfil es obligatorio");
                return;
            }
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                Generales.Mensaje_Informacion("El nombre es obligatorio");
                return;
            }
            if (string.IsNullOrEmpty(txt_usuario.Text))
            {
                Generales.Mensaje_Informacion("El usuario es obligatorio");
                return;
            }
            if (string.IsNullOrEmpty(txt_clave.Text))
            {
                Generales.Mensaje_Informacion("La clave es obligatorio");
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
