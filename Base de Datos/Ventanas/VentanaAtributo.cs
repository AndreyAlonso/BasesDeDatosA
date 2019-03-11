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
    public partial class VentanaAtributo : Form
    {
        public VentanaAtributo()
        {
            InitializeComponent();
            comboTipo.Items.Add("E");
            comboTipo.Items.Add("F");
            comboTipo.Items.Add("C");
            comboClave.Items.Add("Sin clave");
            comboClave.Items.Add("Clave Primaria");
            comboClave.Items.Add("Clave Foranea");
        }
    }
}
