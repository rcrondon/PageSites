using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PageSitesLib
{
    public class ManejadorPageSites
    {
        public Dictionary<string, Dictionary<string, object>> obtener_datos(List<string> listado)
        {
            //var url = "http://html-agility-pack.net/";
            //var web = new HtmlWeb();
            //var doc = web.Load(url);

            var diccionario_datos = new Dictionary<string, Dictionary<string, object>>();

            foreach (var pagina in listado)
            {
                diccionario_datos.Add("PageSpeed", PageSpeed(pagina));
            }
            
            return diccionario_datos;
        }

        //Paginas REST consultadas
        public Dictionary<string, object> PageSpeed(string url)
        {
            var diccionario_tmp = new Dictionary<string, object>();

            var json = new WebClient().DownloadString("https://www.googleapis.com/pagespeedonline/v4/runPagespeed?url=" + url);
            var jss = new JavaScriptSerializer();
            var table = jss.Deserialize<dynamic>(json);

            diccionario_tmp.Add("url", table["id"]);
            diccionario_tmp.Add("captchaResult", table["captchaResult"]);
            diccionario_tmp.Add("median_DOM", table["loadingExperience"]["metrics"]["DOM_CONTENT_LOADED_EVENT_FIRED_MS"]["median"]);
            diccionario_tmp.Add("score", table["ruleGroups"]["SPEED"]["score"]);
            diccionario_tmp.Add("overall_category", table["loadingExperience"]["overall_category"]);

            return diccionario_tmp;

        }
    }
}
