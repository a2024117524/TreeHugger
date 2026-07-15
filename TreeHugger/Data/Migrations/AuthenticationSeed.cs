using FluentMigrator;
using Microsoft.AspNetCore.Identity;

namespace TreeHugger.Data.Migrations
{
    [Migration(1)]
    public class AuthenticationSeed : Migration
    {
        public override void Up()
        {
            Create.Table(nameof(IdentityRole))
                .WithColumn(nameof(IdentityRole.Id)).AsString().Nullable()
                .WithColumn(nameof(IdentityRole.Name)).AsString().Nullable()
                .WithColumn(nameof(IdentityRole.NormalizedName)).AsString().Nullable()
                .WithColumn(nameof(IdentityRole.ConcurrencyStamp)).AsString().Nullable();
            
            Create.Table(nameof(IdentityRoleClaim<string>))
                .WithColumn(nameof(IdentityRoleClaim<string>.Id)).AsInt32().Nullable()
                .WithColumn(nameof(IdentityRoleClaim<string>.RoleId)).AsString().Nullable()
                .WithColumn(nameof(IdentityRoleClaim<string>.ClaimType)).AsString().Nullable()
                .WithColumn(nameof(IdentityRoleClaim<string>.ClaimValue)).AsString().Nullable();
                
            Create.Table(nameof(IdentityUser))
                .WithColumn(nameof(IdentityUser.Id)).AsString().PrimaryKey()
                .WithColumn(nameof(IdentityUser.UserName)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.NormalizedUserName)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.Email)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.NormalizedEmail)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.EmailConfirmed)).AsBoolean().Nullable()
                .WithColumn(nameof(IdentityUser.PasswordHash)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.SecurityStamp)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.ConcurrencyStamp)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.PhoneNumber)).AsString().Nullable()
                .WithColumn(nameof(IdentityUser.PhoneNumberConfirmed)).AsBoolean().Nullable()
                .WithColumn(nameof(IdentityUser.TwoFactorEnabled)).AsBoolean().Nullable()
                .WithColumn(nameof(IdentityUser.LockoutEnd)).AsDateTime().Nullable()
                .WithColumn(nameof(IdentityUser.LockoutEnabled)).AsBoolean().Nullable()
                .WithColumn(nameof(IdentityUser.AccessFailedCount)).AsInt32().Nullable();

            Create.Table(nameof(IdentityUserClaim<string>))
                .WithColumn(nameof(IdentityUserClaim<string>.Id)).AsInt32().PrimaryKey()
                .WithColumn(nameof(IdentityUserClaim<string>.UserId)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserClaim<string>.ClaimType)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserClaim<string>.ClaimValue)).AsString().Nullable();

            Create.Table(nameof(IdentityUserLogin<string>))
                .WithColumn(nameof(IdentityUserLogin<string>.LoginProvider)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserLogin<string>.ProviderKey)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserLogin<string>.ProviderDisplayName)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserLogin<string>.UserId)).AsString().Nullable();

            Create.Table(nameof(IdentityUserRole<string>))
                .WithColumn(nameof(IdentityUserRole<string>.UserId)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserRole<string>.RoleId)).AsString().Nullable();
            
            Create.Table(nameof(IdentityUserToken<string>))
                .WithColumn(nameof(IdentityUserToken<string>.UserId)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserToken<string>.LoginProvider)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserToken<string>.Name)).AsString().Nullable()
                .WithColumn(nameof(IdentityUserToken<string>.Value)).AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table(nameof(IdentityUserToken<string>));
            Delete.Table(nameof(IdentityUserRole<string>));
            Delete.Table(nameof(IdentityUserLogin<string>));
            Delete.Table(nameof(IdentityUserClaim<string>));
            Delete.Table(nameof(IdentityUser));
            Delete.Table(nameof(IdentityRoleClaim<string>));
            Delete.Table(nameof(IdentityRole));
        }
    }
}