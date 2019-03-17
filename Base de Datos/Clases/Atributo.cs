using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_Datos.Clases
{
    public class Atributo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int tipo { get; set; }
        public int tam { get; set; }
        public string indice { get; set; }
        
        public Atributo()
        {

        }
    }
}
