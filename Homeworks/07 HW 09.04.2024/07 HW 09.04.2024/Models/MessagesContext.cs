using Microsoft.EntityFrameworkCore;

namespace _07_HW_09._04._2024.Models
{
    public class MessagesContext: DbContext
    {
        public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
