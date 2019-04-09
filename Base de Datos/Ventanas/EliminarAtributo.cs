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
        public EliminarAtributo(Tabla tabla)
        {
            InitializeComponent();
            this.tabla = tabla;
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
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
