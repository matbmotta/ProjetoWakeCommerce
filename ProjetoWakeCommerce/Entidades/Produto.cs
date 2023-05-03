using System.ComponentModel.DataAnnotations;

namespace ProjetoWakeCommerce.Entidades
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Estoque { get; set; }

        public decimal Valor { get; set; }
    }
}