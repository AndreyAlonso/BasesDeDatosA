using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base_de_Datos.Ventanas
{
    public partial class Directorio : Form
    {
        private int posx, posy;
        public string nombreBD { get; set; }
        

        public Directorio()
        {
            InitializeComponent();
            posx = 0;
            posy = 0;
        }
        public Directorio(bool b)
        {
            InitializeComponent();
            if (b == true)
            {
                label1.Text = "Escribe el nuevo nombre de la base de datos";
            }
        }
        public Directorio(int n)
        {
            InitializeComponent();
            if(n == 1)
            {
                textBox1.Visible = false;
                label1.Text = "Esta seguro que desea eliminar la Base de Datos? \r\n----------------------- \r\nTODOS los datos se borraran";
                label1.Location = new Point(label1.Location.X-50, ClientSize.Height / 2 - 5);
                button1.Text = "Eliminar";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nombreBD = textBox1.Text;
        }
        #region PROPIEDADES VENTANA
        private void mueveVentana(object sender, MouseEventArgs e)
        {
           if(e.Button != MouseButtons.Left)
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
        #endregion
    }

}
