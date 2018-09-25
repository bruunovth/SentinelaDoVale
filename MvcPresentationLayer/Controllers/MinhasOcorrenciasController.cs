using BusinessLogicalLayer;
using Domain;
using MvcPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    public class MinhasOcorrenciasController : Controller
    {
        // GET: MinhasOcorrencias
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Atualizar(TodasOcorrenciasViewModel todasOcorrenciasViewModel)
        {
            Ocorrencia ocorrencia = CustomAutoMapper<Ocorrencia, TodasOcorrenciasViewModel>.ConvertTo(todasOcorrenciasViewModel);
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            try
            {
                ocorrenciaBLL.AtualizarStatus(ocorrencia);
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
        public PartialViewResult LerOcorrencias(TodasOcorrenciasViewModel todasOcorrenciasViewModel)
        {
            Ocorrencia ocorrencia = CustomAutoMapper<Ocorrencia, TodasOcorrenciasViewModel>.ConvertTo(todasOcorrenciasViewModel);
            ocorrencia.IDUsuario = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            List<Ocorrencia> ocorrencias = new List<Ocorrencia>();
            try
            {
                ocorrencias = ocorrenciaBLL.RetornarFiltroUsrConectado(ocorrencia);
            }
            catch
            {
                ErrorField errors = new ErrorField();
                errors.Message = "Erro no DB, contate o administrador.";
                return PartialView("_LerOcorrencias", ocorrencias);
            }
            return PartialView("_LerMinhasOcorrencias", ocorrencias);
        }
    }
}