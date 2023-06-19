using System.ComponentModel.DataAnnotations;

namespace PokemonWiki.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        public int? TypeId { get; set; }
        public Type_pok? Type { get; set; }
        
        public int? AttackId { get; set; }
        public Attacks? Attack { get; set; }
    }
}
