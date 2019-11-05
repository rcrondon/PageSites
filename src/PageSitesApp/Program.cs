using PageSitesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PageSitesApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Dictionary<string, Dictionary<string, Dictionary<string, object>>> datos { get; set; }
        [STAThread]
        static void Main()
        {
            //ManejadorPageSites manejador = new ManejadorPageSites();
            //var listado = new List<string> { "https://unapec.edu.do", "https://google.com"/*, "https://wikipedia.com", "https://alibaba.com", "https://aliexpress.com"*/ };
            //datos = manejador.obtener_datos(listado);
            //manejador.exportar_excel(datos, @"D:\test.xlsx");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PageSitesApp());
        }
    }
}
