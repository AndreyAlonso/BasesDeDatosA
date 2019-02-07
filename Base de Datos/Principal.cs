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
using Base_de_Datos.Ventanas;
namespace Base_de_Datos
{
    public partial class Principal : Form
    {
        
        int posx, posy;
        public Principal()
        {
            InitializeComponent();
            posx = 0;
            posy = 0;
            eliminaBD.Enabled = false;
            deshabilitaTablas();
        }
        #region PROPIEDADES VENTANA
        private void label1_MouseMove(object sender, MouseEventArgs e)
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.AccessibleName)
            {
                case "nueva":
                    creaBD();
                    eliminaBD.Enabled = true;
                break;
                case "abrir":
                    abreBD();
                    eliminaBD.Enabled = true;
                break;
            }
        }


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

        private void salir(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
        #region Abrir/Cerrar Base de Datos
        public void creaBD()
        {
            nBD.Text = "BD :";
            Directorio ventana = new Directorio();
            string nombreBD;
            if (ventana.ShowDialog() == DialogResult.OK)
            {
                nombreBD = ventana.nombreBD;
                if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\" + nombreBD);
                    nBD.Text += nombreBD;
                    
                }

            }
        }
        public void abreBD()
        {
            nBD.Text = "BD :";
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> tablas = new List<string>();
                string aux;
                tablas = Directory.GetFiles(folderBrowserDialog1.SelectedPath).ToList();
                nBD.Text += folderBrowserDialog1.SelectedPath.Split('\\').Last().ToString();
                if (tablas.Count == 0)
                    deshabilitaTablas();
                else
                {
                    habilitaTablas();
                    foreach (string t in tablas)
                    {
                        aux = t.Split(Convert.ToChar('\\')).Last();
                        listBox1.Items.Add(aux.Split('.').First());
                    }
                }
                
                    
            }
            
        }
        #endregion
        #region TABLAS
        public void deshabilitaTablas()
        {
            creaTabla.Enabled = false;
            modificaTabla.Enabled = false;
            eliminaTabla.Enabled = false;
        }
        public void habilitaTablas()
        {
            creaTabla.Enabled = true;
            modificaTabla.Enabled = true;
            eliminaTabla.Enabled = true;
        }
        #endregion
    }
}
