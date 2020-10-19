using Microsoft.Win32;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Desktop
{
    public class Generales
    {
        public static class WindowsManagement
        {
            public static T GetWindow<T>() where T : Form
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form is T) return (T)((Form)form);
                }
                return default(T);
            }
        }

        public static void Cambiar_Formato_Fecha_Hora()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            //punto decimal y coma de formato numerico
            key.SetValue("sDecimal", ".");
            key.SetValue("sThousand", ",");

            //punto decimal y coma de formato moneda
            key.SetValue("sMonDecimalSep", ".");
            key.SetValue("sMonThousandSep", ",");

            //formato de hora 24
            key.SetValue("sTime", "HH:mm:ss");
            key.SetValue("sShortTime", "HH:mm");
            key.SetValue("sShortTimeFormat", "HH:mm");
            key.SetValue("sTimeFormat", "HH:mm:ss");

            //formaro de fecha corta
            key.SetValue("sShortDate", "dd/MM/yyyy");
            key.SetValue("sLongDate", "dd/MM/yyyy");
        }
        public static bool Validar_Email(string correo)
        {
            String Expresion;
            Expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(correo, Expresion))
            {
                if (Regex.Replace(correo, Expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static String Encriptar_Clave(string Clave)
        {
            TripleDESCryptoServiceProvider Des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider Hashmd5 = new MD5CryptoServiceProvider();
            string MyKey = "MyKey2012";
            String Encriptar = "";

            if ((Clave.Trim() == ""))
            {
                Encriptar = "";
            }
            else
            {
                Des.Key = Hashmd5.ComputeHash(new UnicodeEncoding().GetBytes(MyKey));
                Des.Mode = CipherMode.ECB;
                ICryptoTransform encrypt = Des.CreateEncryptor();
                byte[] buff = UnicodeEncoding.ASCII.GetBytes(Clave);
                Encriptar = Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length));
            }
            return Encriptar;
        }

        public static String Desencriptar_Clave(string texto)
        {
            TripleDESCryptoServiceProvider Des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider Hashmd5 = new MD5CryptoServiceProvider();
            string MyKey = "MyKey2012";
            String Desencriptar = "";
            if ((texto.Trim() == ""))
            {
                Desencriptar = "";
            }
            else
            {
                Des.Key = Hashmd5.ComputeHash(new UnicodeEncoding().GetBytes(MyKey));
                Des.Mode = CipherMode.ECB;
                ICryptoTransform desencrypta = Des.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(texto);
                Desencriptar = UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length));
                buff.GetLength(0);
            }

            return Desencriptar;
        }

        public static class Variables_Globales
        {
            public static string usuario { get; set; }
            public static string si { get; set; }
            public static int id_cliente { get; set; }
            public static string nombre_cliente { get; set; }
            public static string opciones_prestamo { get; set; }

        }

        public static bool Validad_Decimal(TextBox txt)
        {
            double price;
            bool isDouble = Double.TryParse(txt.Text, out price);
            return isDouble;
        }

        public static bool Validad_Decimal(string txt)
        {
            double price;
            bool isDouble = Double.TryParse(txt, out price);
            return isDouble;
        }

        public static void Mensaje_Informacion(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Mensaje_Error(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Error Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Mensaje_Confirmacion(string Mensaje)
        {
            DialogResult Resultado = MessageBox.Show(Mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return Resultado;
        }

        public static void Permitir_Solo_Numero_y_Punto(object sender, KeyPressEventArgs e, TextBox text)
        {
            if (((e.KeyChar) < 48) && ((e.KeyChar) != 8) || ((e.KeyChar) > 57))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
                //si ya hay un punto no permite un nuevo ingreso de este
                if (text.Text.Contains("."))
                    e.Handled = true;
                else
                    e.Handled = false;
        }

        public static void Permitir_Solo_Numero(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) < 48) && ((e.KeyChar) != 8) || ((e.KeyChar) > 57))
            {
                e.Handled = true;
            }
        }

        public static string Dia_Semana(int y, int m, int d)
        {
            //Metodo de variacion de Kraitchik del metodo de Gauss
            int yy = ((y % 100) / 4 + (y % 100)) % 7;
            int[] mm = { 1, 4, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5 };
            int[] cc = { 0, 5, 3, 1 };
            int c = (y / 100) % 4;

            int w = (d + mm[m - 1] + cc[c] + yy) % 7;
            string[] day = { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };

            return day[w];
        }
    }
}
