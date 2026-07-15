namespace TreeHugger.Data.Models;

public class ChoiceEntity
{
    public Guid? Id { get; set; }
    public Guid? QuestionId { get; set; }
    public string? Text { get; set; }
    public bool? Answer { get; set; }
}