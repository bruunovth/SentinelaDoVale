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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Logar()
        {
            if (Cookie.Exists("SentinelaDoValeLogin"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logar(LoginViewModel loginViewModel)
        {
            Usuario usuario = CustomAutoMapper<Usuario, LoginViewModel>.ConvertTo(loginViewModel);
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            try
            {
                Usuario user = usuarioBLL.Autenticar(usuario);
                Cookie.Set("SentinelaDoValeLogin", user.ID.ToString());
                return RedirectToAction("Cadastrar", "Ocorrencia");
            }
            catch (SDVException ex)
            {
                if (ex.Errors.Count == 0)
                {
                    ModelState.AddModelError("Username", "Erro no DB, contate o administrador.");
                }
                else
                {
                    foreach (ErrorField error in ex.Errors)
                    {
                        ModelState.AddModelError(error.Field, error.Message);
                    }
                }
            }
            return View();
        }
    }
}