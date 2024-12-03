using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjetoLuzaBlog.Pages.Autenticacao
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Faz logout do usu�rio
            await _signInManager.SignOutAsync();

            // Redireciona para a p�gina inicial ap�s o logout
            return Redirect("/TemplateBlog");
        }
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

    }
}

