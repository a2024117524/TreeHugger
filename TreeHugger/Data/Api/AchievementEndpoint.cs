using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class AchievementEndpoints
    {
        public static void MapAchievementEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Achievement").WithTags(nameof(AchievementEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.AchievementEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var achievementEntity = await db.AchievementEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return achievementEntity != null ? Results.Ok(achievementEntity) : Results.NotFound();
                }
            })
                .WithName("GetAchievementById")
                .WithOpenApi();
            
            group.MapPost("/", async (AchievementEntity achievementEntity, ApplicationDbContext db) =>
            {
                db.AchievementEntities?.Add(achievementEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Achievement/{achievementEntity.Id}", achievementEntity);
            })
                .WithName("CreateAchievement")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.AchievementEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.AchievementEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteAchievement")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, AchievementEntity achievementEntity, ApplicationDbContext db) =>
            {
                if (db.AchievementEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingAchievementEntity = await db.AchievementEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingAchievementEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.AchievementEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.EducatorId, achievementEntity.EducatorId)
                                .SetProperty(p => p.Name, achievementEntity.Name)
                                .SetProperty(p => p.Description, achievementEntity.Description)
                                .SetProperty(p => p.Requirement, achievementEntity.Requirement)
                                .SetProperty(p => p.Icon, achievementEntity.Icon)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateAchievement")
                .WithOpenApi();
        }
    }
}