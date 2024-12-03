using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoLuzaBlog.Dal;
using ProjetoLuzaBlog.Models;

namespace ProjetoLuzaBlog.Pages.PostagemMaster
{
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


        public IList<Postagem> Postagem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Postagens != null)
            {

                var user = await _userManager.GetUserAsync(User);
                Postagem = await _context.Postagens.Where(x=>x.UserId == user.Id).ToListAsync();
            }
        }
    }
}
