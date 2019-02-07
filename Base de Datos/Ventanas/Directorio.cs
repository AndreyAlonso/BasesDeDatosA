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
