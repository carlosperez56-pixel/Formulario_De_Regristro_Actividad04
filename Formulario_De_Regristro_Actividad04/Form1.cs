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

namespace Formulario_De_Regristro_Actividad04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
    }
}
