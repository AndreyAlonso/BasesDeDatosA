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
        private bool max;
        private int posx, posy;
        private string directorioBD;
        public Principal()
        {
            InitializeComponent();
            posx = 0;
            posy = 0;
            eliminaBD.Enabled = false;
            modificaBD.Enabled = false;
            max = false;
            directorioBD = "";
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
                break;
                case "abrir":
                    abreBD();
                    eliminaBD.Enabled = true;
                    modificaBD.Enabled = true;
                break;
                case "modificaBD":
                    renombraBD();
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
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(folderBrowserDialog1.SelectedPath + "\\" + nombreBD);
                    directorioBD = folderBrowserDialog1.SelectedPath + "\\" + nombreBD;
                    nBD.Text += nombreBD;
                    creaTabla.Enabled = true;
                    listBox1.Items.Clear();
                    eliminaBD.Enabled = true;
                    modificaBD.Enabled = true;
                }
            }
            else 
            {
                eliminaBD.Enabled = false;
                modificaBD.Enabled = false;
            }
        }
        public void abreBD()
        {
            nBD.Text = "BD :";
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();

                cargaTablas(folderBrowserDialog1.SelectedPath);
                directorioBD = folderBrowserDialog1.SelectedPath;
            }
            
        }
        public void renombraBD()
        {
            string nuevo;
            string n;
            string dirActual;
            Directorio directorio = new Directorio(true);
            if(directorio.ShowDialog() == DialogResult.OK)
            {
                nuevo = directorio.nombreBD;
                n = directorioBD.Split('\\').Last().ToString();
                dirActual = directorioBD.Replace(label1.Text, "");
                dirActual = directorioBD.Replace(n, "");
                Directory.Move(directorioBD, dirActual + nuevo);
                nBD.Text = "BD : " + nuevo;
                cargaTablas(dirActual + nuevo);
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

        public void cargaTablas(string ubicacion)
        {
            listBox1.Items.Clear();
            List<string> tablas = new List<string>();
            string aux;
            tablas = Directory.GetFiles(ubicacion).ToList();
            nBD.Text = "BD : " + ubicacion.Split('\\').Last().ToString();
            directorioBD = ubicacion;
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
        private void Principal_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = ClientSize.Width+10;
            pictureBox3.Location = new Point(ClientSize.Width-50, pictureBox3.Location.Y);
            pictureBox4.Location = new Point(ClientSize.Width - pictureBox4.Width, pictureBox4.Location.Y);
            pictureBox4.Height = ClientSize.Height;
            pictureBox6.Height = ClientSize.Height;
            pictureBox5.Location = new Point(0, ClientSize.Height - pictureBox5.Height);
            pictureBox5.Width = ClientSize.Width;
            maximiza.Location = new Point(pictureBox3.Location.X - 47, maximiza.Location.Y);
            
        }

        private void maximizar(object sender, EventArgs e)
        {
            if(max == false)
            {
                WindowState = FormWindowState.Maximized;
                maximiza.Image = System.Drawing.Image.FromFile("maximizar2.png");
                max = true;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                maximiza.Image = Image.FromFile("maximizar.png");
                StartPosition = FormStartPosition.CenterScreen;
                max = false;
            }
            
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
