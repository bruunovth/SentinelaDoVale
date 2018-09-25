using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPresentationLayer.Models
{
    public class OcorrenciaViewModel
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int IDCategoria { get; set; }
        public string Descricao { get; set; }
    }
}