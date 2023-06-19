using System.ComponentModel.DataAnnotations;

namespace PokemonWiki.Models
{
    public class Attacks
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Name")]
        public string AttackName { get; set; }
        
        [Display(Name = "Moc")]
        public int Power { get; set; }
        
        public int? TypeId { get; set; }
        public Type_pok? Type { get; set; }
    }
}
