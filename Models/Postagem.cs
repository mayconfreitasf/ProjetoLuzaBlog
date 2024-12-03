using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLuzaBlog.Models
{
    public class Postagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodPostagem { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Por favor, forneça o conteúdo da postagem.")]
        [DisplayName("Conteúdo")]
        public string DesConteudo { get; set; }

        [Required]
        [DisplayName("Cadastro")]
        public DateTime DatCadastro { get; set; }

        [DisplayName("Atualização")]
        public DateTime? DatAtualizacao { get; set; }

        //[NotMapped]
        //[BindNever]
        //public IdentityUser Usuario { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    // Certifique-se de que o EF não valide a propriedade Usuario
        //    if (Usuario == null)
        //    {
        //        yield return new ValidationResult("Usuário não pode ser nulo.", new[] { "Usuario" });
        //    }
        //}
    }
}
