using PageSitesLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PageSitesWeb
{
    public partial class _Default : Page
    {
        private static Dictionary<string, Dictionary<string, Dictionary<string, object>>> datos { get; set; }
        public static string html_body { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && txt_listado.Text.Length > 0)
            {
                ManejadorPageSites manejador = new ManejadorPageSites();
                datos = manejador.obtener_datos(txt_listado.Text);

                html_body = "";
                foreach (var pagina in datos)
                {
                    html_body += "<table border='1'><caption><b>" + "Reporte de '" + pagina.Key + "'" + "</b></caption>";

                    foreach (var metricas in pagina.Value)
                    {
                        html_body += "<thead><tr><th colspan='4'>" + "Datos de '" + metricas.Key + "'" + " </th></tr></thead><tbody>";

                        foreach (var campo in metricas.Value)
                            html_body += "<tr><th>" + campo.Key + "</th><td>" + campo.Value + "</td></tr>";
                    }

                    html_body += "</tbody></table><br><br>";
                }
            }
        }

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            if (datos != null)
            {
                string nombre_archivo = "Reporte " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                ManejadorPageSites manejador = new ManejadorPageSites();
                string path = Server.MapPath("~/" + nombre_archivo);
                bool finished = manejador.exportar_excel(datos, path);

                if (finished)
                {
                    byte[] Content = File.ReadAllBytes(path);
                    Response.ContentType = "text/csv";
                    Response.AddHeader("content-disposition", "attachment; filename=" + nombre_archivo);
                    Response.BufferOutput = true;
                    Response.OutputStream.Write(Content, 0, Content.Length);
                    Response.End();
                }
            }
        }
    }
}