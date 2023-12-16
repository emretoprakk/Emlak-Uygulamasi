using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emlak
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        

        private void AnaMenu_Load(object sender, EventArgs e)
        {

        }

        private void button_Ev_Ekleme_Click(object sender, EventArgs e)
        {
            EvEkleme fr = new EvEkleme();
            fr.Show();
            
        }

        private void button_Ev_Sorgulama_Click(object sender, EventArgs e)
        {
            EvListelemeSorgulama fr = new EvListelemeSorgulama();
            fr.Show();
        }
    }
}
