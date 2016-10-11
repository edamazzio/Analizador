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
        public List<bool> opcionesConfiguracion { get; set; }

        public frmSettings(List<bool> list)
        {
            InitializeComponent();
            opcionesConfiguracion = list;
            setCheckedListBox1();
        }
        /// <summary>
        /// settea el estado de los checkboxes al abrir la ventana
        /// segun el atributo opcionesConfiguracion
        /// </summary>
        private void setCheckedListBox1(){
            for (int i = 0; i < this.chkBxSettings.Items.Count; i++)
            {
                if (opcionesConfiguracion[i] == false)
                {
                    //this.chkBxSettings.SetItemCheckState(i, CheckState.Unchecked);
                    this.chkBxSettings.SetItemChecked(i, false);
                }
                else
                {
                    //this.chkBxSettings.SetItemCheckState(i, CheckState.Checked);
                    this.chkBxSettings.SetItemChecked(i, true);
                }
            }
        }
        /// <summary>
        /// recoje el estado de los checkboxes al cerrarse la venta
        /// guarda los estados en el atributo opcionesConfiguracion
        /// </summary>
        private void getCheckedListBox1()
        {
            for (int i = 0; i < this.chkBxSettings.Items.Count; i++)
            {
                if (this.chkBxSettings.GetItemChecked(i) == false)
                {
                    opcionesConfiguracion[i] = false;
                }
                else
                {
                    opcionesConfiguracion[i] = true;
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void frmSettings_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// botton de guardar: devuelve la lista de estados de los checkboxes al form padre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            getCheckedListBox1();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
