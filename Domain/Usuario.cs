using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [EntityTableName("USUARIOS")]
    public class Usuario
    {
        public int ID { get; set; }
        public string Username { get; set; }
        [ColumnName("Senha")]
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Pontos { get; set; }
        [ColumnName("Cargo")]
        public Cargo Funcao { get; set; }
    }
}
