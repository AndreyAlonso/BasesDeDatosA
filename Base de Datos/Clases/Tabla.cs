using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_Datos.Clases
{
    public class Tabla
    {
        public string nombre { get; set; }
        private string extension;

        public Tabla(string n)
        {
            nombre = n;
            extension = ".txt";
        }
        public void agregaTabla(string directorio)
        {
            File.Create(directorio + "\\" + nombre + extension);
        }
    }
}
