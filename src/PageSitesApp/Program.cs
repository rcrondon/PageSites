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
        [STAThread]
        static void Main()
        {
            //ManejadorPageSites manejador = new ManejadorPageSites();
            //var listado = new List<string> { "https://unapec.edu.do", "https://google.com", "https://wikipedia.com", "https://alibaba.com", "https://aliexpress.com" };
            //manejador.obtener_datos(listado);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
