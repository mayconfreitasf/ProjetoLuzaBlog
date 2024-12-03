using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
namespace ProjetoLuzaBlog.Pages.Autenticacao
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public bool LoginError { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {


                var user = await _userManager.FindByNameAsync(Username);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect("/Blog/Index");  // Redireciona para a página principal após o login
                    }
                    else
                    {
                        LoginError = true;  // Caso a senha esteja errada
                    }
                }
                else
                {
                    LoginError = true;  // Caso o usuário não exista
                }
            }
            else
            {
                LoginError = true;  // Campos vazios
            }
            return Page();
        }
    }
}