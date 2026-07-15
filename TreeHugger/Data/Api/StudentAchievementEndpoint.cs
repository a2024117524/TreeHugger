using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class StudentAchievementEndpoints
    {
        public static void MapStudentAchievementEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/StudentAchievement").WithTags(nameof(StudentAchievementEntity));

            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.StudentAchievementEntities == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    var studentAchievementEntity = await db.StudentAchievementEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return studentAchievementEntity != null ? Results.Ok(studentAchievementEntity) : Results.NotFound();
                }
            })
                .WithName("GetStudentAchievementById")
                .WithOpenApi();

            group.MapPost("/", async (StudentAchievementEntity studentAchievementEntity, ApplicationDbContext db) =>
            {
                db.StudentAchievementEntities?.Add(studentAchievementEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/StudentAchievement/{studentAchievementEntity.Id}", studentAchievementEntity);
            })
                .WithName("CreateStudentAchievement")
                .WithOpenApi();

            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.StudentAchievementEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.StudentAchievementEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();

                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
            })
                .WithName("DeleteStudentAchievement")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, StudentAchievementEntity studentAchievementEntity, ApplicationDbContext db) =>
            {
                if (db.StudentAchievementEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingStudentAchievementEntity = await db.StudentAchievementEntities.FirstOrDefaultAsync(p => p.Id == id);

                    if (existingStudentAchievementEntity == null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.StudentAchievementEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.ProfileId, studentAchievementEntity.ProfileId)
                                .SetProperty(p => p.AchievementId, studentAchievementEntity.AchievementId)
                                .SetProperty(p => p.TimeStamp, studentAchievementEntity.TimeStamp)
                            );

                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
            })
                .WithName("UpdateStudentAchievement")
                .WithOpenApi();
        }
    }
}
