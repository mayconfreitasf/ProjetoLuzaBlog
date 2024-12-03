using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoLuzaBlog.Dal;
using ProjetoLuzaBlog.Models;
using ProjetoLuzaBlog.WebSocket;

namespace ProjetoLuzaBlog.Pages.PostagemMaster
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ServicoNotificacao _servicoNotificacao;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Construtor único que injeta tanto o AppDbContext quanto o UserManager
        public CreateModel(AppDbContext context, UserManager<IdentityUser> userManager, ServicoNotificacao servicoNotificacao)
        {
            _context = context;
            _userManager = userManager;
            _servicoNotificacao = servicoNotificacao;
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = new Postagem();
        public string UserId { get; set; }
        public IdentityUser Usuario { get; set; }
        public string WebSocketUrl { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Recupera o usuário logado
                var user = await _userManager.GetUserAsync(User);
                UserId = user.Id;
                Postagem.UserId = UserId;  // Define o UserId no modelo antes de exibir o formulário
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string nomeUsuario = "";
            if (User.Identity.IsAuthenticated)
            {
                // Recupera o usuário logado
                var user = await _userManager.GetUserAsync(User);
                nomeUsuario = user.UserName;
            }


            if (!ModelState.IsValid || _context.Postagens == null || Postagem == null)
            {
                return Page();
            }
            var dateNow = DateTime.Now;
            Postagem.DatCadastro = dateNow;
            Postagem.DatAtualizacao = dateNow;

            _context.Postagens.Add(Postagem);
            await _context.SaveChangesAsync();
            await _servicoNotificacao.EnviarNotificacao(Postagem, nomeUsuario);

            return RedirectToPage("./Index");
        }
    }
}
