#nullable disable
using Yahoot.AppContext;
namespace Yahoot.Models;


public record QuestionViewModel(int QuizId,int QuestionId,string Question, ICollection<Answer> Answers,int Total,int Current);

public class StudentAnswer
{
    public int Index { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public Guid UserId { get; set; }
}