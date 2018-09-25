using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class UsuarioBLL : BaseValidator<Usuario>
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();
        protected override void Validate(Usuario item)
        {            
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                //Adicionar erro
                AddError("Email", "O email deve ser informado.");
            }
            else
            {
                item.Email = item.Email.Trim();
                if (!Validacoes.IsValidEmail(item.Email))
                {
                    AddError("Email", "Email inválido.");
                }
                else if (item.Email.Length > 120)
                {
                    //Adicionar erro
                    AddError("Email", "O email não pode conter mais de 120 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                //Adicionar erro
                AddError("Nome", "O nome deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Trim();
                if (item.Nome.Length > 100)
                {
                    //Adicionar erro
                    AddError("Nome", "O nome não pode conter mais de 100 caracteres.");
                }
            }            
            if (string.IsNullOrWhiteSpace(item.Password))
            {
                //Adicionar erro
                AddError("Password", "A senha deve ser informada.");
            }
            else
            {
                item.Password = item.Password.Trim();
                if (item.Password.Length < 8 || item.Password.Length > 30 || !Validacoes.IsValidPassword(item.Password))
                {
                    //Adicionar erro
                    AddError("Password", "A senha deve conter entre 8 e 30 caracteres e pelo menos uma letra e um número.");
                }
            }            
            if (string.IsNullOrWhiteSpace(item.Telefone))
            {
                //Adicionar erro
                AddError("Telefone", "O telefone deve ser informado.");
            }
            else
            {
                item.Telefone = item.Telefone.Replace("-", "").Replace("(", "").Replace(")", "");
                item.Telefone = item.Telefone.Trim();
                if (item.Telefone.Length > 11)
                {
                    //Adicionar erro
                    AddError("Telefone", "O telefone não pode conter mais de 11 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(item.Username))
            {
                //Adicionar erro
                AddError("Username", "O username deve ser informado.");
            }
            else
            {
                item.Username = item.Username.Trim();
                if (item.Username.Length > 20)
                {
                    //Adicionar erro
                    AddError("Username", "O usuário não pode conter mais de 20 caracteres.");
                }
            }
            if (item.DataNascimento.Year < 1900 || item.DataNascimento.Year > 2018)
            {
                AddError("DataNascimentoErro", "Data inválida.");
            }
            base.Validate(item);
        }        

        private void ValidarAutenticacao(Usuario item)
        {
            if (string.IsNullOrWhiteSpace(item.Username))
            {
                //Adicionar erro
                AddError("Username", "O usuário deve ser informado.");
            }
            if (string.IsNullOrWhiteSpace(item.Password))
            {
                //Adicionar erro
                AddError("Password", "A senha deve ser informada.");
            }
        }

        private void ValidarAlteracaoDados(Usuario item)
        {
            item.Telefone = item.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
            if (string.IsNullOrWhiteSpace(item.Email))
            {
                //Adicionar erro
                AddError("Email", "O email deve ser informado.");
            }
            else
            {
                item.Email = item.Email.Trim();
                if (!Validacoes.IsValidEmail(item.Email))
                {
                    AddError("Email", "Email inválido.");
                }
                else if (item.Email.Length > 120)
                {
                    //Adicionar erro
                    AddError("Email", "O email não pode conter mais de 120 caracteres.");
                }
            }
            if (string.IsNullOrWhiteSpace(item.Telefone))
            {
                //Adicionar erro
                AddError("Telefone", "O telefone deve ser informado.");
            }
            else
            {
                item.Telefone = item.Telefone.Trim();
                if (item.Telefone.Length > 11)
                {
                    //Adicionar erro
                    AddError("Telefone", "O telefone não pode conter mais de 11 caracteres.");
                }
            }
        }

        private void ValidarAlteracaoSenha(Usuario item, string newPassword, string confirmNewPassword)
        {
            Usuario usuarioConectado = usuarioDAL.GetByID(item);
            if (string.IsNullOrWhiteSpace(item.Password))
            {
                AddError("Password", "A senha atual deve ser informada.");
            }
            else if (item.Password != usuarioConectado.Password)
            {
                AddError("Password", "Senha incorreta. Digite novamente!");
            }
            else if (string.IsNullOrWhiteSpace(newPassword))
            {
                AddError("NewPassword", "A nova senha deve ser informada.");
            }
            else if (newPassword.Length < 8 || newPassword.Length > 30 || !Validacoes.IsValidPassword(newPassword))
            {
                AddError("NewPassword", "A senha deve conter entre 8 e 30 caracteres e pelo menos uma letra e número.");
            }
            else if (string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                AddError("ConfirmNewPassword", "Por favor, confirme a nova senha!");
            }
            else if (newPassword != confirmNewPassword)
            {
                AddError("ConfirmNewPassword", "As senhas informadas não coincidem.");
            }
        }

        public Usuario Autenticar(Usuario usuario)
        {
            ValidarAutenticacao(usuario);
            //Evita uma ida ao banco desnecessária
            CheckErrors();
            Usuario usr = usuarioDAL.Autenticar(usuario);
            if (usr == null)
            {
                AddError("Username", "Usuário e/ou senha inválidos.");
            }
            CheckErrors();
            return usr;
        }

        public Usuario GetByID(int IDUsuario)
        {
            return usuarioDAL.GetByID(IDUsuario);
        }

        public Usuario GetByID(Ocorrencia ocorrencia)
        {
            return usuarioDAL.GetByID(ocorrencia);
        }

        public Usuario Inserir(Usuario usuario)
        {
            Validate(usuario);
            DbResponse response = usuarioDAL.Inserir(usuario);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
            Usuario usuarioCadastrado = usuarioDAL.GetByUsername(usuario);
            return usuarioCadastrado;
        }

        public void Atualizar(Usuario usuario)
        {
            ValidarAlteracaoDados(usuario);
            CheckErrors();
            DbResponse response = usuarioDAL.Atualizar(usuario);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
        }

        public void AlterarDados(Usuario usuario)
        {
            ValidarAlteracaoDados(usuario);
            CheckErrors();
            DbResponse response = usuarioDAL.AlterarDados(usuario);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
        }

        public void AlterarSenha(Usuario usuario, string newPassword, string confirmNewPassword)
        {
            ValidarAlteracaoSenha(usuario, newPassword, confirmNewPassword);
            CheckErrors();
            usuario.Password = newPassword;
            DbResponse response = usuarioDAL.AlterarSenha(usuario);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
        }
    }
}