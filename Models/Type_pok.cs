using System.ComponentModel.DataAnnotations;

namespace PokemonWiki.Models
{
    public class Type_pok
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Typ")]
        public string TypeName { get; set; }
        
        [Display(Name = "Weakness")]
        public string Weaknesses { get; set; }
        
        [Display(Name = "Strengths")]
        public string Strengths { get; set; }
    }
}
