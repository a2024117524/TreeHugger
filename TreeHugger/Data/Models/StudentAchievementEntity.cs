namespace TreeHugger.Data.Models;

public class StudentAchievementEntity
{
    public Guid? Id { get; set; }
    public Guid? ProfileId { get; set; }
    public Guid? AchievementId { get; set; }
    public DateTime? TimeStamp { get; set; }
}