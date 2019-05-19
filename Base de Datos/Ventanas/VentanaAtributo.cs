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
        public Atributo aModificado { get; set; }
        public int pos { get; set; }
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
            comboBox1.Visible = false;
            label6.Visible = false;
            button2.Text = "Agregar";
        }
        public VentanaAtributo(string bd, Tabla tab, List<Tabla> tablas, string s)
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
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboClave.Enabled = false;
            comboTipo.Enabled = false;
            comboPrimarias.Enabled = false;
            this.tablas = tablas;
            button2.Text = "MODIFICAR";
            comboBox1.Visible = true;
            label6.Visible = true;
            foreach (Atributo a in tab.atributos)
                comboBox1.Items.Add(a.nombre);
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

        /// <summary>
        /// Boton AGREGAR
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agregaAtributo(object sender, EventArgs e)
        {
            int contador = 0;
            if (button2.Text == "Agregar")
            {
                if (textBox1.Text == string.Empty || textBox2.Text == string.Empty) //Verificar si no se han completado los campos
                    MessageBox.Show("Complete todos los campos");
                else //Los campos están completos 
                {
                    foreach (Atributo t in tabla.atributos)
                    {
                        if (t.indice == "Clave Primaria" && t.nombre != textBox1.Text) //Verifica cuantas llaves primarias tiene la tabla
                            contador++;
                    }
                    // Si no tiene clave Primaria
                    if (contador == 0)
                    {
                        atributo = new Atributo();
                        atributo.nombre = textBox1.Text;
                        atributo.tipo = Convert.ToChar(comboTipo.Text);
                        atributo.tam = Convert.ToInt32(textBox2.Text);
                        atributo.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            atributo.foranea = comboPrimarias.Text;
                        else
                            atributo.foranea = "NULL";

                        tabla.atributos.Add(atributo);
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    // Ya existe clave primaria y se piensa agregar otra
                    else if (contador > 0 && comboClave.Text == "Clave Primaria" && label6.Visible == false)
                    {
                        MessageBox.Show("La tabla ya contiene una clave primaria");
                    }
                    // Ya existe PK y se va a modificar la misma
                    else if (label6.Visible == true && contador > 0 && comboClave.Text == "Clave Primaria")
                    {
                        atributo = new Atributo();
                        atributo.nombre = textBox1.Text;
                        atributo.tipo = Convert.ToChar(comboTipo.Text);
                        atributo.tam = Convert.ToInt32(textBox2.Text);
                        atributo.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            atributo.foranea = comboPrimarias.Text;
                        else
                            atributo.foranea = "NULL";

                        tabla.atributos.Add(atributo);
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        atributo = new Atributo();
                        atributo.nombre = textBox1.Text;
                        atributo.tipo = Convert.ToChar(comboTipo.Text);
                        atributo.tam = Convert.ToInt32(textBox2.Text);
                        atributo.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            atributo.foranea = comboPrimarias.Text;
                        else
                            atributo.foranea = "NULL";

                        tabla.atributos.Add(atributo);
                        textBox1.Clear();
                        textBox2.Clear();
                    }

                }
            }
            else if (button2.Text == "MODIFICAR")
            {
                if (textBox1.Text == string.Empty || textBox2.Text == string.Empty) //Verificar si no se han completado los campos
                    MessageBox.Show("Complete todos los campos");
                else //Los campos están completos 
                {
                    foreach (Atributo t in tabla.atributos)
                    {
                        if (t.indice == "Clave Primaria" && t.nombre != textBox1.Text) //Verifica cuantas llaves primarias tiene la tabla
                            contador++;
                    }
                    // Si no tiene clave Primaria
                    if (contador == 0)
                    {
                        aModificado = new Atributo();
                        aModificado.nombre = textBox1.Text;
                        aModificado.tipo = Convert.ToChar(comboTipo.Text);
                        aModificado.tam = Convert.ToInt32(textBox2.Text);
                        aModificado.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            aModificado.foranea = comboPrimarias.Text;
                        else
                            aModificado.foranea = "NULL";
                        Atributo a = tabla.atributos.Find(x => x.nombre.Equals(aModificado.nombre));
                        if (a != null)
                        {
                            tabla.atributos.Remove(a);
                            tabla.atributos.Add(aModificado);
                        }
                            
                        
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    // Ya existe clave primaria y se piensa agregar otra
                    else if (contador > 0 && comboClave.Text == "Clave Primaria" && label6.Visible == true)
                    {
                        MessageBox.Show("La tabla ya contiene una clave primaria");
                    }
                    // Ya existe PK y se va a modificar la misma
                    else if (label6.Visible == true && contador > 0 && comboClave.Text == "Clave Primaria")
                    {

                        aModificado = new Atributo();
                        aModificado.nombre = textBox1.Text;
                        aModificado.tipo = Convert.ToChar(comboTipo.Text);
                        aModificado.tam = Convert.ToInt32(textBox2.Text);
                        aModificado.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            aModificado.foranea = comboPrimarias.Text;
                        else
                            aModificado.foranea = "NULL";

                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        aModificado = new Atributo();
                        aModificado.nombre = textBox1.Text;
                        aModificado.tipo = Convert.ToChar(comboTipo.Text);
                        aModificado.tam = Convert.ToInt32(textBox2.Text);
                        aModificado.indice = comboClave.Text;
                        if (comboPrimarias.Enabled == true)
                            aModificado.foranea = comboPrimarias.Text;
                        else
                            aModificado.foranea = "NULL";
                        textBox1.Clear();
                        textBox2.Clear();
                    }

                }
            }
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

        private void VentanaAtributo_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Atributo a in tabla.atributos)
            {
                if(comboBox1.Text == a.nombre)
                {
                    pos = comboBox1.SelectedIndex;
                    textBox1.Text = a.nombre;
                    comboTipo.Text = a.tipo.ToString();
                    textBox2.Text = a.tam.ToString();
                    comboClave.Text = a.indice;
                    comboPrimarias.Text = a.foranea;

                    textBox1.Enabled = true;
                    comboClave.Enabled = true;
                    comboTipo.Enabled = true;
                    comboPrimarias.Enabled = true;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            /*
            int contador = 0;
            if(tabla.atributos.Count == 0 )
            {
                MessageBox.Show("Se necesita AGREGAR atributos "); 
            }
            
            else if (button2.Visible == false)
            {
                foreach (Atributo t in tabla.atributos)
                {
                    if (t.indice == "Clave Primaria")
                        contador++;

                }
                if (contador == 1 && comboClave.Text == "Clave Primaria")
                    MessageBox.Show("La tabla ya contiene una clave primaria");
                else
                {
                    aModificado = new Atributo();
                    aModificado.nombre = textBox1.Text;
                    aModificado.tipo = Convert.ToChar(comboTipo.Text);
                    aModificado.tam = Convert.ToInt32(textBox2.Text);
                    aModificado.indice = comboClave.Text;
                    if (comboPrimarias.Enabled == true)
                        aModificado.foranea = comboPrimarias.Text;
                    else
                        aModificado.foranea = "NULL";
                }
                
            }
            */
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
