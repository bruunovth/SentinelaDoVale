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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(RegisterViewModel registerViewModel)
        {
            Usuario usuario = CustomAutoMapper<Usuario, RegisterViewModel>.ConvertTo(registerViewModel);
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            try
            {
                Usuario usuarioCadastrado = usuarioBLL.Inserir(usuario);
                Cookie.Set("SentinelaDoValeLogin", usuarioCadastrado.ID.ToString());
                return RedirectToAction("Index", "Home");
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
            return View();
        }
    }
}