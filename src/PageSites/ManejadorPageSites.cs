using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PageSitesLib
{
    public class ManejadorPageSites
    {
        private static WebClient wc { get; set; }
        public ManejadorPageSites()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            wc = new WebClient();
            wc.Proxy = WebRequest.DefaultWebProxy;
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> obtener_datos(string txt_listado)
        {
            var diccionario_datos = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

            List<string> listado = txt_listado.Replace("\n", "").Split('\r').Select(s => s.StartsWith("http://www.") ? s : s.StartsWith("http://") ? s : s.StartsWith("https://www.") ? s : s.StartsWith("https://") ? s : "https://" + s).ToList();

            foreach (var pagina in listado)
            {
                var diccionario_paginas = new Dictionary<string, Dictionary<string, object>>();
                diccionario_paginas.Add("Google PageSpeed", PageSpeed(pagina));
                diccionario_paginas.Add("check-host.net", checkhost(pagina));

                var datos_certspotter = certspotter(pagina);
                if (datos_certspotter.Values.Count > 0)
                    diccionario_paginas.Add("certspotter", datos_certspotter);

                var datos_observatorysecurity = observatorysecurity(pagina);
                if (datos_observatorysecurity.Values.Count > 0)
                    diccionario_paginas.Add("observatory.security.mozilla.org", datos_observatorysecurity);

                diccionario_datos.Add(pagina, diccionario_paginas);
            }

            return diccionario_datos;
        }

        public bool exportar_excel(Dictionary<string, Dictionary<string, Dictionary<string, object>>> datos, string ruta_guardado)
        {
            try
            {
                Application wapp = new Application();
                Worksheet wsheet;
                Workbook wbook;
                Range rng;

                wapp.Visible = false;
                wapp.DisplayAlerts = false;
                wapp.UserControl = true;

                wbook = wapp.Workbooks.Add(true);
                wsheet = (Worksheet)wbook.ActiveSheet;

                wsheet = (Worksheet)wbook.Worksheets.get_Item(1);

                int columna = 2;
                foreach (var pagina in datos)
                {
                    wsheet.Cells[columna, 2] = "Reporte de '" + pagina.Key + "'";
                    rng = wapp.Sheets[1].Range[wapp.Sheets[1].Cells[columna, 2], wapp.Sheets[1].Cells[columna, 3]];
                    rng.Merge();

                    wapp.Sheets[1].Range[wapp.Sheets[1].Cells[1, 1], wapp.Sheets[1].Cells[columna, 3]].Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    wsheet.Cells[columna, 2].EntireRow.Font.Bold = true;
                    wsheet.Cells[columna, 2].EntireRow.Font.Size = 16;

                    foreach (var metricas in pagina.Value)
                    {
                        columna = columna + 1;
                        wsheet.Cells[columna, 2] = "Datos de '" + metricas.Key + "'";

                        rng = wapp.Sheets[1].Range[wapp.Sheets[1].Cells[columna, 2], wapp.Sheets[1].Cells[columna, 3]];
                        rng.Merge();

                        wapp.Sheets[1].Range[wapp.Sheets[1].Cells[1, 1], wapp.Sheets[1].Cells[columna, 3]].Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        wsheet.Cells[columna, 2].EntireRow.Font.Bold = true;
                        wsheet.Cells[columna, 2].EntireRow.Font.Size = 14;
                        wsheet.Cells[columna, 2].EntireRow.Font.Underline = true;

                        foreach (var campo in metricas.Value)
                        {
                            columna = columna + 1;

                            wsheet.Cells[columna, 2] = campo.Key;
                            wsheet.Cells[columna, 3] = campo.Value;
                        }
                    }

                    columna = columna + 1;
                    wsheet.Cells[columna, 2] = "";
                    columna = columna + 1;
                }

                wsheet.Columns["B"].ColumnWidth = 50.0;
                wsheet.Columns["C"].ColumnWidth = 80.0;

                wsheet.Columns["C"].Style.WrapText = true;

                wbook.SaveAs(ruta_guardado, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                wbook.Close(true);
                wapp.Quit();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }

        //Paginas REST consultadas
        private Dictionary<string, object> PageSpeed(string url)
        {
            var diccionario_tmp = new Dictionary<string, object>();

            try
            {
                var json = wc.DownloadString("https://www.googleapis.com/pagespeedonline/v4/runPagespeed?url=" + url);
                var jss = new JavaScriptSerializer();
                var table = jss.Deserialize<dynamic>(json);

                diccionario_tmp.Add("url", table["id"]);
                diccionario_tmp.Add("captchaResult", table["captchaResult"]);
                diccionario_tmp.Add("median_DOM", table["loadingExperience"]["metrics"]["DOM_CONTENT_LOADED_EVENT_FIRED_MS"]["median"]);
                diccionario_tmp.Add("score", table["ruleGroups"]["SPEED"]["score"]);
                diccionario_tmp.Add("overall_category", table["loadingExperience"]["overall_category"]);
            }
            catch (Exception)
            {

            }


            return diccionario_tmp;

        }

        private Dictionary<string, object> certspotter(string url)
        {
            var diccionario_tmp = new Dictionary<string, object>();

            try
            {
                if (url.StartsWith("http://") || url.StartsWith("https://"))
                {
                    Uri uri = new Uri(url);
                    url = uri.Host;
                }

                var json = wc.DownloadString("https://api.certspotter.com/v1/issuances?domain=" + url + "&expand=dns_names&expand=issuer&expand=cert");
                var jss = new JavaScriptSerializer();
                var table = jss.Deserialize<dynamic>(json);

                diccionario_tmp.Add("dns_names", string.Join(", ", table[0]["dns_names"]));
                diccionario_tmp.Add("pubkey_sha256", table[0]["pubkey_sha256"]);
            }
            catch (Exception ex)
            {

            }

            return diccionario_tmp;

        }

        private Dictionary<string, object> observatorysecurity(string url)
        {
            var diccionario_tmp = new Dictionary<string, object>();

            try
            {
                if (url.StartsWith("http://") || url.StartsWith("https://"))
                {
                    Uri uri = new Uri(url);
                    url = uri.Host;
                }

                var json = wc.DownloadString("https://http-observatory.security.mozilla.org/api/v1/analyze?host=" + url);
                var jss = new JavaScriptSerializer();
                var table = jss.Deserialize<dynamic>(json);

                diccionario_tmp.Add("grade", table["grade"]);
                diccionario_tmp.Add("score", table["score"]);
                diccionario_tmp.Add("likelihood_indicator", table["likelihood_indicator"]);
                diccionario_tmp.Add("Server", table["response_headers"]["Server"]);
                diccionario_tmp.Add("Set-Cookie", table["response_headers"]["Set-Cookie"]);
            }
            catch (Exception)
            {

            }

            return diccionario_tmp;

        }


        //Paginas donde se buscan con bosquejo
        private Dictionary<string, object> checkhost(string url)
        {
            var diccionario_tmp = new Dictionary<string, object>();

            try
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(wc.DownloadString("https://check-host.net/ip-info?host=" + url));

                var datos1 = htmlDoc.DocumentNode.SelectNodes("//tr[@class='zebra']/td").Where(z => z.EndNode.Name == "td").Select(y => y.InnerText).Take(12).ToArray();
                var datos2 = htmlDoc.DocumentNode.SelectNodes("//tr[@class='zebra']/td").Where(z => z.EndNode.Name == "td").Select(y => y.InnerText).Skip(12).Take(12).ToArray();
                var datos3 = htmlDoc.DocumentNode.SelectNodes("//tr[@class='zebra']/td").Where(z => z.EndNode.Name == "td").Select(y => y.InnerText).Skip(24).Take(12).ToArray();

                var diccionario_tmp1 = Enumerable.Range(0, datos1.Length / 2).ToDictionary(i => datos1[2 * i], i => (object)datos1[2 * i + 1]);
                var diccionario_tmp2 = Enumerable.Range(0, datos2.Length / 2).ToDictionary(i => datos2[2 * i], i => (object)datos2[2 * i + 1]);
                var diccionario_tmp3 = Enumerable.Range(0, datos3.Length / 2).ToDictionary(i => datos3[2 * i], i => (object)datos3[2 * i + 1]);

                diccionario_tmp = diccionario_tmp1.Concat(diccionario_tmp2).Concat(diccionario_tmp3).ToLookup(x => x.Key, x => x.Value).ToDictionary(x => x.Key, g => (object)string.Join(", ", g));
            }
            catch (Exception ex)
            {

            }

            return diccionario_tmp;

        }


    }
}
