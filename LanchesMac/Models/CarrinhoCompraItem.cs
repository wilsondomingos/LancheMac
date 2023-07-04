using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("CarrinhoCompraItem")]
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoCompraItenId { get; set; }

        public Lanche Lanche { get; set; }

        public int Quantidade { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }
    }
}