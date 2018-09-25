using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPresentationLayer.Models
{
    public class TodasOcorrenciasViewModel
    {
        public int ID { get; set; }
        public Situacao Status { get; set; }
        public int IDCategoria { get; set; }
    }
}