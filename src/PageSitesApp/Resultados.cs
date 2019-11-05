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
    public partial class Resultados : Form
    {
        private static Dictionary<string, Dictionary<string, Dictionary<string, object>>> datos { get; set; }
        public Resultados(Dictionary<string, Dictionary<string, Dictionary<string, object>>> data)
        {
            datos = data;

            TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();

            string html_body = "";

            foreach (var pagina in datos)
            {
                html_body += "<div style='text-align='center''><h1>" + "Reporte de '" + pagina.Key + "'" + "</h1></div><br>";

                foreach (var metricas in pagina.Value)
                {
                    html_body += "<h2 style='text-align='center''>" + "Datos de '" + metricas.Key + "'</h2><br>";

                    foreach (var campo in metricas.Value)
                        html_body += "<b>" + campo.Key + ": </b>" + campo.Value + "<br>";
                }
            }

            htmlPanel.Text = html_body;
            htmlPanel.Dock = DockStyle.Fill;
            Controls.Add(htmlPanel);
            
            InitializeComponent();
        }


        private void btn_exportar_excel_Click(object sender, EventArgs e)
        {
            SaveFileDialog ruta = new SaveFileDialog();

            ruta.InitialDirectory = "\\C";
            ruta.RestoreDirectory = true;
            ruta.Title = "Seleccionar la ruta donde guardará el Reporte";
            ruta.DefaultExt = "xlsx";
            ruta.FileName = "Reporte " + DateTime.Now.ToString("dd-MM-yyyy");
            ruta.Filter = "Archivo Hoja de Calculo (*.xlsx)|*.xlsx";
            ruta.CheckPathExists = true;

            bool check = ruta.ShowDialog() == DialogResult.OK;

            if (check)
            {
                ManejadorPageSites manejador = new ManejadorPageSites();
                bool finished = manejador.exportar_excel(datos, ruta.FileName);

                if(finished)
                    MessageBox.Show("Se generó el reporte correctamente", "Reporte Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Ocurrió un error durante el generado del reporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void Resultados_Load(object sender, EventArgs e)
        {
            
        }
    }
}
