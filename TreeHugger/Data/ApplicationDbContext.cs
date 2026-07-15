using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<AchievementEntity>? AchievementEntities { get; set; }
    public DbSet<ChoiceEntity>? ChoiceEntities { get; set; }
    public DbSet<CommentEntity>? CommentEntities { get; set; }
    public DbSet<LessonEntity>? LessonEntities { get; set; }
    public DbSet<ProfileEntity>? ProfileEntities { get; set; }
    public DbSet<QuestionEntity>? QuestionEntities { get; set; }
    public DbSet<QuizEntity>? QuizEntities { get; set; }
    public DbSet<ReplyEntity>? ReplyEntities { get; set; }
    public DbSet<StudentAchievementEntity>? StudentAchievementEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<IdentityRole>().ToTable(nameof(IdentityRole));
        builder.Entity<IdentityRoleClaim<string>>().ToTable(nameof(IdentityRoleClaim<string>));
        builder.Entity<IdentityUser>().ToTable(nameof(IdentityUser));
        builder.Entity<IdentityUserRole<string>>().ToTable(nameof(IdentityUserRole<string>));
        builder.Entity<IdentityUserClaim<string>>().ToTable(nameof(IdentityUserClaim<string>));
        builder.Entity<IdentityUserLogin<string>>().ToTable(nameof(IdentityUserLogin<string>));
        builder.Entity<IdentityUserToken<string>>().ToTable(nameof(IdentityUserToken<string>));
    }
}