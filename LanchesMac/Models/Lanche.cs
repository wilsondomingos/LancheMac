using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [StringLength(80, MinimumLength =10, ErrorMessage = "O {0} deve ter no minimo {1} e no maximo {2}")]
        [Required(ErrorMessage = "O nome do Lanche deve ser informado")]
        [Display(Name = "Nome do Lanche")]
        public string Nome { get; set; }

        [MinLength(20,  ErrorMessage = "A descrição deve ter no minimo {1} caracter")]
        [MaxLength(200,  ErrorMessage = "A descrição pode exceder  {1} caracter")]
        [Required(ErrorMessage = "A descrição do Lanche deve ser informado")]
        [Display(Name = "Decsrição do Lanche")]
        public string DescricaoCurta { get; set; }

        [MinLength(20, ErrorMessage = "A descrição deve ter no minimo {1} caracter")]
        [MaxLength(200, ErrorMessage = "A descrição pode exceder  {1} caracter")]
        [Required(ErrorMessage = "A descrição do Lanche deve ser informado")]
        [Display(Name = "Decsrição do Lanche")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [StringLength(80, MinimumLength = 200, ErrorMessage = "O {0} deve ter no minimo {1} caracter")]
        [Display(Name = "Caminnho da Imagem")]
        public string ImagemUrl { get; set; }

        [StringLength(80, MinimumLength = 200, ErrorMessage = "O {0} deve ter no minimo {1} caracter")]
        [Display(Name = "Caminnho da Imagem")]
        public string ImagemThumbnailUrl { get; set;}

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}
