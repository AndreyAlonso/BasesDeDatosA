using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_Datos.Clases
{
    [Serializable]
    public class Tabla
    {
        [NonSerialized]FileStream archivo;
        public string nombre { get; set; }
        public string extension;
        public List<Atributo> atributos { get; set; }

        public Tabla(string n)
        {
            nombre = n;
            extension = ".txt";
            atributos = new List<Atributo>();
        }
        /// <summary>
        /// AGREGAR TABLA A BASE DE DATOS
        /// crea SOLO el archivo en la carpeta de la base de datos
        /// </summary>
        /// <param name="directorio">Ubicación de la base de datos</param>
        public void agregaTabla(string directorio)
        {
            archivo = new FileStream(directorio + "\\" + nombre + extension,FileMode.Append);
            archivo.Close();                 
        }
        public void modificaNombre(string directorio, string nuevo)
        {
            File.Move(directorio + "\\" + nombre + extension, directorio + "\\" + nuevo + extension);
        }
        public void eliminaTabla(string directorio)
        {
            File.Delete(directorio + "\\" + nombre + extension);
        }
    }
}
