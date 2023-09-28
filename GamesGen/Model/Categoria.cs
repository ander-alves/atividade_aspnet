using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesGen.Model
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [InverseProperty("Categoria")]
        [JsonIgnore]
        public virtual ICollection<Produto>? Produto { get; set; }

    }
}