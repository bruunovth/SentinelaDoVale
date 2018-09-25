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
    public class PasswordController : BaseController
    {
        // GET: Password
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(PasswordViewModel passwordViewModel)
        {
            Usuario usuario = CustomAutoMapper<Usuario, PasswordViewModel>.ConvertTo(passwordViewModel);
            usuario.ID = Convert.ToInt32(Cookie.Get("SentinelaDoValeLogin"));
            string newPassword = passwordViewModel.NewPassword;
            string confirmNewPassword = passwordViewModel.ConfirmNewPassword;
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            try
            {
                usuarioBLL.AlterarSenha(usuario, newPassword, confirmNewPassword);
                return RedirectToAction("Atualizar", "Account");
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