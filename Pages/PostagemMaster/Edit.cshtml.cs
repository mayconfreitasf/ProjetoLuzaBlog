using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLuzaBlog.Dal;
using ProjetoLuzaBlog.Models;

namespace ProjetoLuzaBlog.Pages.PostagemMaster
{
    public class EditModel : PageModel
    {
        private readonly ProjetoLuzaBlog.Dal.AppDbContext _context;

        public EditModel(ProjetoLuzaBlog.Dal.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Postagens == null)
            {
                return NotFound();
            }

            var postagem =  await _context.Postagens.FirstOrDefaultAsync(m => m.CodPostagem == id);
            if (postagem == null)
            {
                return NotFound();
            }
            Postagem = postagem;
           //ViewData["CodUsuario"] = new SelectList(_context.Usuarios, "CodUsuario", "DesSenha");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Postagem.DatAtualizacao = DateTime.Now;
            _context.Attach(Postagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostagemExists(Postagem.CodPostagem))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostagemExists(int id)
        {
          return (_context.Postagens?.Any(e => e.CodPostagem == id)).GetValueOrDefault();
        }
    }
}
