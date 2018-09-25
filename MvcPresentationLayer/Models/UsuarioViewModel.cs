using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPresentationLayer.Models
{
    public class UsuarioViewModel
    {
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Pontos { get; set; }
        public Cargo Funcao { get; set; }
    }
}