using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmSettings : Form
    {
        List<bool> opcionesConfiguracion = new List<bool>()
        {
            {true},{true},{true},{true},{true},{true},{true},{true},{true},{true}
        };

        public frmSettings(List<bool> list)
        {
            InitializeComponent();
            opcionesConfiguracion = list;
            setCheckedListBox1();
        }

        private void setCheckedListBox1(){
            for (int i = 0; i < this.chkBxSettings.Items.Count; i++)
            {
                if (opcionesConfiguracion[i] == false)
                {
                    this.chkBxSettings.SetItemCheckState(i, CheckState.Unchecked);
                    this.chkBxSettings.SetItemChecked(i, false);
                }
                else
                {
                    this.chkBxSettings.SetItemCheckState(i, CheckState.Checked);
                    this.chkBxSettings.SetItemChecked(i, true);
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
