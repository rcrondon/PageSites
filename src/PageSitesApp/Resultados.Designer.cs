namespace PageSitesApp
{
    partial class Resultados
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
            this.btn_exportar_excel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_exportar_excel
            // 
            this.btn_exportar_excel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_exportar_excel.Location = new System.Drawing.Point(0, 483);
            this.btn_exportar_excel.Name = "btn_exportar_excel";
            this.btn_exportar_excel.Size = new System.Drawing.Size(752, 23);
            this.btn_exportar_excel.TabIndex = 1;
            this.btn_exportar_excel.Text = "Exportar Excel";
            this.btn_exportar_excel.UseVisualStyleBackColor = true;
            this.btn_exportar_excel.Click += new System.EventHandler(this.btn_exportar_excel_Click);
            // 
            // Resultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 506);
            this.Controls.Add(this.btn_exportar_excel);
            this.Name = "Resultados";
            this.Text = "Resultados";
            this.Load += new System.EventHandler(this.Resultados_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_exportar_excel;
    }
}