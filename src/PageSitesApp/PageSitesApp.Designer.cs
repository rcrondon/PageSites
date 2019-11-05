namespace PageSitesApp
{
    partial class PageSitesApp
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
            this.txt_listado_webpages = new System.Windows.Forms.TextBox();
            this.btn_conseguir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_listado_webpages
            // 
            this.txt_listado_webpages.Location = new System.Drawing.Point(12, 12);
            this.txt_listado_webpages.Multiline = true;
            this.txt_listado_webpages.Name = "txt_listado_webpages";
            this.txt_listado_webpages.Size = new System.Drawing.Size(279, 156);
            this.txt_listado_webpages.TabIndex = 0;
            this.txt_listado_webpages.TextChanged += new System.EventHandler(this.txt_listado_webpages_TextChanged);
            // 
            // btn_conseguir
            // 
            this.btn_conseguir.Location = new System.Drawing.Point(110, 186);
            this.btn_conseguir.Name = "btn_conseguir";
            this.btn_conseguir.Size = new System.Drawing.Size(75, 23);
            this.btn_conseguir.TabIndex = 1;
            this.btn_conseguir.Text = "Conseguir";
            this.btn_conseguir.UseVisualStyleBackColor = true;
            this.btn_conseguir.Click += new System.EventHandler(this.btn_conseguir_Click);
            // 
            // PageSitesApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 233);
            this.Controls.Add(this.btn_conseguir);
            this.Controls.Add(this.txt_listado_webpages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PageSitesApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manejador PageSites App";
            this.Load += new System.EventHandler(this.PageSitesApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_listado_webpages;
        private System.Windows.Forms.Button btn_conseguir;
    }
}

