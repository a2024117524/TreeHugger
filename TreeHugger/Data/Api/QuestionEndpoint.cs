using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class QuestionEndpoints
    {
        public static void MapQuestionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Question").WithTags(nameof(QuestionEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.QuestionEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var questionEntity = await db.QuestionEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return questionEntity != null ? Results.Ok(questionEntity) : Results.NotFound();
                }
            })
                .WithName("GetQuestionById")
                .WithOpenApi();
            
            group.MapPost("/", async (QuestionEntity questionEntity, ApplicationDbContext db) =>
            {
                db.QuestionEntities?.Add(questionEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Question/{questionEntity.Id}", questionEntity);
            })
                .WithName("CreateQuestion")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.QuestionEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.QuestionEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteQuestion")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, QuestionEntity questionEntity, ApplicationDbContext db) =>
            {
                if (db.QuestionEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingQuestionEntity = await db.QuestionEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingQuestionEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.QuestionEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.QuizId, questionEntity.QuizId)
                                .SetProperty(p => p.Text, questionEntity.Text)
                                .SetProperty(p => p.Points, questionEntity.Points)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateQuestion")
                .WithOpenApi();
        }
    }
}