using Coelsa.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Coelsa.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }        
    }
}
