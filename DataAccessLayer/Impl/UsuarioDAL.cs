using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UsuarioDAL
    {
        public Usuario Autenticar(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM USUARIOS WHERE USERNAME = @USERNAME AND SENHA = @PASSWORD";
            command.Parameters.AddWithValue("@USERNAME", usuario.Username);
            command.Parameters.AddWithValue("@PASSWORD", usuario.Password);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Usuario usr = new Usuario();
                if (reader.Read())
                {
                    usr.ID = Convert.ToInt32(reader["ID"]);
                    usr.Username = (string)reader["USERNAME"];
                    usr.Password = (string)reader["SENHA"];
                    usr.Nome = (string)reader["NOME"];
                    usr.DataNascimento = (DateTime)reader["DATANASCIMENTO"];
                    usr.Telefone = (string)reader["TELEFONE"];
                    usr.Email = (string)reader["EMAIL"];
                    usr.Pontos = Convert.ToInt32(reader["PONTOS"]);
                    usr.Funcao = (Cargo)reader["CARGO"];
                    return usr;
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

        public Usuario GetByID(int IDUsuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM USUARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Usuario usr = new Usuario();
                if (reader.Read())
                {
                    usr.ID = Convert.ToInt32(reader["ID"]);
                    usr.Username = (string)reader["USERNAME"];
                    usr.Password = (string)reader["SENHA"];
                    usr.Nome = (string)reader["NOME"];
                    usr.DataNascimento = (DateTime)reader["DATANASCIMENTO"];
                    usr.Telefone = (string)reader["TELEFONE"];
                    usr.Email = (string)reader["EMAIL"];
                    usr.Pontos = Convert.ToInt32(reader["PONTOS"]);
                    usr.Funcao = (Cargo)reader["CARGO"];
                    return usr;
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

        public Usuario GetByID(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM USUARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", usuario.ID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Usuario usr = new Usuario();
                if (reader.Read())
                {
                    usr.ID = Convert.ToInt32(reader["ID"]);
                    usr.Username = (string)reader["USERNAME"];
                    usr.Password = (string)reader["SENHA"];
                    usr.Nome = (string)reader["NOME"];
                    usr.DataNascimento = (DateTime)reader["DATANASCIMENTO"];
                    usr.Telefone = (string)reader["TELEFONE"];
                    usr.Email = (string)reader["EMAIL"];
                    usr.Pontos = Convert.ToInt32(reader["PONTOS"]);
                    usr.Funcao = (Cargo)reader["CARGO"];
                    return usr;
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

        public Usuario GetByID(Ocorrencia ocorrencia)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM USUARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", ocorrencia.IDUsuario);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Usuario usr = new Usuario();
                if (reader.Read())
                {
                    usr.ID = Convert.ToInt32(reader["ID"]);
                    usr.Username = (string)reader["USERNAME"];
                    usr.Password = (string)reader["SENHA"];
                    usr.Nome = (string)reader["NOME"];
                    usr.DataNascimento = (DateTime)reader["DATANASCIMENTO"];
                    usr.Telefone = (string)reader["TELEFONE"];
                    usr.Email = (string)reader["EMAIL"];
                    usr.Pontos = Convert.ToInt32(reader["PONTOS"]);
                    usr.Funcao = (Cargo)reader["CARGO"];
                    return usr;
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

        public DbResponse Inserir(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand commandInsert = new SqlGenerator<Usuario>().CreateInsertCommand(usuario);
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

        public Usuario GetByUsername(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM USUARIOS WHERE USERNAME = @USERNAME";
            command.Parameters.AddWithValue("@USERNAME", usuario.Username);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                Usuario usr = new Usuario();
                if (reader.Read())
                {
                    usr.ID = Convert.ToInt32(reader["ID"]);
                    usr.Username = (string)reader["USERNAME"];
                    usr.Password = (string)reader["SENHA"];
                    usr.Nome = (string)reader["NOME"];
                    usr.DataNascimento = (DateTime)reader["DATANASCIMENTO"];
                    usr.Telefone = (string)reader["TELEFONE"];
                    usr.Email = (string)reader["EMAIL"];
                    usr.Pontos = Convert.ToInt32(reader["PONTOS"]);
                    usr.Funcao = (Cargo)reader["CARGO"];
                    return usr;
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

        public DbResponse Atualizar(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                "UPDATE USUARIOS SET PONTOS=@PONTOS, CARGO=@CARGO WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", usuario.ID);
            command.Parameters.AddWithValue("@PONTOS", usuario.Pontos);
            command.Parameters.AddWithValue("@CARGO", usuario.Funcao);
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

        public DbResponse AlterarDados(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                "UPDATE USUARIOS SET TELEFONE=@TELEFONE, EMAIL=@EMAIL WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", usuario.ID);
            command.Parameters.AddWithValue("@TELEFONE", usuario.Telefone);
            command.Parameters.AddWithValue("@EMAIL", usuario.Email);
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

        public DbResponse AlterarSenha(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(DbConfig.CONNECTION_STRING);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                "UPDATE USUARIOS SET SENHA=@PASSWORD WHERE ID=@ID";
            command.Parameters.AddWithValue("@ID", usuario.ID);
            command.Parameters.AddWithValue("@PASSWORD", usuario.Password);
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
    }
}
