namespace MyCompany.MyProject.DataAccess.SqlServer
{
    using Microsoft.EntityFrameworkCore;
    using MyCompany.MyProject.Common.Customers;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Customers.CustomerMap());
        }
    }
}
