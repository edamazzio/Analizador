namespace WindowsFormsApplication1
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkBxSettings = new System.Windows.Forms.CheckedListBox();
            this.lblIndicadores = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkBxSettings
            // 
            this.chkBxSettings.BackColor = System.Drawing.SystemColors.Control;
            this.chkBxSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkBxSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxSettings.FormattingEnabled = true;
            this.chkBxSettings.Items.AddRange(new object[] {
            "Cantidad de ciclos anidados",
            "Máximo nivel de anidamiento de los ciclos",
            "Cantidad de instrucciones dentro de ciclos",
            "Cantidad de funciones definidas",
            "Cantidad de funciones recursivas simples",
            "Cantidad de funciones recursivas múltiples",
            "Cantidad de instrucciones dentro de funciones recursivas",
            "Cantidad de instrucciones totales del programa",
            "Cantidad de instrucciones en comentario",
            "Cantidad de instrucciones NO dentro de funciones"});
            this.chkBxSettings.Location = new System.Drawing.Point(12, 42);
            this.chkBxSettings.Margin = new System.Windows.Forms.Padding(0);
            this.chkBxSettings.Name = "chkBxSettings";
            this.chkBxSettings.Size = new System.Drawing.Size(556, 362);
            this.chkBxSettings.TabIndex = 0;
            this.chkBxSettings.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // lblIndicadores
            // 
            this.lblIndicadores.AutoSize = true;
            this.lblIndicadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndicadores.Location = new System.Drawing.Point(13, 11);
            this.lblIndicadores.Name = "lblIndicadores";
            this.lblIndicadores.Size = new System.Drawing.Size(139, 29);
            this.lblIndicadores.TabIndex = 1;
            this.lblIndicadores.Text = "Indicadores";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 419);
            this.Controls.Add(this.lblIndicadores);
            this.Controls.Add(this.chkBxSettings);
            this.Name = "frmSettings";
            this.Text = "Configuración";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkBxSettings;
        private System.Windows.Forms.Label lblIndicadores;
    }
}