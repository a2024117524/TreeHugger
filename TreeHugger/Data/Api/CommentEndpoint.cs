using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Comment").WithTags(nameof(CommentEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.CommentEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var commentEntity = await db.CommentEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return commentEntity != null ? Results.Ok(commentEntity) : Results.NotFound();
                }
            })
                .WithName("GetCommentById")
                .WithOpenApi();
            
            group.MapPost("/", async (CommentEntity commentEntity, ApplicationDbContext db) =>
            {
                db.CommentEntities?.Add(commentEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Comment/{commentEntity.Id}", commentEntity);
            })
                .WithName("CreateComment")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.CommentEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.CommentEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteComment")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, CommentEntity commentEntity, ApplicationDbContext db) =>
            {
                if (db.CommentEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingCommentEntity = await db.CommentEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingCommentEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.CommentEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.LessonId, commentEntity.LessonId)
                                .SetProperty(p => p.UserId, commentEntity.UserId)
                                .SetProperty(p => p.Content, commentEntity.Content)
                                .SetProperty(p => p.Edited, commentEntity.Edited)
                                .SetProperty(p => p.Pinned, commentEntity.Pinned)
                                .SetProperty(p => p.TimeStamp, commentEntity.TimeStamp)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateComment")
                .WithOpenApi();
        }
    }
}