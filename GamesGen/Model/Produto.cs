using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamesGen.Model
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Descricao { get; set; }

        /*[Column(TypeName = "varchar(100)")]
        public string Console { get; set; }

        public DateTime DataLancamento { get; set; }

        public decimal Preco { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Foto { get; set; }*/

       public virtual Categoria? Categoria { get; set; }
    }
}