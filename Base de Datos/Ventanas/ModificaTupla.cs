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
    public partial class ModificaTupla : Form
    {
        private int posx, posy;
        public ModificaTupla(DataGridView r, string row)
        {
            InitializeComponent();
            cargaGrid(r, row);
            registro.ForeColor = Color.Black;

        }
        public void cargaGrid(DataGridView r,string row)
        {
            int i, j;
            for(j = 0; j < r.Columns.Count; j++)
            {
                registro.Columns.Add(r.Columns[j].HeaderText, r.Columns[j].HeaderText);
            }
            registro.Rows.Add(row.Split(','));
           
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
}
