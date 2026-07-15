using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class ChoiceEndpoints
    {
        public static void MapChoiceEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Choice").WithTags(nameof(ChoiceEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ChoiceEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var choiceEntity = await db.ChoiceEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return choiceEntity != null ? Results.Ok(choiceEntity) : Results.NotFound();
                }
            })
                .WithName("GetChoiceById")
                .WithOpenApi();
            
            group.MapPost("/", async (ChoiceEntity choiceEntity, ApplicationDbContext db) =>
            {
                db.ChoiceEntities?.Add(choiceEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Choice/{choiceEntity.Id}", choiceEntity);
            })
                .WithName("CreateChoice")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ChoiceEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.ChoiceEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteChoice")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, ChoiceEntity choiceEntity, ApplicationDbContext db) =>
            {
                if (db.ChoiceEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingChoiceEntity = await db.ChoiceEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingChoiceEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.ChoiceEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.QuestionId, choiceEntity.QuestionId)
                                .SetProperty(p => p.Text, choiceEntity.Text)
                                .SetProperty(p => p.Answer, choiceEntity.Answer)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateChoice")
                .WithOpenApi();
        }
    }
}