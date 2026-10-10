using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Formulario_De_Regristro_Actividad04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Creación de Manejos deEventos y Controladores
            texnombres.TextChanged += ValidarNombre;
            texapellido.TextChanged += ValidarApellido;
            texedad.TextChanged += ValidarEdad;
            texestatura.TextChanged += ValidarEstatura;
            textelefono.Leave += ValidarTelefono;
        }

        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if  (!EstextoValido(textboxLocal.Text))
            {
                MessageBox.Show( "Escriba un nombre valido", "Error Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }

        private bool EstextoValido(string Valor)
        {
            return Regex.IsMatch(Valor, @"^[a-zA-Z\s]+$");
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsApellidoValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba un apellido valido", "Error Apellido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }

        private bool EsApellidoValido(string Valor)
        {
            return Regex.IsMatch(Valor, @"^[a-zA-Z\s]+$");
        }

        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!EsEnteroValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba una edad valida", "Error Edad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }

        private bool EsEnteroValido(String Valor)
        {
            int Edad;

            return int.TryParse(Valor, out Edad);
        }

        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!TelefonoValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba un telefono valido", "Error Telefono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.BackColor = System.Drawing.Color.IndianRed;
                
            }
        }
         private bool TelefonoValido(string Valor)
         {
            long telefono;
            return long.TryParse(Valor, out telefono)&&Valor.Length==10;
         }

        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textboxLocal = (TextBox)sender;
            if (!DecimalValido(textboxLocal.Text))
            {
                MessageBox.Show("Escriba una estatura valida", "Error Estatura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textboxLocal.Clear();
            }
        }

        private bool DecimalValido(string Valor)
        {
            decimal Estatura;
            return decimal.TryParse(Valor, out Estatura);
        }


        private void butguardar_Click(object sender, EventArgs e)
        {
            string nombres = texnombres.Text;
            string apellidos = texapellido.Text;
            string telefono = textelefono.Text;
            string edad = texedad.Text;
            string estatura = texestatura.Text;
            string genero = "" ;

            if (radMascu.Checked)
            {
                genero = "Masculino";
            }
            else if (radfeme.Checked)
            {
                genero = "Femenino";
            }
            else if (radotro.Checked)
            {
                genero = "Otro";
            }

            string mensaje = $"Nombres:{nombres}\n\r Apellidos:{apellidos}\n\r " +
                $"Genero:{genero}\n\r Edad:{edad}\n\r Telefono:{telefono}\n\r Estatura:{estatura}\n\r";
            MessageBox.Show(mensaje, "Registros de Usuaio", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //Ruta del almacenamieno de archivo TXT
            string rutaFile = "D:\\Practica04\\Formulario_De_Registro_Actividad04.txt";

            //Valor de verificaciòn de archivo existen
            bool ArchivoExiste = File.Exists(rutaFile);

            //Instansación de la Clase/objeto StreamWriter para escribir archivo TXT
            using (StreamWriter Escritor = new StreamWriter(rutaFile, true))

            
            {
                if (ArchivoExiste)
                {
                    Escritor.WriteLine();
                }
                Escritor.WriteLine(mensaje);
            }

            MessageBox.Show(mensaje, "Registro de -Usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            
        }

        private void butlimpiar_Click(object sender, EventArgs e)
        {
            texnombres.Clear();
            texapellido.Clear();
            texedad.Clear();
            texestatura.Clear();
            textelefono.Clear();
            radMascu.Checked = false;
            radfeme.Checked = false;
            radotro.Checked = false;
        }
    }
}
