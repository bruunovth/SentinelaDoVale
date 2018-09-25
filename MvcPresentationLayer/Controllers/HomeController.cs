using BusinessLogicalLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            StringBuilder markers = new StringBuilder();
            try
            {
                List<Ocorrencia> todasOcorrencias = ocorrenciaBLL.LerTodasEmAberto();
                markers.Append("[");
                foreach (Ocorrencia ocorrencia in todasOcorrencias)
                {
                    ocorrencia.Lat = ocorrencia.Lat.Replace(",", ".");
                    ocorrencia.Lng = ocorrencia.Lng.Replace(",", ".");
                    markers.Append("{");
                    markers.Append(string.Format("'lat': '{0}',", ocorrencia.Lat));
                    markers.Append(string.Format("'lng': '{0}',", ocorrencia.Lng));
                    markers.Append(string.Format("'description': '{0}',", ocorrencia.Descricao));
                    markers.Append(string.Format("'title': '{0}'", categoriaBLL.GetByID(ocorrencia).Nome));
                    markers.Append("},");
                }
                markers.Append("];");
                ViewBag.Markers = markers.ToString();
            }
            catch
            {
                
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}