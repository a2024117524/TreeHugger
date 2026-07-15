using Microsoft.EntityFrameworkCore;
using TreeHugger.Data.Models;

namespace TreeHugger.Data.Api
{
    public static class ProfileEndpoints
    {
        public static void MapProfileEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Profile").WithTags(nameof(ProfileEntity));
            
            group.MapGet("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ProfileEntities == null) 
                {
                    return Results.NotFound();
                }
                else
                {
                    var profileEntity = await db.ProfileEntities.FirstOrDefaultAsync(model => model.Id == id);
                    return profileEntity != null ? Results.Ok(profileEntity) : Results.NotFound();
                }
            })
                .WithName("GetProfileById")
                .WithOpenApi();
            
            group.MapPost("/", async (ProfileEntity profileEntity, ApplicationDbContext db) =>
            {
                db.ProfileEntities?.Add(profileEntity);
                await db.SaveChangesAsync();
                return Results.Created($"/api/Profile/{profileEntity.Id}", profileEntity);
            })
                .WithName("CreateProfile")
                .WithOpenApi();
            
            group.MapDelete("/{id}", async (Guid id, ApplicationDbContext db) =>
            {
                if (db.ProfileEntities == null) 
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var rowsAffected = await db.ProfileEntities
                        .Where(p => p.Id == id)
                        .ExecuteDeleteAsync();
                
                    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                }
                
            })
                .WithName("DeleteProfile")
                .WithOpenApi();

            group.MapPut("/{id}", async (Guid id, ProfileEntity profileEntity, ApplicationDbContext db) =>
            {
                if (db.ProfileEntities == null)
                {
                    return Results.StatusCode(500);
                }
                else
                {
                    var existingProfileEntity = await db.ProfileEntities.FirstOrDefaultAsync(p => p.Id == id);
                
                    if(existingProfileEntity ==null)
                    {
                        return Results.NotFound();
                    }
                    else
                    {
                        var rowsAffected = await db.ProfileEntities
                            .Where(p => p.Id == id)
                            .ExecuteUpdateAsync(setters => setters
                                .SetProperty(p => p.UserId, profileEntity.UserId)
                                .SetProperty(p => p.Avatar, profileEntity.Avatar)
                            );
                    
                        return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
                    }
                }
                
            })
                .WithName("UpdateProfile")
                .WithOpenApi();
        }
    }
}