using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoLuzaBlog.Models;

namespace ProjetoLuzaBlog.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProjetoLuzaBlog.Dal.AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Construtor único que injeta tanto o AppDbContext quanto o UserManager
        public IndexModel(ProjetoLuzaBlog.Dal.AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string LoggedInUsername { get; set; }

        public IList<Postagem> Postagens { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Recupera o usuário logado
                var user = await _userManager.GetUserAsync(User);
                LoggedInUsername = user.UserName; 
                if (_context.Postagens != null)
                {
                    Postagens = await _context.Postagens.ToListAsync();
                }
            }
            else
            {
                LoggedInUsername = "Não autenticado";
            }
        }
    }
}