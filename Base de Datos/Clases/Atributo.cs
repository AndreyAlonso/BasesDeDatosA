using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_de_Datos.Clases
{
    [Serializable]
    public class Atributo
    {
        public string nombre { get; set; }
        public char tipo { get; set; }
        public int tam { get; set; }
        public string indice { get; set; }
        
        public Atributo()
        {

        }
    }
}
