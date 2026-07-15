namespace TreeHugger.Data.Models;

public class QuizEntity
{
    public Guid? Id { get; set; }
    public Guid? LessonId { get; set; }
    public string? Banner { get; set; }
    public string? Title { get; set; }
    public decimal? Score { get; set; }
}