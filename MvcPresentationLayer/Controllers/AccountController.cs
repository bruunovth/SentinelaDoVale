using BusinessLogicalLayer;
using Domain;
using MvcPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcPresentationLayer.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Atualizar()
        {
            int IDUsuario = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            Usuario usuarioConectado = usuarioBLL.GetByID(IDUsuario);
            OcorrenciaBLL ocorrenciaBLL = new OcorrenciaBLL();
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            StringBuilder markers = new StringBuilder();
            try
            {
                 List<Ocorrencia> ocorrenciasUsuarioConectado = ocorrenciaBLL.LerTodos(IDUsuario);
                 markers.Append("[");
                 foreach (Ocorrencia ocorrencia in ocorrenciasUsuarioConectado)
                 {
                     ocorrencia.Lat = ocorrencia.Lat.Replace(",", ".");
                     ocorrencia.Lng = ocorrencia.Lng.Replace(",", ".");
                     markers.Append("{");
                     markers.Append(string.Format("'lat': '{0}',", ocorrencia.Lat));
                     markers.Append(string.Format("'lng': '{0}',", ocorrencia.Lng));
                     markers.Append(string.Format("'description': '{0}',", ocorrencia.Descricao));
                     markers.Append(string.Format("'title': '{0}'", categoriaBLL.GetByID(ocorrencia).Nome));
                     markers.Append("},");
                 }
                 markers.Append("];");
                 ViewBag.Markers = markers.ToString();
            }
            catch
            {

            }
            return View(usuarioConectado);
        }

        [HttpPost]
        public ActionResult Atualizar(UsuarioViewModel usuarioViewModel)
        {
            Usuario usuario = CustomAutoMapper<Usuario, UsuarioViewModel>.ConvertTo(usuarioViewModel);
            usuario.ID = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            try
            {
                usuarioBLL.AlterarDados(usuario);
            }
            catch (SDVException ex)
            {
                if (ex.Errors.Count == 0)
                {
                    if (ex.Message.Contains("UNIQUE"))
                    {
                        ModelState.AddModelError("Username", "Este email e/ou username já está cadastrado.");
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Erro no DB, contate o administrador.");
                    }
                }
                else
                {
                    foreach (ErrorField error in ex.Errors)
                    {
                        ModelState.AddModelError(error.Field, error.Message);
                    }
                }
            }
            return RedirectToAction("Atualizar", "Account");
        }
    }
}