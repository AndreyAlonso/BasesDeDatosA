using Base_de_Datos.Clases;
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

namespace Base_de_Datos.Ventanas
{
    public partial class VentanaAtributo : Form
    {
        private int posx,posy;
        private Tabla tabla;
        private FileStream archivo;
        private string directorio;
        private Atributo atributo;
        private List<Tabla> tablas;
        public VentanaAtributo(string bd,Tabla tab, List<Tabla> tablas)
        {
            
            InitializeComponent();
            comboTipo.Items.Add("E");
            comboTipo.Items.Add("F");
            comboTipo.Items.Add("C");
            comboClave.Items.Add("Sin clave");
            comboClave.Items.Add("Clave Primaria");
            comboClave.Items.Add("Clave Foranea");
            tabla = tab;
            directorio = bd;
            comboPrimarias.Enabled = false;
            this.tablas = tablas;
        }

        private void mueveVentana(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posx = e.X;
                posy = e.Y;
            }
            else
            {
                Left = Left + (e.X - posx);
                Top = Top + (e.Y - posy);
            }
        }

        private void agregaAtributo(object sender, EventArgs e)
        {
            //archivo = new FileStream(directorio + "\\" + tabla.nombre + tabla.extension, FileMode.Open);
            atributo = new Atributo();
            atributo.nombre = textBox1.Text;
            atributo.tipo = Convert.ToChar(comboTipo.Text);
            atributo.tam = Convert.ToInt32(textBox2.Text);
            atributo.indice = comboClave.Text;
            if (comboPrimarias.Enabled == true)
                atributo.foranea = comboPrimarias.Text;
            else
                atributo.foranea = "";
            tabla.atributos.Add(atributo);
            textBox1.Clear();
            textBox2.Clear();
        }

        /// <summary>
        /// Al seleccionar como clave "Clave Foranea" entonces se habilita 
        /// el combobox que muestra las claves primaras de la Base de Datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboClave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboClave.Text == "Clave Foranea")
            {
                comboPrimarias.Enabled = true;
                cargaClavesPrimarias();
            }
            else
            {
                comboPrimarias.Enabled = false;
            }
        }
        public void cargaClavesPrimarias()
        {
            comboPrimarias.Items.Clear();
            foreach(Tabla t in tablas)
            {
                foreach(Atributo a in t.atributos)
                {
                    if(a.indice == "Clave Primaria")
                    {
                        comboPrimarias.Items.Add(a.nombre);
                    }
                }
            }
        }
        public Tabla dameTabla()
        {
            return tabla;
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTipo.Text == "E" || comboTipo.Text == "F")
            {
                textBox2.Text = 4.ToString();
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Clear();
                textBox2.Enabled = true;
            }
                
        }

        /// <summary>
        /// Busca en la carpeta de la Base de Datos 
        /// el archivo donde se insertara el atributo
        /// </summary>
        /// <returns>La dirección de la tabla</returns>
        public void buscaTabla()
        { 
            archivo = new FileStream(directorio + "\\" + tabla.nombre, FileMode.Open);
        }

    }
}
