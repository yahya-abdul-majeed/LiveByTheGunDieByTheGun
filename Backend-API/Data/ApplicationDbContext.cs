using Backend_API.Models;
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
        public DbSet<JailedUser> JailedUser { get; set; }   
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Group> Groups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Student>(entity => { entity.ToTable("Students"); });
            builder.Entity<Teacher>(entity => { entity.ToTable("Teachers"); });
            builder.Entity<ApplicationUser>(entity => { entity.ToTable("ApplicationUsers"); });
            builder.Entity<JailedUser>(entity => { entity.ToTable("JailedUsers"); });
            builder.Entity<Faculty>(entity => { entity.ToTable("Faculties"); });
            builder.Entity<Direction>(entity => { entity.ToTable("Directions"); });
            builder.Entity<Discipline>(entity => { entity.ToTable("Disciplines"); });
            builder.Entity<Group>(entity => { entity.ToTable("Groups"); });
            builder.Entity<Schedule>(entity => { entity.ToTable("Schedules"); });

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
