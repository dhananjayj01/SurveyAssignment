using Microsoft.EntityFrameworkCore;
using SurveyAssignment.Domain.Entities;

namespace SurveyAssignment.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Survey> Survey { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionOption> QuestionOption { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Section)
                .WithOne(s => s.Survey)
                .HasForeignKey(s => s.SurveyId);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.Question)
                .WithOne(q => q.Section)
                .HasForeignKey(q => q.SectionId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Option)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasKey(x => x.OptionId);

                entity.Property(x => x.OptionText)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.HasOne(x => x.Question)
                      .WithMany(x => x.Option)
                      .HasForeignKey(x => x.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.HasKey(x => x.QuestionTypeId);

                entity.Property(x => x.TypeCode)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.TypeName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}
