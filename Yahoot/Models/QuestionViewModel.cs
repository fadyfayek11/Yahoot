#nullable disable
using Yahoot.AppContext;
namespace Yahoot.Models;


public record QuestionViewModel(int QuizId,int QuestionId,string Question, ICollection<Answer> Answers);

