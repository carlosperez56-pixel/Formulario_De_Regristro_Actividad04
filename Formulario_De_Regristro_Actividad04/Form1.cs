using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Libreria para lectura de archivos TXT
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; //Libreria para evaluaciones de estructura de texto
using MySql.Data.MySqlClient; //Libreria para la conexion y uso de MySQL




namespace Formulario_De_Regristro_Actividad04
{
    public partial class Form1 : Form
    {
        string SQLConection = "Server=127.0.0.1;Port=3306;Database=avanzada;Uid=root;Pwd=Mycarlos-07;";
       
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

        private void insertarRegistros(string nombre, string apellidos, int edad, decimal estatura, string telefono, string genero)
        {
            using (MySqlConnection conectar = new MySqlConnection(SQLConection))
            {
                conectar.Open();

                string insertQuery = "INSERT INTO registros (nombres, apellidos, telefono, estatura, edad, genero) " +
                                     "VALUES (@nombres, @apellidos, @telefono, @estatura, @edad, @genero)";

                using (MySqlCommand comando = new MySqlCommand(insertQuery, conectar))
                {
                    comando.Parameters.AddWithValue("@nombres", nombre);
                    comando.Parameters.AddWithValue("@apellidos", apellidos);
                    comando.Parameters.AddWithValue("@telefono", telefono);
                    comando.Parameters.AddWithValue("@estatura", estatura);
                    comando.Parameters.AddWithValue("@edad", edad);
                    comando.Parameters.AddWithValue("@genero", genero);
                    comando.ExecuteNonQuery();
                }
            }
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


        private void butguardar_click(object sender, EventArgs e)
        {
            string nombres = texnombres.Text;
            string apellidos = texapellido.Text;
            string telefono = textelefono.Text;
            string edad = texedad.Text;
            string estatura = texestatura.Text;
            string genero = "";

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

            // Validar que todo esté correcto antes de guardar
            if (!EsEnteroValido(edad) || !DecimalValido(estatura) ||
                !TelefonoValido(telefono) || !EstextoValido(nombres) ||
                !EsApellidoValido(apellidos) || genero == "")
            {
                MessageBox.Show("Por favor, ingrese datos válidos en todos los campos.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string datos = $"Nombres: {nombres}\r\nApellidos: {apellidos}\r\n" +
                           $"Género: {genero}\r\nEdad: {edad}\r\n" +
                           $"Teléfono: {telefono}\r\nEstatura: {estatura}\r\n";

            try
            {
                // 1) Guardar en el archivo TXT
                string rutaArchivo = @"D:\Practica04\Formulario_De_Registro_Actividad04.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));
                File.AppendAllText(rutaArchivo, datos + Environment.NewLine);

                // 2) Guardar en la base de datos
                insertarRegistros(nombres, apellidos, int.Parse(edad),
                                  decimal.Parse(estatura), telefono, genero);

                MessageBox.Show("Datos guardados en TXT y en la Base de Datos:\n\n" + datos,
                                "Registro de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
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
