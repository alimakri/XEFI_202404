using Microsoft.EntityFrameworkCore;
using ProjetPowWeb.Models;

namespace ProjetPowWeb.Data
{
    // https://www.c-sharpcorner.com/article/implement-entity-framework-a-code-first-approach-in-net-8-api/
    // https://www.learnentityframeworkcore.com/migrations/add-migration
    // Classe représentant la base de données
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            
        }
        public DbSet<Todo> Todoes { get; set; }
    }
}
