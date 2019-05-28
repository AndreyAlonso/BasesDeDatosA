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
        int row;
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
            integridadReferencial.Enabled = false;
            aplicaMod.Enabled = false;

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
            switch (e.ClickedItem.AccessibleName)
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
            if (directorio.ShowDialog() == DialogResult.OK)
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
            if (mensaje.ShowDialog() == DialogResult.OK)
            {
                Directory.Delete(directorioBD, true);
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
            try
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
            catch(Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al cargar la Base de Datos \n" + ex);
            }
            
        }
        private void Principal_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = ClientSize.Width + 10;
            pictureBox3.Location = new Point(ClientSize.Width - 50, pictureBox3.Location.Y);
            pictureBox4.Location = new Point(ClientSize.Width - pictureBox4.Width, pictureBox4.Location.Y);
            pictureBox4.Height = ClientSize.Height;
            pictureBox6.Height = ClientSize.Height;
            pictureBox5.Location = new Point(0, ClientSize.Height - pictureBox5.Height);
            pictureBox5.Width = ClientSize.Width;
            maximiza.Location = new Point(pictureBox3.Location.X - 47, maximiza.Location.Y);

        }

        private void maximizar(object sender, EventArgs e)
        {
            if (max == false)
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
            switch (e.ClickedItem.AccessibleName)
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
            if (d.ShowDialog() == DialogResult.OK)
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
                if (listBox1.Text != "" && d.nombreBD != null)
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
            registro.Rows.Clear();
            registro.Columns.Clear();
            grid.Rows.Clear();
            grid.Columns.Clear();
        }

        private void seleccionaTabla(object sender, EventArgs e)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            cargaTabla(aux);
            integridadReferencial.Items.Clear();
            if (aux != null && aux.tuplas.Count > 0)
            {
                modificaAtributo.Enabled = false;
                creaAtributo.Enabled = false;
            }
            else
            {
                modificaAtributo.Enabled = true;
                creaAtributo.Enabled = true;
            }
            modificaTupla.Enabled = false;
            eliminaTupla.Enabled = false;
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
            switch (e.ClickedItem.AccessibleName)
            {
                case "creaAtributo":
                    creaAtributos();
                    break;
                case "modificaAtributo":
                    modificaAtributos();
                    break;
                case "eliminaAtributo":
                    eliminarAtributos();
                    break;
            }
            Tabla aux = buscaTabla();
            aux = abreTabla(aux);
            cargaTabla(aux);
        }

        public void modificaAtributos()
        {
            Tabla aux = buscaTabla();
            cargaTabla(aux);
            aux = abreTabla(aux);
            cargaBD();
            VentanaAtributo ventanaA = new VentanaAtributo(directorioBD, aux, tablas, "modifica");
            if (listBox1.Text != "")
            {
                if (ventanaA.ShowDialog() == DialogResult.OK)
                {
                    //aux.atributos[ventanaA.pos] = ventanaA.aModificado;
                    //aux = abreTabla(aux);
                   
                    if(ventanaA.aModificado != null)
                    {
                        aux.atributos = ventanaA.atributos;
                        //aux.atributos[ventanaA.pos] = ventanaA.aModificado;
                        guardaTabla(ventanaA.dameTabla());
                        cargaTabla(aux);
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Seleccione una Tabla");
            }
        }
        #region  ATRIBUTOS
        public void deshabilitaAtributos()
        {
            creaAtributo.Enabled = false;
            modificaAtributo.Enabled = false;
            eliminaAtributo.Enabled = false;
        }
        /// <summary>
        /// Devuelve la tabla seleccionada por el listbox
        /// </summary>
        /// <returns></returns>
        public Tabla buscaTabla()
        {
            foreach (Tabla t in tablas)
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
            if (listBox1.Text != "")
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
            cargaBD();
            EliminarAtributo elimina = new EliminarAtributo(aux, tablas);
            if (elimina.ShowDialog() == DialogResult.OK)
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
            using (stream = new FileStream(directorioBD + "\\" + t.nombre + t.extension, FileMode.Create, FileAccess.Write, FileShare.None))
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
        /// <summary>
        /// Carga en lista Tabla toda la base de datos 
        /// </summary>
        public void cargaBD()
        {
            List<Tabla> aux = new List<Tabla>();
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
            for (int i = 0; i < registro.Columns.Count; i++)
            {
                if (registro.Columns[i].Name == aux.atributos[i].nombre)// valida si la columna pertenece al atributo
                {
                    if (aux.atributos[i].indice == "Clave Primaria")
                    {
                        for (int j = 0; j < grid.Rows.Count - 1; j++)
                        {
                            if (registro.Rows[0].Cells[i].EditedFormattedValue.ToString() == grid.Rows[j].Cells[i].Value.ToString())
                            {
                                if (j != row)
                                {
                                    MessageBox.Show("La clave primaria ya existe");
                                    return false;
                                }
                                // MessageBox.Show("La clave primaria ya existe");
                                // return false;
                                // else
                                //   registro.Rows[0].Cells[j].ReadOnly = true;

                            }
                        }

                    }
                    if (aux.atributos[i].tipo == 'E' || aux.atributos[i].tipo == 'F')
                    {
                        int ejem;
                        if (!int.TryParse(registro.Rows[0].Cells[i].EditedFormattedValue.ToString(), out ejem))
                        {
                            MessageBox.Show(aux.atributos[i].nombre + " debe contener un valor numerico ");
                            return false;
                        }
                    }
                    else if (aux.atributos[i].tipo == 'C' && registro.Rows[0].Cells[i].EditedFormattedValue.ToString() == string.Empty)
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

            for (i = 0; i < registro.Columns.Count; i++)
            {
                if (registro.Rows[0].Cells[i].EditedFormattedValue == null)
                {
                    return true;
                }
            }
            return false;
        }


        private void menuTupla(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "creaTupla":
                    insertaTupla();
                    guardaTupla();
                    break;
                case "modificaTupla":
                    modificarTupla();
                  //  guardaTupla();
                    break;
                case "eliminaTupla":
                    eliminarTupla();
                    guardaTupla();
                    break;
                case "aplicaMod":
                    aplicarModificacion();
                    guardaTupla();
                    break;
            }
            //guardaTupla();
        }
        public void aplicarModificacion()
        {
            if (tuplaVacia())
                MessageBox.Show("Complete todos los campos de la tupla");
            else if (validaTupla() == true)
            {
                for (int i = 0; i < registro.Columns.Count; i++)
                    grid.Rows[row].Cells[i].Value = registro.Rows[0].Cells[i].EditedFormattedValue;
                aplicaMod.Enabled = false;
                creaTupla.Enabled = true;
                eliminaTupla.Enabled = true;
                modificaTupla.Enabled = true;
                registro.Rows.Clear();
            }

        }
        /// <summary>
        /// Metodo para realizar modificaciones en una tupla 
        /// </summary>
        public void modificarTupla()
        {
            Tabla t = buscaTabla();
            t = abreTabla(t);
            cargaBD();
            t = tablas.Find(x=>x.nombre.Equals(listBox1.Text));
            if (row < t.tuplas.Count)
            {
                string reg = t.tuplas[row];
                registro.Rows.Add(reg.Split(','));
                aplicaMod.Enabled = true;
                creaTupla.Enabled = false;
                eliminaTupla.Enabled = false;
                modificaTupla.Enabled = false;
                //  ModificaTupla mt = new ModificaTupla(registro, reg);
                //  if (mt.ShowDialog() == DialogResult.OK)
                //  {

                //  }
            }


        }
        /// <summary>
        /// Se inserta la tupla escrita en el datagrid de nombre registro en
        /// el datagrid de nombre grid
        /// </summary>
        public void insertaTupla()
        {
            //1) Validar que todos los campos esten correctos
            if (tuplaVacia())
                MessageBox.Show("Complete todos los campos de la tupla");
            else
            {
                if (validaTupla() == true)
                {
                    dt = new DataTable();
                    for (int i = 0; i < registro.Columns.Count; i++)
                        dt.Columns.Add(registro.Columns[i].HeaderText, typeof(string));
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
            //buscar si la alguien esta usando su PK
            //  for (int i = 0; i < tablas.Count; i++)
            //    tablas[i] = abreTabla(tablas[i]);


            try
            {
                grid.Rows.Remove(grid.CurrentRow);
                if (grid.Rows.Count > 0)
                {
                    modificaAtributo.Enabled = false;
                    creaAtributo.Enabled = false;
                }
                else
                {
                    modificaAtributo.Enabled = true;
                    creaAtributo.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("La tupla seleccionada no se puede eliminar");
            }

        }
        int celda;
        /// <summary>
        /// Cuando se selecciona una celda del datagrid "registro"
        /// entonces se detecta si la columna pertenece a una clave foranea (FK)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registro_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            integridadReferencial.Items.Clear();
            Tabla t = tablas.Find(x => x.nombre.Equals(listBox1.SelectedItem));
            Tabla referencial;
            t = abreTabla(t); // Se obtiene la tabla a la que se estará agregando registros
            cargaBD();
            foreach (Atributo a in t.atributos)
            {
                if (a.nombre == registro.Columns[e.ColumnIndex].HeaderText) //verificar si es el atributo de la columna actual
                {
                    if (a.foranea != "NULL") //Si existe clave foranea
                    {
                        //buscar la tabla que tenga como atributo.nombre la a.foranea
                        referencial = buscaEnRelacion(a);
                        if (referencial != null)
                        {
                            integridadReferencial.Enabled = true;
                            cargaPrimarias(referencial);
                            celda = e.ColumnIndex;
                            registro.Rows[0].Cells[e.ColumnIndex].ReadOnly = true;
                            break;
                        }
                        else
                        {
                            integridadReferencial.Enabled = false;
                            registro.Rows[0].Cells[e.ColumnIndex].ReadOnly = false;
                        }
                    }
                    if (a.indice == "Clave Primaria" && aplicaMod.Enabled == true)
                    {
                        registro.Rows[0].Cells[e.ColumnIndex].ReadOnly = true;
                        break;
                    }
                }
                else
                {
                    integridadReferencial.Enabled = false;
                    registro.Rows[0].Cells[e.ColumnIndex].ReadOnly = false;
                }
            }


        }
        /// <summary>
        /// Busca una tabla en base a un atributo
        /// si la tabla tiene el atributo que buscamos
        /// devuelve esta tabla
        /// si no, regresa NULL
        /// </summary>
        /// <param name="relacion"></param>
        /// <returns></returns>
        public Tabla buscaEnRelacion(Atributo relacion)
        {
            if (relacion != null)
                foreach (Tabla aux in tablas)
                {
                    foreach (Atributo aux2 in aux.atributos)
                    {
                        if (relacion.foranea == aux2.nombre && aux2.indice == "Clave Primaria")
                        {
                            return aux;
                        }
                    }
                }
            return null;
        }
        /// <summary>
        /// Agrega todos los registros de la Entidad que se hace relación
        /// en el combobox
        /// </summary>
        public void cargaPrimarias(Tabla t)
        {
            integridadReferencial.Items.Clear();
            foreach (string aux in t.tuplas)
            {
                integridadReferencial.Items.Add(aux);
            }
        }
        public Atributo encuentraRelacion(Atributo iR)
        {
            foreach (Tabla aux in tablas)
            {
                foreach (Atributo aux2 in aux.atributos)
                {
                    if (iR.foranea == aux2.nombre)
                        return aux2;
                }
            }
            return null;
        }

        public void guardaTupla()
        {
            Tabla actual = abreTabla(buscaTabla());
            actual.tuplas = new List<string>();
            string row;
            for (int i = 0; i < grid.Rows.Count - 1; i++)
            {
                row = "";
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    row += grid.Rows[i].Cells[j].EditedFormattedValue + ",";
                }
                row = row.Remove(row.Length - 1);
                actual.tuplas.Add(row);
            }
            guardaTabla(actual);
            cargaTabla(actual);


        }

        private void integridadReferencial_SelectedIndexChanged(object sender, EventArgs e)
        {
            registro.Rows[0].Cells[celda].Value = integridadReferencial.Text.Split(',').First();
        }

        private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            FormConsulta formconsulta = new FormConsulta();
            if (formconsulta.ShowDialog() == DialogResult.OK)
            {
                //Aquí se genera la consulta 
            }
        }

        string[] palabrasReservadas = { "SELECT", "FROM", "WHERE", "INNER JOIN", "ON" };
        private void txtConsulta_TextChanged(object sender, EventArgs e)
        {
            /*
            int i, inicio, final;
            for(i = 0; i < palabrasReservadas.Length; i++)
            {
                inicio = txtConsulta.Find(palabrasReservadas[i]);
                final = txtConsulta.Text.LastIndexOf(palabrasReservadas[i]);
                if( inicio >= 0 && final >= 0)
                {
                    txtConsulta.SelectionLength = palabrasReservadas[i].Length;
                    txtConsulta.SelectionColor = Color.Blue;

                }
                else
                {

                    final = txtConsulta.Text.LastIndexOf("//");
                    if(txtConsulta.Find("//") >= 0 && final >= 0)
                    {
                        txtConsulta.SelectionLength = palabrasReservadas[i].Length;
                        txtConsulta.SelectionColor = Color.Black;
                    }
                    else
                    {
                        txtConsulta.SelectionLength = palabrasReservadas[i].Length;
                        txtConsulta.SelectionColor = Color.Black;
                    }
                }
                txtConsulta.SelectionStart = txtConsulta.Text.Length;
               // txtConsulta.SelectionColor = Color.Black;

            }*/
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            if (txtConsulta.Text == string.Empty)
                MessageBox.Show("Se debe escribir una consulta SQL");
            else
                verificaConsulta();


        }
        /// <summary>
        /// Dada una consulta SQL, se obtiene las columnas definidas en la consulta
        /// </summary>
        /// <param name="consulta">Consulta SQL sin SELECT</param>
        /// <returns></returns>
        public string obtenColumnas(string consulta)
        {
            int i;
            string cmp;
            string col = "";
            for (i = 0; i < consulta.Length; i++)
            {
                cmp = "";
                if (i + 3 < consulta.Length)
                    cmp = string.Concat(consulta[i], consulta[i + 1], consulta[i + 2], consulta[i + 3]);
                if (cmp == "FROM")
                {
                    posFROM = i + 3 + 1;
                    return col;
                }
                else
                    col += consulta[i];

            }
            return "";
        }
        public List<string> limpiaColumnas(List<string> columnas)
        {
            List<string> lista = new List<string>();
            foreach (string col in columnas)
            {
                lista.Add(col.Replace(" ", ""));
            }
            return lista;

        }
        /// <summary>
        /// Se obtiene el nombre de la tabla en la consulta SQL
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns></returns>
        public string obtenTabla(string consulta)
        {
            int i;
            string tabla = "";
            string cmp;
            for (i = posFROM; i < consulta.Length; i++)
            {
                cmp = "";
                if (i + 4 < consulta.Length)
                    cmp = string.Concat(consulta[i], consulta[i + 1], consulta[i + 2], consulta[i + 3], consulta[i + 4]);
                if (cmp == "WHERE" || cmp == "INNER")
                {
                    posWHERE = i + 4;
                    return tabla;
                }
                else
                    tabla += consulta[i];
            }
            return tabla;
        }


        public Atributo buscaClavePrimaria(Tabla t)
        {
            foreach(Atributo a in t.atributos)
            {
                if (a.indice == "Clave Primaria")
                    return a;
            }
            return null;
        }

        int posFROM = 0;
        int posWHERE = 0;

        public void verificaConsulta()
        {

            //1) Verificar que cuenta con SELECT, FROM, WHERE

            string s = "SELECT";
            string f = "FROM";
            string w = "WHERE";
            List<string> columnas = new List<string>();

            string compara = "";

            foreach (char c in txtConsulta.Text)
            {
                compara += c;
                if (compara.Length == s.Length)
                    break;
            }
            if (compara == s)
            {
                compara = txtConsulta.Text.Substring(compara.Length);
                if (compara.Contains("FROM"))
                {
                    string col = obtenColumnas(compara);

                    columnas = col.Split(',').ToList();
                    columnas = limpiaColumnas(columnas);
                    string tabla = obtenTabla(compara);
                    tabla = tabla.Replace(" ", "");
                    if (listBox1.Items.Contains(tabla))
                    {
                        if (col != string.Empty && col != " ")
                        {
                            cargaBD();
                            //1) Contiene WHERE
                            if (compara.Contains("WHERE"))
                            {
                                string condicion = obtenCondicion(compara);
                                // Se verifica que las columnas correspondan a la Tabla de la base de datos
                                if (columnas[0].Contains("*") && columnas.Count == 1)
                                {
                                    //MessageBox.Show("Se han seleccionado todos los atributos");
                                    Tabla t = tablas.Find(x => x.nombre.Equals(tabla));
                                    cargaTabla(t, condicion);
                                }
                                else
                                {
                                    if (verificaAtributos(tabla, columnas))
                                    {
                                        // MessageBox.Show("Todas los atributos existen");
                                        Tabla t = tablas.Find(x => x.nombre.Equals(tabla));
                                        cargaTabla(t, columnas, condicion);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hay atributos que no pertenecen a la Tabla " + tabla);
                                    }
                                }
                            }
                            else if (compara.Contains("INNER"))
                            {
                                if(!compara.Contains("*"))
                                {
                                    //MessageBox.Show("INNER JOIN");
                                    List<string> tablasC = obtenTablasC(columnas, compara);
                                    string cond = obtenON(compara);
                                    List<string> temp = cond.Split(' ').ToList();
                                    List<string> lista = new List<string>();
                                    foreach (string str in temp)
                                        if (str != string.Empty)
                                            lista.Add(str);
                                    List<string> atr = new List<string>();
                                    foreach (string str in columnas)
                                        atr.Add(str.Split('.').Last());
                                    if (lista.Count == 3)
                                    {
                                        bool entra = true;
                                        foreach (string cad in columnas)
                                        {
                                           
                                            Tabla b = tablas.Find(x => x.nombre.Equals(cad.Split('.').First()));
                                            if(b!= null)
                                            {
                                                Atributo c = b.atributos.Find(x => x.nombre.Equals(cad.Split('.').Last()));
                                                if(c == null)
                                                {
                                                    MessageBox.Show("Atributo " + cad.Split('.').Last() + " no coincide en tabla "+ cad.Split('.').First());
                                                    entra = false;
                                                    break;
                                                }

                                            }
                                        }  
                                        if(entra)
                                            validaConsulta(lista, atr);
                                    }
                                    else
                                        MessageBox.Show("Error: Separa las palabras por espacios");
                                    /*
                                    Atributo tmp = new Atributo();
                                    tmp.nombre = lista[2].Split('.').Last();
                                    Tabla aux = buscaEnRelacion(tmp);*/
                                    //Atributo b = buscaClavePrimaria(aux);
                                    /*
                                     if (lista[0].Split('.').Last() == b.nombre)
                                     {

                                         creaConsulta(lista, tablasC, columnas);

                                     }
                                     else
                                     {
                                         MessageBox.Show("Los atributos para realizar INNER JOIN deben coincidir");

                                     }*/

                                    //validaColumnas(columnas);
                                }
                                else
                                {
                                    limpiaGrid();
                                    MessageBox.Show("La consulta INNER JOIN no puede contener *");
                                }

                            }
                            else
                            {
                                // Se verifica que las columnas correspondan a la Tabla de la base de datos
                                if (columnas[0].Contains("*") && columnas.Count == 1)
                                {
                                    //MessageBox.Show("Se han seleccionado todos los atributos");
                                    Tabla t = tablas.Find(x => x.nombre.Equals(tabla));
                                    cargaTabla(t);
                                }
                                else
                                {
                                    if (verificaAtributos(tabla, columnas))
                                    {
                                       //  MessageBox.Show("Todas los atributos existen");
                                        Tabla t = tablas.Find(x => x.nombre.Equals(tabla));
                                        cargaTabla(t, columnas);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hay atributos que no pertenecen a la Tabla " + tabla);
                                    }
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Complete las columnas en la consulta");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: La tabla " + tabla + " no existe en la Base de Datos");
                    }
                }
                else
                {
                    MessageBox.Show("Falta FROM");
                }
            }
            else
            {
                MessageBox.Show("Falta SELECT");
            }
            if(grid.Columns.Count > 0 )
            {
                ordenaConsulta(columnas);
            }
        }
        /// <summary>
        /// Ordenamiento en el datagrid para mostar las columnas dada la consulta
        /// </summary>
        /// <param name="columnas">Columnas de la consulta</param>
        public void ordenaConsulta(List<string> columnas)
        {
            int i = 0;
            if(!columnas.Contains("*") )
            {
                for (i = 0; i < columnas.Count; i++)
                    grid.Columns[columnas[i].Split('.').Last()].DisplayIndex = i;

            }
            
        }
        public void creaConsulta(List<string> lista, List<string> tablasC, List<string> columnas)
        {
            Tabla T = new Tabla("Consulta");
            Tabla izquierdo = tablas.Find(x => x.nombre.Equals(columnas[0].Split('.').First()));
            Tabla derecho = tablas.Find(x => x.nombre.Equals(columnas[1].Split('.').First()));
            cargaIzquierdo(izquierdo);
            cargaDerecho(derecho);
  //          fusion(columnas, lista);


        }
        public void fusion(List<string> columnas, List<string> lista, int posI, int posD)
        {
            int i, j, k, l;
            string renglon = "";
            limpiaGrid();
            List<string> pila = new List<string>();
            //1) se agregan las columnas a grid
            foreach (string s in columnas)
            {
                grid.Columns.Add(s.Split('.').Last(), s.Split('.').Last());
            }
            //2) Se recorren las 2 tablas
            for (i = 0; i < izq.Rows.Count-1; i++)
            {

                    for(k = 0; k < der.Rows.Count-1; k++)
                    {

                            renglon = "";
                            if(izq.Rows[i].Cells[posI].EditedFormattedValue.ToString() == der.Rows[k].Cells[posD].EditedFormattedValue.ToString())
                            {
                                int cont;
                                for(cont = 0; cont < izq.Columns.Count; cont++)
                                {
                                    renglon += izq.Rows[i].Cells[cont].EditedFormattedValue + ",";
                                }
                                for (cont = 0; cont < der.Columns.Count; cont++)
                                {
                                    renglon += der.Rows[k].Cells[cont].EditedFormattedValue + ",";
                                }
                                renglon = renglon.Remove(renglon.Length-1);
                                if (pila.Find(x => x.Equals(renglon)) == null)
                                {
                                    grid.Rows.Add(renglon.Split(','));
                                    pila.Add(renglon);
                                }
                                 
                                    
                            
                        }
                    

                }
            }


        }
        /// <summary>
        /// Carga la Tabla Izquierda para realizar INNER JOIN en un DatagridView oculto
        /// </summary>
        /// <param name="t">Tabla Izquierda</param>
        public void cargaIzquierdo(Tabla t)
        {
            izq.Rows.Clear();
            izq.Columns.Clear();
            if (t != null)
                foreach (Atributo atributo in t.atributos)
                {
                    if (atributo != null)
                    {
                        izq.Columns.Add(atributo.nombre, atributo.nombre);
                    }
                }
            if (t != null)
                foreach (string tupla in t.tuplas)
                    izq.Rows.Add(tupla.Split(','));
        }
        /// <summary>
        /// Carga la Tabla Derecha para realizar INNER JOIN en un DatagridView oculto
        /// </summary>
        /// <param name="t">Tabla Izquierda</param>
        public void cargaDerecho(Tabla t)
        {
            der.Rows.Clear();
            der.Columns.Clear();
            if (t != null)
                foreach (Atributo atributo in t.atributos)
                {
                    if (atributo != null)
                    {
                        der.Columns.Add(atributo.nombre, atributo.nombre);
                    }
                }
            if (t != null)
                foreach (string tupla in t.tuplas)
                    der.Rows.Add(tupla.Split(','));
        }

        /// <summary>
        /// Identifica la condicion establecida en ON de la consulta SQL
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public string obtenON(string consulta)
        {
            int inicio, fin;
            inicio = consulta.LastIndexOf("ON");
            string sql = consulta.Substring(inicio+2, consulta.Length- inicio - 2);

            return sql;
        }
        /// <summary>
        /// Se verifica en la condición de INNER JOIN si cumplen ambas claves donde:
        /// 1. Debe estar una clave primaria
        /// 2. La clave foranea debe relacionarse con la primaria
        /// </summary>
        /// <param name="consulta">Consulta SQL con INNER JOIN </param>
        public void validaConsulta(List<string> l, List<string> tablasC)
        {
            string a, b;
            string nombreA, nombreB;
            a = l[0];
            b = l[2];
            string PK,FK;
            string nForanea;
            PK = FK =  nForanea = "";
            int posI, posD;

            //1 verificar con ambas claves cual es clave primaria
            nombreA =  a.Split('.').First();
            nombreB =  b.Split('.').First();
            Tabla auxA = tablas.Find(x => x.nombre.Equals(nombreA));
            Tabla auxB = tablas.Find(x => x.nombre.Equals(nombreB));
            if(auxA!= null && auxB != null)
            {
                cargaIzquierdo(auxA);
                cargaDerecho(auxB);
                foreach (Atributo atr in auxA.atributos)
                {
                    if (atr.nombre == a.Split('.').Last() && atr.indice == "Clave Primaria")
                    {
                        PK = "A";
                        break;
                    }
                    else if (atr.foranea == b.Split('.').Last())// A.foranea == B.primaria
                    {
                        FK = "A";
                        nForanea = atr.nombre;
                        break;
                    }
                }
                foreach (Atributo atr in auxB.atributos)
                {
                    if (atr.nombre == b.Split('.').Last() && atr.indice == "Clave Primaria")
                    {
                        PK = "B";
                        break;
                    }
                    else if (atr.foranea == a.Split('.').Last())// A.foranea == B.primaria
                    {
                        FK = "B";
                        nForanea = atr.nombre;
                        break;
                    }
                }
                posI = encuentraPosIzquierda(a.Split('.').Last());
                posD = encuentraPosDerecha(b.Split('.').Last());
                int i;
                List<string> col = new List<string>();
                for (i = 0; i < izq.Columns.Count; i++)
                    col.Add(izq.Columns[i].HeaderText);
                for (i = 0; i < der.Columns.Count; i++)
                    col.Add(der.Columns[i].HeaderText);
                if (posI != -1 && posD != -1)
                {
                    fusion(col, null, posI, posD);
                    cargaColumnas(tablasC);
                }
                else
                {
                    limpiaGrid();
                    MessageBox.Show("Error en la consulta");
                }
            }
            else
            {
                limpiaGrid();
                MessageBox.Show("Error de sintaxis en los nombres de las Tablas");
            }
           
            
        }
        public void cargaColumnas(List<string> col)
        {
            int i = 0;
            List<string> existe = new List<string>();
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (col.Contains(grid.Columns[i].HeaderText))
                {
                    if(existe.Count == 0 || existe.Find(x=>x.Equals(grid.Columns[i].HeaderText)) == null)
                    {
                        grid.Columns[i].Visible = true;
                        existe.Add(grid.Columns[i].HeaderText);
                    }
                    else
                    {
                        grid.Columns[i].Visible = false;
                    }

                }
                else
                {
                    grid.Columns[i].Visible = false;
                }
            }
            if(grid.Rows.Count <= 1 && grid.Columns.Count < 1 )
            {
                limpiaGrid();
                MessageBox.Show("No hay datos de coincidencia en INNER JOIN");
            }
        }
        /// <summary>
        /// Recorre el DatagridView IZQ devolviendo la posición de su clave primaria/foranea
        /// </summary>
        /// <param name="columna">nombre a buscar</param>
        /// <returns></returns>
        public int encuentraPosIzquierda(string columna)
        {
            int i;
            for(i = 0; i < izq.Columns.Count; i++)
            {
                if (izq.Columns[i].HeaderText == columna)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Recorre el DatagridView DER devolviendo la posición de su clave primaria/foranea
        /// </summary>
        /// <param name="columna">nombre a buscar</param>
        /// <returns></returns>
        public int encuentraPosDerecha(string columna)
        {
            int i;
            for (i = 0; i < der.Columns.Count; i++)
            {
                if (der.Columns[i].HeaderText == columna)
                    return i;
            }
            return -1;
        }
        public List<string> obtenTablasC(List<string> columnas,string compara)
        {
            int TAM = columnas.Count;
            int i = 0;
            string nombre;
            List<string> tablasC = new List<string>();
            foreach(Tabla t in tablas)
            {
                foreach(string s in columnas)
                {
                    nombre = s.Split('.').First();
                    if(t.nombre.Contains(nombre))
                    {
                        if(!tablasC.Contains(nombre))
                        {
                            tablasC.Add(nombre);
                        }
                    }
                }
            }
            //Verificar si las tablas seleccionadas por FROM e INNER JOIN son las mismas que en tablasC
            string izquierdo, derecho;// = compara.Split('I','N','N', 'E', 'R' ).First();
            int inicio = compara.IndexOf("FROM");
            int final = compara.IndexOf("INNER");
            izquierdo=  compara.Substring(inicio+4,final-inicio-4);
            inicio = compara.IndexOf("JOIN");
            final = compara.IndexOf("ON");
            derecho = compara.Substring(inicio + 4, final - inicio-4);
            //MessageBox.Show(izquierdo);
            izquierdo = izquierdo.Replace(" ", "");
            derecho = derecho.Replace(" ", "");
            if (tablasC.Contains(izquierdo) && tablasC.Contains(derecho))
            {
                //Se procede a verificar si los atributos si pertenecen a las respectivas tablas
                validaColumnas(columnas);
            }
            else
            {
               /// MessageBox.Show("Error, Favor de verificar tablas"); 
            }
               
           

            return tablasC;
        }
        public int validaColumnas(List<string> columnas)
        {
            bool bien = false;
            foreach(Tabla t in tablas)
            {
               foreach(string s in columnas)
                {
                    if (t.nombre.Equals(s.Split('.').First()))
                    {
                        foreach(Atributo a in t.atributos)
                        {
                            if(a.nombre.Equals(s.Split('.').Last()))
                            {
                                bien = true;
                            }
                        }
                        if (bien == false)
                        {
                            MessageBox.Show("Hay atributos que no pertenecen a la Tabla seleccionada ");
                            return -1;
                        }
                            
                    }
                }

            }
            return 1;
        }

        /// <summary>
        /// Obtiene la condición en la consulta
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns></returns>
        public string obtenCondicion(string consulta)
        {
            int i, j = -1;

            string condicion = "";
            bool band = false;
            for (i = 0; i < consulta.Length; i++)
            {
               
                if (i + 4 < consulta.Length && (string.Concat(consulta[i], consulta[i + 1], consulta[i + 2], consulta[i + 3], consulta[i + 4]) == "WHERE"))
                {
                    //MessageBox.Show(string.Concat(consulta[i], consulta[i + 1], consulta[i + 2], consulta[i + 3], consulta[i + 4]));
                    band = true;
                    j = i + 5;
                }
                   
                if (band = true && j == i)
                {
                    condicion += consulta[i];
                    j++;
                }
                    
            }
            return condicion;
        }
        public int posM = 0;
        private void txtConsulta_MouseClick(object sender, MouseEventArgs e)
        {

        }
        /// <summary>
        /// Verifica si los atributos dados en la consulta existen en la Tabla seleccionada
        /// </summary>
        /// <param name="tabla">Nombre de la Tabla solicitada en la consulta</param>
        /// <param name="columnas">Atributos seleccionados en la consulta</param>
        /// <returns>Regresa true si todos los atributos existen en la tabla</returns>
        public bool verificaAtributos(string tabla, List<string> columnas)
        {
            int total = columnas.Count;
            int i = 0;
            foreach(Tabla t in  tablas)
            {
                if(t.nombre == tabla)
                {
                    foreach(Atributo a in t.atributos)
                    {
                        foreach(string s in columnas)
                        {
                            if (s.Contains( a.nombre))
                                i++;
                        }
                    }
                    if (i == total)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Realiza la consulta con WHERE y con columnas seleccionadas
        /// </summary>
        /// <param name="t">Tabla a consultar</param>
        /// <param name="columnas">Columnas Seleccionadas para consulta</param>
        /// <param name="condicion">clave a comparar en WHERE</param>
        public void cargaTabla(Tabla t, List<string> columnas, string condicion)
        {
            List<string> aux = condicion.Split(' ').ToList();
            List<string> cond = new List<string>();
            foreach (string a in aux)
                if (a != string.Empty)
                    cond.Add(a);
            cargaTabla(t);
            //Se eliminan las columnas que no estan  en las consultas
            int i;
            bool existe = false;
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (columnas.Contains(grid.Columns[i].HeaderText))
                {
                    grid.Columns[i].Visible = true;
                }
                else
                {
                    grid.Columns[i].Visible = false;
                  
                }
            }
            //Se verifica que existe la clave de la condicion en los atributos
            foreach (Atributo a in t.atributos)
            {
                if (cond.Count > 0 && cond[0] != string.Empty && a.nombre == cond[0])
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                realizaCondicion(t, cond);
            }
            else
            {
                limpiaGrid();
                MessageBox.Show("Error en WHERE");
            }

        }
        /// <summary>
        /// Realiza la consulta en base al WHERE 
        /// </summary>
        /// <param name="t">Tabla a consultar</param>
        /// <param name="condicion">clave a comparar en WHERE</param>
        public void cargaTabla(Tabla t, string condicion)
        {
            List<string> aux = condicion.Split(' ').ToList();
            List<string> cond = new List<string>();
            foreach (string a in aux)
                if (a != string.Empty)
                    cond.Add(a);
            cargaTabla(t);
            //Se eliminan las columnas que no estan  en las consultas
            int i;
            bool existe = false;
   
            //Se verifica que existe la clave de la condicion en los atributos
            foreach(Atributo a in t.atributos)
            {
                if(cond[0] != string.Empty && a.nombre == cond[0])
                {
                    existe = true;
                    break;
                }
            }
            if(existe)
            {
                realizaCondicion(t,cond);
            }
            else
            {
                limpiaGrid();
                MessageBox.Show("Error en WHERE");
            }

        }
        /// <summary>
        /// Se aplica la condición dada por WHERE
        /// </summary>
        /// <param name="t">Tabla a consultar</param>
        /// <param name="condicion">Condicion separa en una lista</param>
        public void realizaCondicion(Tabla t,List<string> condicion)
        {
            int respuesta = 0;
            // verificar tipo de condicion
            if (condicion.Count < 3)
            {
                limpiaGrid();
                MessageBox.Show("ERROR: Condicional en WHERE incompleta");
            }
                
            else
            switch (condicion[1])
            {
                case "=":
                    respuesta = buscaTupla(condicion);
                    break;
                case ">":
                     respuesta = buscaTuplaMayor(condicion);
                    break;
                case "<":
                     respuesta = buscaTuplaMenor(condicion);
                    break;
                case ">=":
                        respuesta = buscaTuplaMayorIgual(condicion);
                    break;
                case "<=":
                        respuesta = buscaTuplaMenorIgual(condicion);
                    break;
                case "<>":
                        respuesta = buscaTuplaDiferente(condicion);
                    break;
                 
            }
            if(respuesta == 0)
            {
                limpiaGrid();
                MessageBox.Show("El operador utilizado no es valido");
            }
        }
        /// <summary>
        /// Realiza la consulta mostrando solo las tuplas DIFERENTES al dato solicitado
        /// </summary>
        /// <param name="condicion">condicion WHERE guardado en lista</param>
        public int buscaTuplaDiferente(List<string> condicion)
        {
            int i, j;
            bool existe = false;
            int clave = Convert.ToInt32(condicion[2]);
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for (j = 0; j < grid.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt32(grid.Rows[j].Cells[i].EditedFormattedValue) != clave)
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            else
                return 1;
            
        }
        /// <summary>
        /// Realiza la consulta mostrando solo las tuplas MAYORES e IGUALES al dato solicitado
        /// </summary>
        /// <param name="condicion">condicion WHERE guardado en lista</param>
        public int buscaTuplaMayorIgual(List<string> condicion)
        {
            int i, j;
            bool existe = false;
            int clave = Convert.ToInt32(condicion[2]);
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for (j = 0; j < grid.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt32(grid.Rows[j].Cells[i].EditedFormattedValue) >= clave)
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            return 1;
        }
        /// <summary>
        /// Realiza la consulta mostrando solo las tuplas MENORES e IGUALES al dato solicitado
        /// </summary>
        /// <param name="condicion">condicion WHERE guardado en lista</param>
        public int buscaTuplaMenorIgual(List<string> condicion)
        {
            int i, j;
            bool existe = false;
            int clave = Convert.ToInt32(condicion[2]);
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for (j = 0; j < grid.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt32(grid.Rows[j].Cells[i].EditedFormattedValue) <= clave)
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            else
                return 1;
        }
        /// <summary>
        /// Realiza la consulta mostrando solo las tuplas MENORES al dato solicitado
        /// </summary>
        /// <param name="condicion">condicion WHERE guardado en lista</param>
        public int buscaTuplaMenor(List<string> condicion)
        {
            int i, j;
            bool existe = false;
            int clave;
            if(condicion.Count == 3)
                clave = Convert.ToInt32(condicion[2]);
            else
            {
                return 0;
            }
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for (j = 0; j < grid.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt32(grid.Rows[j].Cells[i].EditedFormattedValue) < clave)
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            else
                return 1;
        }
        /// <summary>
        /// Realiza la consulta mostrando solo las tuplas mayores al dato solicitado
        /// </summary>
        /// <param name="condicion">condicion WHERE guardado en lista</param>
        public int buscaTuplaMayor(List<string> condicion)
        {
            int i, j;
            bool existe = false;
            int clave = Convert.ToInt32(condicion[2]);
            for (i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for (j = 0; j < grid.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt32(grid.Rows[j].Cells[i].EditedFormattedValue) > clave)
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            return 1;
        }

        /// <summary>
        /// Consulta comparando = 
        /// </summary>
        /// <param name="condicion">=</param>
        public int buscaTupla(List<string> condicion )
        {
            int i, j;
            bool existe = false;
            for(i = 0; i < grid.Columns.Count; i++ )
            {
                if(grid.Columns[i].HeaderText.Contains(condicion[0]))
                {
                    for(j = 0; j < grid.Rows.Count-1; j++)
                    {
                        if (grid.Rows[j].Cells[i].EditedFormattedValue.ToString() == condicion[2])
                        {
                            grid.Rows[j].Visible = true;
                            existe = true;
                        }
                        else
                            grid.Rows[j].Visible = false;
                    }
                }
            }
            if (existe == false)
            {
                limpiaGrid();
                MessageBox.Show("El valor buscado " + condicion[2] + " no existe en la tabla actual");
                return 0;

            }
            else
                return 1;
        }
        /// <summary>
        /// Se eliminan las columnas y los renglones del Componente grid
        /// </summary>
        public void limpiaGrid()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
        }
        /// <summary>
        /// Muestra La tabla en el datagrid pero solo con las columnas dadas por la consulta
        /// </summary>
        /// <param name="t"> Tabla seleccionada para consulta</param>
        /// <param name="columnas">Lista de Columnas seleccionadas para mostrar en la consulta</param>
        public void cargaTabla(Tabla t, List<string> columnas)
        {
            cargaTabla(t);
            //Se eliminan las columnas que no estan  en las consultas
            int i;
            for(i = 0; i < grid.Columns.Count; i++ )
            {
                if(columnas.Contains(grid.Columns[i].HeaderText))
                {
                    grid.Columns[i].Visible = true;
                }
                else
                {
                    grid.Columns[i].Visible = false;
                }
            }
            
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            validaIntegridadReferencial(e.RowIndex);
        }

        private void grid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            validaIntegridadReferencial(e.RowIndex);
        }
        /// <summary>
        /// Se recorre toda la base de datos y se valida si ya hay una relacion entre los registros para evitar eliminación y modificación
        /// en cascada
        /// </summary>
        /// <param name="celda"> Renglon seleccionado en grid</param>
        public void validaIntegridadReferencial(int celda)
        {
            try
            {
                cargaBD();
                int clave = -1;
                int i;
                string nColumna = "";
                bool existe = false;
                Tabla t = tablas.Find(x => x.nombre.Equals(listBox1.Text));
                Tabla aux;
                if (t != null)
                {
                    foreach (Atributo a in t.atributos)
                    {
                        for (i = 0; i < grid.Columns.Count; i++)
                        {
                            if (a.indice == "Clave Primaria" && a.nombre == grid.Columns[i].HeaderText)
                            {
                                clave = Convert.ToInt32(grid.Rows[celda].Cells[i].Value);
                                nColumna = a.nombre;
                                break;
                            }
                        }
                    }
                    // Se recorren todas las tablas

                    foreach (string s in listBox1.Items)
                    {
                        if (s != t.nombre)
                        {
                            aux = tablas.Find(x => x.nombre.Equals(s));
                            if (aux != null)
                            {
                                cargaIzquierdo(aux);
                                Atributo atri = aux.atributos.Find(x => x.foranea == nColumna);
                                if (atri != null)
                                {
                                    for (i = 0; i < izq.Rows.Count - 1; i++)
                                    {
                                        for (int j = 0; j < izq.Columns.Count; j++)
                                        {
                                            if (izq.Columns[j].HeaderText == atri.nombre && izq.Rows[i].Cells[j].Value.ToString() == clave.ToString())
                                            {
                                                modificaTupla.Enabled = false;
                                                eliminaTupla.Enabled = false;
                                                existe = true;
                                                break;
                                            }
                                        }
                                    }
                                }



                            }

                        }
                    }
                    if (existe == true)
                    {
                        modificaTupla.Enabled = false;
                        eliminaTupla.Enabled = false;
                    }
                    else
                    {
                        modificaTupla.Enabled = true;
                        eliminaTupla.Enabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("Error al cargar tabla");
                }
            }
            catch
            {

            }
            
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            validaIntegridadReferencial(e.RowIndex);
        }



        /// <summary>
        /// Carga los datos de la tabla en el datagrid
        /// </summary>
        /// <param name="t"></param>
        public void cargaTabla(Tabla t)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            registro.Rows.Clear();
            registro.Columns.Clear();
            if(t != null)
            {
                foreach (Atributo atributo in t.atributos)
                {
                    if (atributo != null)
                    {
                        grid.Columns.Add(atributo.nombre, atributo.nombre);
                        registro.Columns.Add(atributo.nombre, atributo.nombre);
                    }


                }
                //Carga en el datagrid
                foreach (string tupla in t.tuplas)
                    grid.Rows.Add(tupla.Split(','));
                //Tiene atributos
                if(t.atributos.Count > 0 )
                {
                    creaAtributo.Enabled = true;
                    modificaAtributo.Enabled = true;
                    eliminaAtributo.Enabled = true;
                }
                //No tiene atributos
                if(t.atributos.Count == 0)
                {
                    creaAtributo.Enabled = true;
                    modificaAtributo.Enabled = false;
                    eliminaAtributo.Enabled = false;
                }
                //Tiene tuplas
                if(t.tuplas.Count > 0)
                {
                    creaTupla.Enabled = true;
                    modificaTupla.Enabled = true;
                    eliminaTupla.Enabled = true;
                    eliminaAtributo.Enabled = false;
                    modificaAtributo.Enabled = false;
                }
                //No tiene tuplas
                if(t.tuplas.Count == 0)
                {
                    creaTupla.Enabled = true;
                    modificaTupla.Enabled = false;
                    eliminaTupla.Enabled = false;
                }
            }/*

            if (t != null && t.tuplas.Count == 0)
            {
                eliminaTupla.Enabled = false;
                modificaTupla.Enabled = false;
                eliminaAtributo.Enabled = false;
                modificaAtributo.Enabled = false;
            }
            else
            {
                eliminaTupla.Enabled = true;
                modificaTupla.Enabled = true;
                eliminaAtributo.Enabled = true;
                modificaAtributo.Enabled = true;
            }
            if(t != null)
                foreach (string tupla in t.tuplas)
                    grid.Rows.Add(tupla.Split(','));
                
            if(t!= null && t.atributos.Count > 0 && t.tuplas.Count == 0)
            {
                modificaAtributo.Enabled = true;
                eliminaAtributo.Enabled = true;
                creaTupla.Enabled = true;    
            }
            else if(t != null && t.atributos.Count > 0 && t.tuplas.Count > 0)
            {
                eliminaTupla.Enabled = false;
                modificaTupla.Enabled = false;

                modificaAtributo.Enabled = false;
                eliminaAtributo.Enabled = false;
            }
            */
        }
        #endregion

    }
}
