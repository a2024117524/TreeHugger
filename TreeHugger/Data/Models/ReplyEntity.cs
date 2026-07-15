namespace TreeHugger.Data.Models;

public class ReplyEntity
{
    public Guid? Id { get; set; }
    public Guid? CommentId { get; set; }
    public string? UserId { get; set; }
    public string? Content { get; set; }
    public bool? Edited { get; set; }
    public bool? Pinned { get; set; }
    public DateTime? TimeStamp { get; set; }
}