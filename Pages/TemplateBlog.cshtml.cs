using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoLuzaBlog.Models;

namespace ProjetoLuzaBlog.Pages
{
    public class TemplateBlogModel : PageModel
    {
        private readonly ProjetoLuzaBlog.Dal.AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TemplateBlogModel(Dal.AppDbContext context)
        {
            _context = context;
        }

        public IList<Postagem> Postagens { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Postagens != null)
            {
                Postagens = await _context.Postagens.ToListAsync();
            }

        }
    }
}
