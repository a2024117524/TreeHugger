namespace TreeHugger.Data.Models;

public class LessonEntity
{
    public Guid? Id { get; set; }
    public Guid? EducatorId { get; set; }
    public string? Banner { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public DateTime? TimeStamp { get; set; }
}