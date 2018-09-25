using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [EntityTableName("OCORRENCIAS")]
    public class Ocorrencia
    {
        public int ID { get; set; }
        [ColumnName("Latitude")]
        public string Lat { get; set; }
        [ColumnName("Longitude")]
        public string Lng { get; set; }
        [ColumnName("Usuario")]
        public int IDUsuario { get; set; }
        [ColumnName("Categoria")]
        public int IDCategoria { get; set; }
        public string Descricao { get; set; }
        [ColumnName("Situacao")]
        public Situacao Status { get; set; }
        public int Cadastros { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Endereco { get; set; }
    }
}
