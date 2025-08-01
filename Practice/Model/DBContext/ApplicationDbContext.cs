using Microsoft.EntityFrameworkCore;
using Practice.Model.Entities;

namespace Practice.Model.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(a =>
            {
                a.ToTable("Users").Property(x => x.GuidUser).ValueGeneratedOnAddOrUpdate();
                a.ToTable(tb => tb.UseSqlOutputClause(false));
            });

            modelBuilder.Entity<Client>(a =>
            {
                a.ToTable("Client").Property(x => x.GuidClient).ValueGeneratedOnAddOrUpdate();
                a.ToTable(tb => tb.UseSqlOutputClause(false));
            });

            modelBuilder.Entity<Admin>(a =>
            {
                a.ToTable("Admin").Property(x => x.GuidAdmin).ValueGeneratedOnAddOrUpdate();
                a.ToTable(tb => tb.UseSqlOutputClause(false));
            });
        }

    }
}
