using Binit.Framework.Constants.Authentication;
using Domain.Entities.Model.SPs;
using Domain.Entities.Model.Views;
using Domain.Entities.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Model
{
    public class ModelDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {

        }

        // Add DbSet for IEntities. Example:
        public DbSet<Product> Products { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<HolidayType> HolidayTypes { get; set; }
        public DbSet<HolidayUser> HolidayUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ProductEditor> ProductEditors { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<IgniteFile> IgniteFiles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventFile> EventFiles { get; set; }
        public DbSet<FrontUser> FrontUsers { get; set; }
        public DbSet<BackOfficeUser> BackOfficeUsers { get; set; }
        public DbSet<Genero> Generos { get; set; } 
        public DbSet<Pelicula> Peliculas { get; set; } 
        public DbSet<Actor> Actores { get; set; }
        public DbSet<PeliculaActor> PeliculaActors { get; set; }
        public DbSet<Curiosidad> Curiosidades { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameAnswer> GameAnswers { get; set; }

        // Model that represents the result from the View_ProductFeaturesCounts
        public DbSet<ProductFeaturesView> ProductFeaturesCount { get; set; }
        public DbSet<SPReturnCategory> SPReturnCategory { get; set; }
        public DbSet<IgniteAddress> IgniteAddresses { get; set; }

     


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Bind the model ProductFeatureCount to a Database View called View_ProductFeaturesCounts
            modelBuilder.Entity<ProductFeaturesView>().HasNoKey()
                .ToTable(ProductFeaturesView.Name)
                .Property(v => v.ProductName).HasColumnName("Name");
            // Bind the model SPReturnCategory to the result from the SP Create Category
            modelBuilder.Entity<SPReturnCategory>().HasNoKey();

            // Add modelBuilder for IEntities. 
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.Deleted);

            modelBuilder.Entity<Holiday>().HasQueryFilter(h => !h.Deleted);

            modelBuilder.Entity<HolidayType>().HasQueryFilter(ht => !ht.Deleted);

            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.Deleted);

            modelBuilder.Entity<Genero>().HasQueryFilter(g => !g.Deleted);
            modelBuilder.Entity<Pelicula>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Actor>().HasQueryFilter(a => !a.Deleted);
            modelBuilder.Entity<Theme>().HasQueryFilter(a => !a.Deleted);
            modelBuilder.Entity<Question>().HasQueryFilter(a => !a.Deleted);
            modelBuilder.Entity<Answer>().HasQueryFilter(a => !a.Deleted);
            modelBuilder.Entity<Curiosidad>().HasQueryFilter(c => !c.Deleted);
            modelBuilder.Entity<Game>().HasQueryFilter(c => !c.Deleted);

            modelBuilder.Entity<Feature>().HasQueryFilter(f => !f.Deleted);

            modelBuilder.Entity<IgniteFile>().HasQueryFilter(f => !f.Deleted);

            modelBuilder.Entity<Tenant>()
                .HasQueryFilter(t => !t.Deleted)
                .HasIndex(t => t.Code)
                .IsUnique();

            modelBuilder.Entity<Event>()
                .HasQueryFilter(e => !e.Deleted)
                .HasOne(e => (Tenant)e.Tenant); // Specify tenant type.

            // Many to many join entities.
            modelBuilder.Entity<ProductEditor>()
                .HasKey(p => new { p.ProductId, p.EditorId });

            modelBuilder.Entity<GameAnswer>()
               .HasOne(e => e.Game)
               .WithMany(g => (List<GameAnswer>)g.Answers).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameAnswer>()
                .HasOne(e => e.Question)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameAnswer>()
             .HasKey(g => new { g.QuestionId, g.AnswerId, g.GameId });

            modelBuilder.Entity<HolidayUser>()
                .HasKey(hu => new { hu.HolidayId, hu.UserId });

            modelBuilder.Entity<EventFile>()
                .HasKey(ef => new { ef.EventId, ef.FileId });
            modelBuilder.Entity<PeliculaActor>()
                .HasKey(ef => new { ef.ActorId, ef.PeliculaId });

            // Setup TPH for User inheritance
            modelBuilder.Entity<BackOfficeUser>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<FrontUser>().HasBaseType<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>()
                .HasQueryFilter(au => !au.Deleted)
                .HasDiscriminator<string>("UserType")
                .HasValue<FrontUser>(UserTypes.FrontUser)
                .HasValue<BackOfficeUser>(UserTypes.BackofficeUser);

            modelBuilder.Entity<IgniteAddress>().HasQueryFilter(ia => !ia.Deleted);

            // Generate seed data.
            try
            {
                SeedManager seedManager = new SeedManager(modelBuilder, Database);
                seedManager.ExecuteSeed();
                seedManager.ExecuteIgniteDbObjectSeed();
            }
            catch (Exception)
            {

            }

        }
    }
}