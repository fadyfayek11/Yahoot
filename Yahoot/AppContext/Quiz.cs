#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yahoot.AppContext;

public class Quiz
{
    [Key] public int Id { get; set; }
    public string QuizName { get; set; }
    public string QuizCode { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
}

public class Question
{
    [Key] public int Id { get; set; }
    public string QuestionName { get; set; }
    public virtual ICollection<Answer> Answers { get; set; }
    [ForeignKey("Quiz")] public int QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }
}

public class Answer
{
    [Key] public int Id { get; set; }
    public string AnswerName { get; set; }
    public bool IsCorrect { get; set; }
    [ForeignKey("Question")] public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
}

public class Degree
{
    [Key] public int Id { get; set; }
    [ForeignKey("Quiz")] public int QuizId { get; set; }
    [ForeignKey("User")] public Guid UserId { get; set; }
    public int UserDegree { get; set; }
    public virtual User User { get; set; }
    public virtual Quiz Quiz { get; set; }
}