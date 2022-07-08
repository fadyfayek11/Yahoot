#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yahoot.AppContext;

public class User
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Quiz> Quizzes { get; set; }

}
public class Admin
{
    [ForeignKey("User")] public Guid Id { get; set; }
    public bool IsRoot { get; set; }
    public virtual User User { get; set; }
}