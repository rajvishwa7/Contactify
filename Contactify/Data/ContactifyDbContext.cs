using Contactify.Models;
using Microsoft.EntityFrameworkCore;

namespace Contactify.Data
{
    public class ContactifyDbContext : DbContext
    {
        public ContactifyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }    // DbSet acts as Table
    }
}
