using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base_de_Datos.Clases;
namespace Base_de_Datos.Ventanas
{
    public partial class EliminarAtributo : Form
    {
        private int posx, posy;
        public Tabla tabla;
        private List<Tabla> tablas;
        public EliminarAtributo(Tabla tabla, List<Tabla> tablas)
        {
            InitializeComponent();
            this.tabla = tabla;
            this.tablas = tablas;
            cargaAtributos();
        }
        private void mueveVentana(object sender, MouseEventArgs e)
        {
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
        }

        private void eliminarAtributo(object sender, EventArgs e)
        {
            Atributo atributo = tabla.atributos.Find(x => x.nombre.Equals(listBox1.Text));
            if (listBox1.Text != "")
            {
                //Busca en la lista de atributos y elimina el atributo seleccionado en el listbox
                if(atributo.indice == "Clave Primaria" && tabla.tuplas.Count > 0)
                {
                    MessageBox.Show("No se puede eliminar el atributo " + atributo.nombre + "\nporque es clave primaria y tiene registros");
                }
                else
                {
                    tabla.atributos.Remove(tabla.atributos.Find(x => x.nombre.Equals(listBox1.Text)));
                    cargaAtributos();
                }
               
            }
        }
        public void cargaAtributos()
        {
            listBox1.Items.Clear();
            foreach(Atributo atri in tabla.atributos)
            {
                listBox1.Items.Add(atri.nombre);
            }
            validaIntegridadReferencial(tabla);
        }
        /// <summary>
        /// Recorre las tablas y verifica si un atributo es foraneo de un atributo que podemos llegar a modificar
        /// entonces se omite de la lista
        /// </summary>
        public void validaIntegridadReferencial(Tabla tab)
        {
            string elimina = "";
            foreach (Tabla t in tablas)
            {
                if (t != tab)
                {
                    foreach (Atributo a in t.atributos)
                    {
                        if (tab.atributos.Find(x => x.nombre.Equals(a.foranea)) != null)
                        {
                            elimina = a.foranea;
                            break;
                        }
                    }
                }

            }
            if (elimina != string.Empty)
            {
                listBox1.Items.Remove(elimina);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
