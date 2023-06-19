using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonWiki.Models;

namespace PokemonWiki.Data
{
    public class PokemonWikiContext : DbContext
    {
        public PokemonWikiContext (DbContextOptions<PokemonWikiContext> options)
            : base(options)
        {
        }

        public DbSet<PokemonWiki.Models.Trainer> Trainer { get; set; } = default!;

        public DbSet<PokemonWiki.Models.Pokemon> Pokemon { get; set; } = default!;

        public DbSet<PokemonWiki.Models.Type_pok> Type_pok { get; set; } = default!;

        public DbSet<PokemonWiki.Models.Attacks> Attacks { get; set; } = default!;
    }
}
