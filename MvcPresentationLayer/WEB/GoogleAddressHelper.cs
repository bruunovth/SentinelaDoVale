using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace MvcPresentationLayer.WEB
{
    public class GoogleAddressHelper
    {

        string baseUri = "https://maps.googleapis.com/maps/api/" +
                              "geocode/xml?latlng={0},{1}&key=AIzaSyCC_FC_GMBKSepZusci_BgPRKz1vANdFXs";

        public string RetrieveFormatedAddress(Ocorrencia ocorrencia)
        {
            string lat = ocorrencia.Lat.Replace(",", ".");
            string lng = ocorrencia.Lng.Replace(",", ".");
            string requestUri = string.Format(baseUri, lat, lng);
            using (WebClient wc = new WebClient())
            {
                var xmlElm = XElement.Parse(wc.DownloadString(new Uri(requestUri)));

                var status = (from elm in xmlElm.Descendants()
                              where elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    return res.Value;
                }
                else
                {
                    return "Não encontrado.";
                }
            }
        }
    }
}