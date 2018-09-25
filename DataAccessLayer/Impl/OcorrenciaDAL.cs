using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Impl
{
    public class OcorrenciaDAL
    {
        public DbResponse Inserir(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand commandInsert = new SqlGenerator<Ocorrencia>().CreateInsertCommand(ocorrencia);
            commandInsert.Connection = conn;
            DbResponse response = new DbResponse();
            try
            {
                conn.Open();
                response.RowsAffected = commandInsert.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Operação realizada com sucesso!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            finally
            {
                conn.Dispose();
            }
            return response;
        }

        public List<Ocorrencia> LerTodos()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS";
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.ID = Convert.ToInt32(reader["ID"]);
                    ocorrencia.Descricao = (string)reader["DESCRICAO"];
                    ocorrencia.Lat = (string)reader["LATITUDE"];
                    ocorrencia.Lng = (string)reader["LONGITUDE"];
                    ocorrencia.Status = (Situacao)reader["SITUACAO"];
                    ocorrencia.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrencia.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrencia.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrencia);
                }
                return ocorrencias;
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

        public List<Ocorrencia> LerPrincipaisOcorrencias()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO ORDER BY CADASTROS DESC";
            command.Parameters.AddWithValue("@SITUACAO", Situacao.Aberto);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.ID = Convert.ToInt32(reader["ID"]);
                    ocorrencia.Descricao = (string)reader["DESCRICAO"];
                    ocorrencia.Lat = (string)reader["LATITUDE"];
                    ocorrencia.Lng = (string)reader["LONGITUDE"];
                    ocorrencia.Status = (Situacao)reader["SITUACAO"];
                    ocorrencia.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrencia.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrencia.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrencia);
                }
                return ocorrencias;
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

        public List<Ocorrencia> LerMaisRecentes()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO ORDER BY DATACADASTRO DESC";
            command.Parameters.AddWithValue("@SITUACAO", Situacao.Aberto);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.ID = Convert.ToInt32(reader["ID"]);
                    ocorrencia.Descricao = (string)reader["DESCRICAO"];
                    ocorrencia.Lat = (string)reader["LATITUDE"];
                    ocorrencia.Lng = (string)reader["LONGITUDE"];
                    ocorrencia.Status = (Situacao)reader["SITUACAO"];
                    ocorrencia.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrencia.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrencia.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrencia);
                }
                return ocorrencias;
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

        public List<Ocorrencia> LerTodos(int IDUsuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE USUARIO=@IDUSUARIO";
            command.Parameters.AddWithValue("@IDUSUARIO", IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.ID = Convert.ToInt32(reader["ID"]);
                    ocorrencia.Descricao = (string)reader["DESCRICAO"];
                    ocorrencia.Lat = (string)reader["LATITUDE"];
                    ocorrencia.Lng = (string)reader["LONGITUDE"];
                    ocorrencia.Status = (Situacao)reader["SITUACAO"];
                    ocorrencia.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrencia.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrencia.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrencia);
                }
                return ocorrencias;
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

        public List<Ocorrencia> LerTodasEmAberto()
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO";
            command.Parameters.AddWithValue("@SITUACAO", Situacao.Aberto);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrencia = new Ocorrencia();
                    ocorrencia.ID = Convert.ToInt32(reader["ID"]);
                    ocorrencia.Descricao = (string)reader["DESCRICAO"];
                    ocorrencia.Lat = (string)reader["LATITUDE"];
                    ocorrencia.Lng = (string)reader["LONGITUDE"];
                    ocorrencia.Status = (Situacao)reader["SITUACAO"];
                    ocorrencia.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrencia.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrencia.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrencia);
                }
                return ocorrencias;
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

        public List<Ocorrencia> LerTodasComum(Ocorrencia ocorrencia, List<string> pesquisas)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            for (int i = 0; i < pesquisas.Count; i++)
            {
                command.CommandText += @"SELECT * FROM OCORRENCIAS WHERE CATEGORIA=@CATEGORIA"+i+" AND DESCRICAO LIKE @PESQUISA"+i+" AND SITUACAO=@SITUACAO"+i;
                command.Parameters.AddWithValue("@PESQUISA"+i, string.Format("%{0}%", pesquisas[i]));
                command.Parameters.AddWithValue("@CATEGORIA"+i, ocorrencia.IDCategoria);
                command.Parameters.AddWithValue("@SITUACAO"+i, Situacao.Aberto);

                if(i != pesquisas.Count - 1)
                {
                    command.CommandText += " UNION ";
                }
            }

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia ocorrence = new Ocorrencia();
                    ocorrence.ID = Convert.ToInt32(reader["ID"]);
                    ocorrence.Descricao = (string)reader["DESCRICAO"];
                    ocorrence.Lat = (string)reader["LATITUDE"];
                    ocorrence.Lng = (string)reader["LONGITUDE"];
                    ocorrence.Status = (Situacao)reader["SITUACAO"];
                    ocorrence.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    ocorrence.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    ocorrence.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    ocorrencia.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(ocorrence);
                }
                return ocorrencias;
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

        public DbResponse AtualizarCadastros(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            //SqlCommand commandUpdate = new SqlGenerator<Usuario>().CreateUpdateCommand(usuario);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                "UPDATE OCORRENCIAS SET CADASTROS=@CADASTROS WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", ocorrencia.ID);
            command.Parameters.AddWithValue("@CADASTROS", ocorrencia.Cadastros);
            DbResponse response = new DbResponse();
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                response.RowsAffected = command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Operação realizada com sucesso!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            finally
            {
                conn.Dispose();
            }
            return response;
        }

        public DbResponse AtualizarStatus(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            //SqlCommand commandUpdate = new SqlGenerator<Usuario>().CreateUpdateCommand(usuario);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                "UPDATE OCORRENCIAS SET SITUACAO=@SITUACAO WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", ocorrencia.ID);
            command.Parameters.AddWithValue("@SITUACAO", ocorrencia.Status);
            DbResponse response = new DbResponse();
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                response.RowsAffected = command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Operação realizada com sucesso!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            finally
            {
                conn.Dispose();
            }
            return response;
        }

        public List<Ocorrencia> RetornarOcPorStatusCategoria(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO AND CATEGORIA=@CATEGORIA";
            command.Parameters.AddWithValue("@SITUACAO", ocorrencia.Status);
            command.Parameters.AddWithValue("@CATEGORIA", ocorrencia.IDCategoria);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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

        public List<Ocorrencia> RetornarOcPorStatusCategoriaUsrConectado(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO AND CATEGORIA=@CATEGORIA AND USUARIO=@IDUSUARIO";
            command.Parameters.AddWithValue("@SITUACAO", ocorrencia.Status);
            command.Parameters.AddWithValue("@CATEGORIA", ocorrencia.IDCategoria);
            command.Parameters.AddWithValue("@IDUSUARIO", ocorrencia.IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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

        public List<Ocorrencia> RetornarTodasOcPorCategoria(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE CATEGORIA=@CATEGORIA";
            command.Parameters.AddWithValue("@CATEGORIA", ocorrencia.IDCategoria);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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

        public List<Ocorrencia> RetornarTodasOcPorCategoriaUsrConectado(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE CATEGORIA=@CATEGORIA AND USUARIO=@IDUSUARIO";
            command.Parameters.AddWithValue("@CATEGORIA", ocorrencia.IDCategoria);
            command.Parameters.AddWithValue("@IDUSUARIO", ocorrencia.IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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

        public List<Ocorrencia> RetornarTodasOcPorStatus(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO";
            command.Parameters.AddWithValue("@SITUACAO", ocorrencia.Status);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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

        public List<Ocorrencia> RetornarTodasOcPorStatusUsrConectado(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM OCORRENCIAS WHERE SITUACAO=@SITUACAO AND USUARIO=@IDUSUARIO";
            command.Parameters.AddWithValue("@SITUACAO", ocorrencia.Status);
            command.Parameters.AddWithValue("@IDUSUARIO", ocorrencia.IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
                while (reader.Read())
                {
                    Ocorrencia oc = new Ocorrencia();
                    oc.ID = Convert.ToInt32(reader["ID"]);
                    oc.Descricao = (string)reader["DESCRICAO"];
                    oc.Lat = (string)reader["LATITUDE"];
                    oc.Lng = (string)reader["LONGITUDE"];
                    oc.Status = (Situacao)reader["SITUACAO"];
                    oc.IDCategoria = Convert.ToInt32(reader["CATEGORIA"]);
                    oc.IDUsuario = Convert.ToInt32(reader["USUARIO"]);
                    oc.Cadastros = Convert.ToInt32(reader["CADASTROS"]);
                    oc.DataCadastro = (DateTime)reader["DATACADASTRO"];
                    ocorrencia.Endereco = (string)reader["ENDERECO"];
                    ocorrencias.Add(oc);
                }
                return ocorrencias;
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
