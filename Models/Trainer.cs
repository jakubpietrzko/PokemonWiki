using System.ComponentModel.DataAnnotations;

namespace PokemonWiki.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        
        public int? PokemonId { get; set; }
        public Pokemon? Pokemon { get; set; }
    }
}
