using System;
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
        DataTable dt;
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
            grid.ForeColor = Color.Black;
            registro.ForeColor = Color.Black;
            creaTupla.Enabled = false;
            modificaTupla.Enabled = false;
            eliminaTupla.Enabled = false;
           
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
                case "eliminaAtributo":
                    eliminarAtributos();
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
            cargaTabla(aux);
            aux = abreTabla(aux);
            cargaBD();
            VentanaAtributo ventanaA = new VentanaAtributo(directorioBD, aux, tablas);
            if(listBox1.Text != "")
            {
                if (ventanaA.ShowDialog() == DialogResult.OK)
                {
                    guardaTabla(ventanaA.dameTabla());  
                }
            }
            else
            {
                MessageBox.Show("Seleccione una Tabla");
            }
           
        }
        public void eliminarAtributos()
        {
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            EliminarAtributo elimina = new EliminarAtributo(aux);
            if(elimina.ShowDialog() == DialogResult.OK)
            {
                aux = elimina.tabla;
                guardaTabla(aux);
                cargaTabla(aux);
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

        public void cargaBD()
        {
            List<Tabla> aux = new List<Tabla>() ;
            foreach (Tabla t in tablas)
                aux.Add(abreTabla(t));

            tablas = aux;
        }
        /// <summary>
        /// Interfaz en el datagrid "grid"
        /// se intercalan los colores de los renglones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(204, 204, 255);//153,204,244);
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }


        /// <summary>
        /// Verifica que los datos escritos en la tabla "registro"
        /// coincidan con el tipo de dato en la tabla
        /// </summary>
        /// <returns>Regresa TRUE si todo coincide y FALSE si encontro una inconsistencia</returns>
        public bool validaTupla()
        {
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            for(int i = 0; i < registro.Columns.Count; i++)
            {
                if(registro.Columns[i].Name == aux.atributos[i].nombre)// valida si la columna pertenece al atributo
                {
                    if (aux.atributos[i].tipo == 'E' || aux.atributos[i].tipo == 'F')
                    {
                        int ejem;
                        if (!int.TryParse(registro.Rows[0].Cells[i].EditedFormattedValue.ToString(), out ejem))
                        {
                            MessageBox.Show(aux.atributos[i].nombre + " debe contener un valor numerico ");
                            return false;
                        }
                    }
                    else if(aux.atributos[i].tipo == 'C' && registro.Rows[0].Cells[i].EditedFormattedValue.ToString() == string.Empty)
                    {
                        MessageBox.Show(aux.atributos[i].nombre + " no debe ser una cadena vacia");
                        return false;
                    }

                }
            }
            return true;
        }

        public bool tuplaVacia()
        {
            int i;
            
            for(i = 0; i < registro.Columns.Count; i++)
            {
                if(registro.Rows[0].Cells[i].EditedFormattedValue == null)
                {
                    return true;
                }
            }
            return false;
        }


        private void menuTupla(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.AccessibleName)
            {
                case "creaTupla":
                    insertaTupla();
                break;
                case "modificaTupla":
                    MessageBox.Show("Modifica Tupla");
                break;
                case "eliminaTupla":
                    eliminarTupla();
                break;
            }
        }
        /// <summary>
        /// Se inserta la tupla escrita en el datagrid de nombre registro en
        /// el datagrid de nombre grid
        /// </summary>
        public void insertaTupla()
        {
            //1) Validar que todos los campos esten correctos
            if(tuplaVacia())
                MessageBox.Show("Complete todos los campos de la tupla");
            else
            { 
                if (validaTupla() == true)
                {
                   dt = new DataTable();
                   for (int i = 0; i < registro.Columns.Count; i++)
                        dt.Columns.Add(registro.Columns[i].HeaderText,typeof(string));
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < registro.Columns.Count; i++)
                        row[i] = registro.Rows[0].Cells[i].EditedFormattedValue;
                    grid.Rows.Add(row.ItemArray);
                    registro.Rows.Clear();
                }
            }
            
   
        }
        public void eliminarTupla()
        {
            try
            {
                grid.Rows.Remove(grid.CurrentRow);
            }
            catch
            {
                MessageBox.Show("La tupla seleccionada no se puede eliminar");
            }
            
        }
        public void cargaTabla(Tabla t)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            registro.Rows.Clear();
            registro.Columns.Clear();
            foreach(Atributo atributo in t.atributos)
            {
                grid.Columns.Add(atributo.nombre, atributo.nombre);
                registro.Columns.Add(atributo.nombre, atributo.nombre);
            }
            if(t.atributos.Count > 0)
            {
                modificaAtributo.Enabled = true;
                eliminaAtributo.Enabled = true;
                creaTupla.Enabled = true;
                eliminaTupla.Enabled = true;
                modificaTupla.Enabled = true;
            }
            else
            {
                eliminaTupla.Enabled = false;
                modificaTupla.Enabled = false;

                modificaAtributo.Enabled = false;
                eliminaAtributo.Enabled = false;
            }
        }
        #endregion

    }
}
