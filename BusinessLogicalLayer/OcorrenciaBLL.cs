using DataAccessLayer;
using DataAccessLayer.Impl;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class OcorrenciaBLL : BaseValidator<Ocorrencia>
    {
        OcorrenciaDAL ocorrenciaDAL = new OcorrenciaDAL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        protected override void Validate(Ocorrencia item)
        {
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                //Adicionar erro
                AddError("Descricao", "A descrição deve ser informada.");
            }
            else
            {
                item.Descricao = item.Descricao.Trim();
                bool temPalavrao = false;
                string[] frases = item.Descricao.Split(' ');
                List<string> pesquisa = frases.ToList();
                List<string> palavroes = new List<string>()
                {
                   "Arrombado", "Babaca", "Baitola", "Baleia", "Biba", "Bicha", "Biroska", "Bobo", "Bocal", "Bosta", "Buceta", "Bundao", "Burro", "Cacete", "Cadelona", "Capiroto", "Caralho", "FilhodaPuta", "Foda", "Fuder", "Fudido", "Galinha", "Cocozento", "Cu", "DebilMental", "Demente", "Desgracado", "Mulambo", "Endemoniado", "Enfianocu", "EngoleRola", "Escroto", "Estupido", "FDP", "Fidumaegua", "Gonorreia", "GordoEscroto", "Gozado", "Idiota", "Ignorante", "Imbecil", "Imundo", "Jacu", "KCT", "Ku", "lazarento", "Leproso", "Merda", "MiolodeCu", "Mocorongo", "MontedeMerda", "Nazista", "Fascista", "Olhodocu", "Ogro", "Otario", "Panaca", "PauNoCu", "Piroca", "Porra", "prostituta", "Punheta", "Puta", "PutaQuePariu", "Quenga", "Xoxota", "ZeBuceta"
                };
                for (int i = pesquisa.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < palavroes.Count; j++)
                    {
                        if (pesquisa[i].ToLower() == palavroes[j].ToLower())
                        {
                            temPalavrao = true;
                            break;
                        }
                    }
                }
                if (temPalavrao)
                {
                    AddError("TemPalavrao", "Descrição inválida.");
                }
                else if(item.Descricao.Length > 500)
                {
                    //Adicionar erro
                    AddError("Descricao", "A descrição não pode conter mais de 500 caracteres.");
                }
            }
            base.Validate(item);
        }

        private void ValidarOcComum(Ocorrencia item)
        {
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                AddError("Descricao", "A descrição deve ser informada.");
            }
            else if (item.Descricao.Length > 500)
            {
                AddError("Descricao", "A descrição não pode conter mais de 500 caracteres.");
            }
        }

        public void Inserir(Ocorrencia ocorrencia)
        {
            Validate(ocorrencia);
            using (TransactionScope scope = new TransactionScope())
            {
                DbResponse response = ocorrenciaDAL.Inserir(ocorrencia);
                Usuario dadosUsuarioConectado = usuarioBLL.GetByID(ocorrencia);
                dadosUsuarioConectado.Pontos += 10;
                if (dadosUsuarioConectado.Pontos > 50 && dadosUsuarioConectado.Pontos < 150)
                {
                    dadosUsuarioConectado.Funcao = Domain.Enums.Cargo.Bronze;
                }
                else if (dadosUsuarioConectado.Pontos > 150 && dadosUsuarioConectado.Pontos < 300)
                {
                    dadosUsuarioConectado.Funcao = Domain.Enums.Cargo.Prata;
                }
                else if (dadosUsuarioConectado.Pontos > 300 && dadosUsuarioConectado.Pontos < 1000)
                {
                    dadosUsuarioConectado.Funcao = Domain.Enums.Cargo.Ouro;
                }
                else if (dadosUsuarioConectado.Pontos > 1000)
                {
                    dadosUsuarioConectado.Funcao = Domain.Enums.Cargo.Moderador;
                }
                usuarioBLL.Atualizar(dadosUsuarioConectado);
                if (!response.Success)
                {
                    throw new SDVException(response.Message);
                }
                SendEmail.Send(ocorrencia);
                scope.Complete();
            }
        }

        public List<Ocorrencia> LerTodos()
        {
            return ocorrenciaDAL.LerTodos();
        }

        public List<Ocorrencia> LerTodos(int IDUsuario)
        {
            return ocorrenciaDAL.LerTodos(IDUsuario);
        }

        public List<Ocorrencia> LerTodasEmAberto()
        {
            return ocorrenciaDAL.LerTodasEmAberto();
        }

        public List<Ocorrencia> LerPrincipaisOcorrencias()
        {
            return ocorrenciaDAL.LerPrincipaisOcorrencias();
        }

        public List<Ocorrencia> LerMaisRecentes()
        {
            return ocorrenciaDAL.LerMaisRecentes();
        }

        public List<Ocorrencia> LerTodasComum(Ocorrencia ocorrencia, string[] termosPesquisa)
        {
            ValidarOcComum(ocorrencia);
            CheckErrors();
            List<string> pesquisa = termosPesquisa.ToList();
            //remover artigos, preposições e outras coisas inuteis 
            List<string> palavrasARemover = new List<string>()
            {
                "a", "e", "i", "o", "u", "ante", "ate", "apos", "com", "contra", "de", "desde", "em", "entre", "para", "por", "per", "perante", "sem", "sob", "sobre", "tras", "foi", "fato",
               "após", "mito", "ação", "apto", "área", "além", "pois", "ruim", "veio", "ente", "cedo","vida",
               "caos", "cela", "meio", "numa", "medo","pude", "como", "será","agir", "alvo", "nojo", "onde", "você","teve", 
               "alta", "trás", "traz", "auto", "peço", "tudo","mais", "auge", "todo", "sei",
               "algo", "em", "que", "quando", "onde", "ate", "porém", "entanto", "tal", "amanha", "antes", "da","de","di","do","du","na","ni","no","nu","ne","as","os","das","dos","des","la","lo", "meu", "teu", "nosso", "nossa", "vosso", "vossa", "minha"
            };

            for (int i = pesquisa.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < palavrasARemover.Count; j++)
                {
                    if (pesquisa[i].ToLower() == palavrasARemover[j].ToLower())
                    {
                        pesquisa.RemoveAt(i);
                        break;
                    }
                }
            }
            return ocorrenciaDAL.LerTodasComum(ocorrencia, pesquisa);
        }

        public void AtualizarCadastros(Ocorrencia ocorrencia)
        {
            ocorrencia.Cadastros++;
            DbResponse response = ocorrenciaDAL.AtualizarCadastros(ocorrencia);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
        }

        public void AtualizarStatus(Ocorrencia ocorrencia)
        {
            DbResponse response = ocorrenciaDAL.AtualizarStatus(ocorrencia);
            if (!response.Success)
            {
                throw new SDVException(response.Message);
            }
        }

        public List<Ocorrencia> RetornarFiltro(Ocorrencia ocorrencia)
        {
            //SE O ID CATEGORIA FOR 7 É TODAS OCORRENCIAS... SE O ID FOR 1 O FILTRO É POR STATUS EM ABERTO/RESOLVIDA (TODAS)
            if (ocorrencia.IDCategoria == 7 && ocorrencia.ID == 1)
            {
                return ocorrenciaDAL.LerTodos();
            }
            else if (ocorrencia.IDCategoria == 7 && ocorrencia.ID == 0)
            {
                return ocorrenciaDAL.RetornarTodasOcPorStatus(ocorrencia);
            }
            //SE O ID FOR 0 O FILTRO É EM ABERTO OU RESOLVIDA
            else if (ocorrencia.ID == 0)
            {
                return ocorrenciaDAL.RetornarOcPorStatusCategoria(ocorrencia);
            }
            return ocorrenciaDAL.RetornarTodasOcPorCategoria(ocorrencia);
        }

        public List<Ocorrencia> RetornarFiltroUsrConectado(Ocorrencia ocorrencia)
        {
            //SE O ID CATEGORIA FOR 7 É TODAS OCORRENCIAS... SE O ID FOR 1 O FILTRO É POR STATUS EM ABERTO/RESOLVIDA
            if (ocorrencia.IDCategoria == 7 && ocorrencia.ID == 1)
            {
                return ocorrenciaDAL.LerTodos(ocorrencia.IDUsuario);
            }
            else if (ocorrencia.IDCategoria == 7 && ocorrencia.ID == 0)
            {
                return ocorrenciaDAL.RetornarTodasOcPorStatusUsrConectado(ocorrencia);
            }
            //SE O ID FOR 0 O FILTRO É EM ABERTO OU RESOLVIDA
            else if (ocorrencia.ID == 0)
            {
                return ocorrenciaDAL.RetornarOcPorStatusCategoriaUsrConectado(ocorrencia);
            }
            return ocorrenciaDAL.RetornarTodasOcPorCategoriaUsrConectado(ocorrencia);
        }
    }
}
