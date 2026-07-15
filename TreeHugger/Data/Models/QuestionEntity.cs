namespace TreeHugger.Data.Models;

public class QuestionEntity
{
    public Guid? Id { get; set; }
    public Guid? QuizId { get; set; }
    public string? Text { get; set; }
    public decimal? Points { get; set; }
}