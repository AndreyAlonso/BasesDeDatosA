﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Windows.Forms;
using Base_de_Datos.Ventanas;
using Base_de_Datos.Clases;
namespace Base_de_Datos
{
    public partial class Principal : Form
    {
        private bool max;
        private int posx, posy;
        private string directorioBD;
        private List<Tabla> tablas;
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
            deshabilitaAtributos();
            tablas = new List<Tabla>();
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
                break;
                case "modificaBD":
                    renombraBD();
                break;
                case "eliminaBD":
                    eliminarBD();
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
            Dispose();
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
                    tablas = new List<Tabla>();
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
                modificaBD.Enabled = true;
                eliminaBD.Enabled = true;
                
            }
            else
            {
                modificaBD.Enabled = false;
                eliminaBD.Enabled = false;
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
        public void eliminarBD()
        {
            Directorio mensaje = new Directorio(1);
            if(mensaje.ShowDialog() == DialogResult.OK)
            {
                Directory.Delete(directorioBD,true);
                nuevoProyecto();
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
            tablas = new List<Tabla>();
            List<string> tabla = new List<string>();
            string aux;
            tabla = Directory.GetFiles(ubicacion).ToList();
            nBD.Text = "BD : " + ubicacion.Split('\\').Last().ToString();
            directorioBD = ubicacion;
            if (tabla.Count == 0)
            {
                deshabilitaTablas();
                creaTabla.Enabled = true;
                deshabilitaAtributos();
            }
            else
            {
                habilitaTablas();
                creaAtributo.Enabled = true;
                foreach (string t in tabla)
                {
                    aux = t.Split(Convert.ToChar('\\')).Last();
                    listBox1.Items.Add(aux.Split('.').First());
                    tablas.Add(new Tabla(aux.Split('.').First()));
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
        private void opcionTabla(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.AccessibleName)
            {
                case "creaTabla":
                    creaTablas();
                break;
                case "modificaTabla":
                    modificaTablas();
                break;
                case "eliminaTabla":
                    eliminaTablas();
                break;
            }
        }

        public void creaTablas()
        {
            Directorio d = new Directorio(false);
            Tabla tabla;
            if(d.ShowDialog() == DialogResult.OK)
            {
                tabla = new Tabla(d.nombreBD);
                tabla.agregaTabla(directorioBD);
                cargaTablas(directorioBD);
                creaAtributo.Enabled = true;
            }

        }
        public void modificaTablas()
        {
            Tabla t = new Tabla(listBox1.Text);
            Directorio d = new Directorio(false);
            if (d.ShowDialog() == DialogResult.OK)
            {
                if(listBox1.Text != "" && d.nombreBD != null)
                {
                    t = new Tabla(listBox1.Text);
                    t.modificaNombre(directorioBD, d.nombreBD);
                    cargaTablas(directorioBD);
                }
               
            }

            
        }
        public void eliminaTablas()
        {
            Tabla t = new Tabla(listBox1.Text);
            t.eliminaTabla(directorioBD);
            cargaTablas(directorioBD);
        }

        private void seleccionaTabla(object sender, EventArgs e)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            cargaTabla(aux);
        }

        public void nuevoProyecto()
        {
            deshabilitaTablas();
            eliminaBD.Enabled = false;
            modificaBD.Enabled = false;
            listBox1.Items.Clear();
            nBD.Text = "BD : ";
        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.AccessibleName)
            {
                case "creaAtributo":
                    creaAtributos();
                break;
            }
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            cargaTabla(aux);
        }


        #region  ATRIBUTOS
        public void deshabilitaAtributos()
        {
            creaAtributo.Enabled = false;
            modificaAtributo.Enabled = false;
            eliminaAtributo.Enabled = false;
        }
        public Tabla buscaTabla()
        {
            foreach(Tabla t in tablas)
            {
                if (t.nombre == listBox1.Text)
                    return t;
            }
            return null;
        }
        public void creaAtributos()
        {
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            VentanaAtributo ventanaA = new VentanaAtributo(directorioBD, aux);
            if(listBox1.Text != "")
            {
                if (ventanaA.ShowDialog() == DialogResult.OK)
                {
                    guardaTabla(aux);  
                }
            }
            else
            {
                MessageBox.Show("Seleccione una Tabla");
            }
           
        }
        /// <summary>
        /// Guarda en el arhivo los datos de la tabla con sus atributos
        /// </summary>
        /// <param name="t"> Tabla actual</param>
        public void guardaTabla(Tabla t)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream;
            using ( stream = new FileStream(directorioBD + "\\" + t.nombre + t.extension, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                
                formatter.Serialize(stream, (Tabla)t);
                stream.Close();
            }
                
           
            
            
        }
        public Tabla abreTabla(Tabla t)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream;
                using (stream = new FileStream(directorioBD + "\\" + t.nombre + t.extension, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    t = (Tabla)formatter.Deserialize(stream);
                    stream.Close();
                }
                  
            }
            catch
            {

            }
            return t;
            
        }
        public void cargaTabla(Tabla t)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            foreach(Atributo atributo in t.atributos)
            {
                grid.Columns.Add(atributo.nombre, atributo.nombre);
            }
        }
        #endregion

    }
}
