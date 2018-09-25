using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SacDAL
    {
        public List<Sac> LerTodos()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM SACS";
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Sac> sacs = new List<Sac>();
                while (reader.Read())
                {
                    Sac sac = new Sac();
                    sac.ID = Convert.ToInt32(reader["ID"]);
                    sac.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    sac.Descricao = (string)reader["DESCRICAO"];
                    sac.Email = (string)reader["EMAIL"];
                    sac.Endereco = (string)reader["ENDERECO"];
                    sac.Telefone = (string)reader["TELEFONE"];
                    sacs.Add(sac);
                }
                return sacs;
            }
            catch (Exception)
            {
                throw new SDVException("Erro no DB, contate o administrador.");
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
