namespace TreeHugger.Data.Models;

public class AchievementEntity
{
    public Guid? Id { get; set; }
    public Guid? EducatorId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Requirement { get; set; }
    public string? Icon { get; set; }
}