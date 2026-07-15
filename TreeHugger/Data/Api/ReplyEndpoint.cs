using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class ReplyEndpoints
    {
        public static void MapReplyEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Reply").WithTags(nameof(ReplyEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ReplyEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var replyEntity = await db.ReplyEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return replyEntity != null ? Results.Ok(replyEntity) : Results.NotFound();
                }
            })
                .WithName("GetReplyById")
                .WithOpenApi();
            
            group.MapPost("/", async (ReplyEntity replyEntity, ApplicationDbContext db) =>
            {
                db.ReplyEntities?.Add(replyEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Reply/{replyEntity.Id}", replyEntity);
            })
                .WithName("CreateReply")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ReplyEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.ReplyEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteReply")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, ReplyEntity replyEntity, ApplicationDbContext db) =>
            {
                if (db.ReplyEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingReplyEntity = await db.ReplyEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingReplyEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.ReplyEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.CommentId, replyEntity.CommentId)
                                .SetProperty(p => p.UserId, replyEntity.UserId)
                                .SetProperty(p => p.Content, replyEntity.Content)
                                .SetProperty(p => p.Edited, replyEntity.Edited)
                                .SetProperty(p => p.Pinned, replyEntity.Pinned)
                                .SetProperty(p => p.TimeStamp, replyEntity.TimeStamp)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateReply")
                .WithOpenApi();
        }
    }
}