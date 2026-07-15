using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class LessonEndpoints
    {
        public static void MapLessonEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Lesson").WithTags(nameof(LessonEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.LessonEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var lessonEntity = await db.LessonEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return lessonEntity != null ? Results.Ok(lessonEntity) : Results.NotFound();
                }
            })
                .WithName("GetLessonById")
                .WithOpenApi();
            
            group.MapPost("/", async (LessonEntity lessonEntity, ApplicationDbContext db) =>
            {
                db.LessonEntities?.Add(lessonEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Lesson/{lessonEntity.Id}", lessonEntity);
            })
                .WithName("CreateLesson")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.LessonEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.LessonEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteLesson")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, LessonEntity lessonEntity, ApplicationDbContext db) =>
            {
                if (db.LessonEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingLessonEntity = await db.LessonEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingLessonEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.LessonEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.EducatorId, lessonEntity.EducatorId)
                                .SetProperty(p => p.Banner, lessonEntity.Banner)
                                .SetProperty(p => p.Content, lessonEntity.Content)
                                .SetProperty(p => p.Description, lessonEntity.Description)
                                .SetProperty(p => p.TimeStamp, lessonEntity.TimeStamp)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateLesson")
                .WithOpenApi();
        }
    }
}