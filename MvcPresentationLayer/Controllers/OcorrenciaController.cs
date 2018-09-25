using BusinessLogicalLayer;
using Domain;
using MvcPresentationLayer.Models;
using MvcPresentationLayer.WEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    public class OcorrenciaController : BaseController
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Cadastrar(OcorrenciaViewModel ocorrenciaViewModel)
        {
            Ocorrencia ocorrencia = CustomAutoMapper<Ocorrencia, OcorrenciaViewModel>.ConvertTo(ocorrenciaViewModel);
            ocorrencia.IDUsuario = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            ocorrencia.Cadastros = 1;
            ocorrencia.DataCadastro = DateTime.Now;
            MvcPresentationLayer.WEB.GoogleAddressHelper googleAddressHelper = new GoogleAddressHelper();
            ocorrencia.Endereco = googleAddressHelper.RetrieveFormatedAddress(ocorrencia);
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            Ocorrencia ocorrenciaMenor = new Ocorrencia();
            double menorDist = double.MaxValue;
            string[] frases = null;
            if (ocorrencia.Descricao != null)
            {
                frases = ocorrencia.Descricao.Split(' ');
            }
            try
            {
                List<Ocorrencia> ocorrenciasComum = ocorrenciaBLL.LerTodasComum(ocorrencia, frases);
                if (ocorrenciasComum.Count != 0)
                {
                    foreach (Ocorrencia oc in ocorrenciasComum)
                    {
                        double dist = Haversine.calculate(ocorrencia, oc);
                        if (dist <= 1000 && dist <= menorDist)
                        {
                            ocorrenciaMenor = oc;
                            menorDist = dist;
                        }
                    }
                }
                if (ocorrenciaMenor.IDUsuario == 0)
                {
                    ocorrenciaBLL.Inserir(ocorrencia);
                }
                else
                {
                    GoogleAddressHelper helper = new GoogleAddressHelper();
                    string address = helper.RetrieveFormatedAddress(ocorrenciaMenor);
                    Categoria categoria = categoriaBLL.GetByID(ocorrenciaMenor);
                    return new JsonResult()
                    {
                        Data = new
                        {
                            Success = true,
                            ID = ocorrenciaMenor.ID,
                            Descricao = ocorrenciaMenor.Descricao,
                            CategoriaOcorrencia = categoria.Nome,
                            Endereco = address,
                            MenorDistancia = Math.Ceiling(menorDist).ToString() + " metros",
                        }
                    };
                }                
            }
            catch (SDVException ex)
            {
                ErrorField errors = new ErrorField();
                if (ex.Errors.Count == 0)
                {
                    errors.Message = "Erro no DB, contate o administrador.";
                }
                else
                {
                    foreach (ErrorField error in ex.Errors)
                    {
                        ModelState.AddModelError(error.Field, error.Message);
                        errors.Message = error.Message;
                    }
                }
                return new JsonResult
                {
                    Data = new { ErrorMessage = errors.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            return new JsonResult() { Data = new { Success = true, ID = 0 },  };
        }

        [HttpPost]
        public JsonResult CadastrarNaoEncontrada(OcorrenciaViewModel ocorrenciaViewModel)
        {
            Ocorrencia ocorrencia = CustomAutoMapper<Ocorrencia, OcorrenciaViewModel>.ConvertTo(ocorrenciaViewModel);
            ocorrencia.IDUsuario = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            ocorrencia.Cadastros = 1;
            ocorrencia.DataCadastro = DateTime.Now;
            MvcPresentationLayer.WEB.GoogleAddressHelper googleAddressHelper = new GoogleAddressHelper();
            ocorrencia.Endereco = googleAddressHelper.RetrieveFormatedAddress(ocorrencia);
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            try
            {
                ocorrenciaBLL.Inserir(ocorrencia);
            }
            catch (SDVException ex)
            {
                ErrorField errors = new ErrorField();
                if (ex.Errors.Count == 0)
                {
                    errors.Message = "Erro no DB, contate o administrador.";
                }
                else
                {
                    foreach (ErrorField error in ex.Errors)
                    {
                        ModelState.AddModelError(error.Field, error.Message);
                        errors.Message = error.Message;
                    }
                }
                return new JsonResult
                {
                    Data = new { ErrorMessage = errors.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            return new JsonResult() { Data = new { Success = true }, };
        }

        [HttpPost]
        public JsonResult AtualizarCadastros(OcorrenciaViewModel ocorrenciaViewModel)
        {
            Ocorrencia ocorrencia = CustomAutoMapper<Ocorrencia, OcorrenciaViewModel>.ConvertTo(ocorrenciaViewModel);
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            string[] frases = ocorrencia.Descricao.Split(' ');
            double menorDist = double.MaxValue;
            Ocorrencia ocorrenciaMenor = new Ocorrencia();
            try
            {
                List<Ocorrencia> ocorrenciasComum = ocorrenciaBLL.LerTodasComum(ocorrencia, frases);
                foreach (Ocorrencia oc in ocorrenciasComum)
                {
                    double dist = Haversine.calculate(ocorrencia, oc);
                    if (dist <= 1000 && dist <= menorDist)
                    {
                        ocorrenciaMenor = oc;
                        menorDist = dist;
                    }
                }
                ocorrenciaBLL.AtualizarCadastros(ocorrenciaMenor);
            }
            catch (SDVException ex)
            {
                ErrorField errors = new ErrorField();
                if (ex.Errors.Count == 0)
                {
                    errors.Message = "Erro no DB, contate o administrador.";
                }
                else
                {
                    foreach (ErrorField error in ex.Errors)
                    {
                        ModelState.AddModelError(error.Field, error.Message);
                        errors.Message = error.Message;
                    }
                }
                return new JsonResult
                {
                    Data = new { ErrorMessage = errors.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            return new JsonResult() { Data = new { Success = true }, };
        }
    }
}
