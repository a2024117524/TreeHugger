using FluentMigrator;
using Microsoft.AspNetCore.Identity;

namespace TreeHugger.Data.Migrations
{
    [Migration(3)] 
    public class RoleSeed : Migration
    {
        public override void Up()
        {
            Insert.IntoTable(nameof(IdentityRole))
                .Row(new
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });

            Insert.IntoTable(nameof(IdentityRole))
                .Row(new
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Educator",
                    NormalizedName = "EDUCATOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
        }

        public override void Down()
        {
            Delete.FromTable(nameof(IdentityRole)).AllRows();
        }
    }
}