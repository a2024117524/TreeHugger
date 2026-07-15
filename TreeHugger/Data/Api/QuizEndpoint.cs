using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class QuizEndpoints
    {
        public static void MapQuizEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Quiz").WithTags(nameof(QuizEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.QuizEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var quizEntity = await db.QuizEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return quizEntity != null ? Results.Ok(quizEntity) : Results.NotFound();
                }
            })
                .WithName("GetQuizById")
                .WithOpenApi();
            
            group.MapPost("/", async (QuizEntity quizEntity, ApplicationDbContext db) =>
            {
                db.QuizEntities?.Add(quizEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Quiz/{quizEntity.Id}", quizEntity);
            })
                .WithName("CreateQuiz")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.QuizEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.QuizEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteQuiz")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, QuizEntity quizEntity, ApplicationDbContext db) =>
            {
                if (db.QuizEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingQuizEntity = await db.QuizEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingQuizEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.QuizEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.LessonId, quizEntity.LessonId)
                                .SetProperty(p => p.Banner, quizEntity.Banner)
                                .SetProperty(p => p.Title, quizEntity.Title)
                                .SetProperty(p => p.Score, quizEntity.Score)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateQuiz")
                .WithOpenApi();
        }
    }
}