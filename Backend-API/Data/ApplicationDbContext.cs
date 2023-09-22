using Backend_API.Models.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend_API.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Student>(entity => { entity.ToTable("Students"); });
            builder.Entity<Teacher>(entity => { entity.ToTable("Teachers"); });
            builder.Entity<ApplicationUser>(entity => { entity.ToTable("ApplicationUsers"); });

            //builder.Entity<Teacher>().HasData(new Teacher
            //{
            //    Email = "yahya.zf2@gmail.com",
            //    Avatar = "somethings random",
            //    UserName ="yoyo",
            //    BirthDate = DateOnly.FromDateTime(DateTime.Now),
            //    PhoneNumber = "1234567890",

            //});
        }
    }
}
