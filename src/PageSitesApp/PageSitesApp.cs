using PageSitesLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PageSitesApp
{
    public partial class PageSitesApp : Form
    {
        public PageSitesApp()
        {
            InitializeComponent();
        }

        private void PageSitesApp_Load(object sender, EventArgs e)
        {
            btn_conseguir.Enabled = false;
        }

        private void txt_listado_webpages_TextChanged(object sender, EventArgs e)
        {
            if (txt_listado_webpages.Text.Length > 0)
                btn_conseguir.Enabled = true;
            else
                btn_conseguir.Enabled = false;
        }

        private void btn_conseguir_Click(object sender, EventArgs e)
        {
            try
            {
                ManejadorPageSites manejador = new ManejadorPageSites();
                var datos = manejador.obtener_datos(txt_listado_webpages.Text);

                MessageBox.Show("Se realizó la consulta correctamente", "Consulta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Resultados resultado = new Resultados(datos);
                resultado.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error durante la consulta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
