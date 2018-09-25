using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class CategoriaDAL
    {
        public List<Categoria> LerTodos()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM CATEGORIAS";
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Categoria> categorias = new List<Categoria>();
                while (reader.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = (string)reader["NOME"];
                    categoria.Descricao = (string)reader["DESCRICAO"];
                    categorias.Add(categoria);
                }
                return categorias;
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

        public Categoria GetByID(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM CATEGORIAS WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", ocorrencia.IDCategoria);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Categoria categoria = new Categoria();
                if (reader.Read())
                {
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = (string)reader["NOME"];
                    categoria.Descricao = (string)reader["DESCRICAO"];
                    return categoria;
                }
                return null;
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

        public Categoria GetByID(Sac sac)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM CATEGORIAS WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", sac.IDCategoria);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Categoria categoria = new Categoria();
                if (reader.Read())
                {
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    categoria.Nome = (string)reader["NOME"];
                    categoria.Descricao = (string)reader["DESCRICAO"];
                    return categoria;
                }
                return null;
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
