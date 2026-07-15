namespace TreeHugger.Data.Models;

public class CommentEntity
{
    public Guid? Id { get; set; }
    public Guid? LessonId { get; set; }
    public string? UserId { get; set; }
    public string? Content { get; set; }
    public bool? Edited { get; set; }
    public bool? Pinned { get; set; }
    public DateTime? TimeStamp { get; set; }
}