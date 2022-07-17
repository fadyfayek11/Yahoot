using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Yahoot.AppContext;
using Yahoot.Hubs;
using Yahoot.Models;

namespace Yahoot.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<YahootHub> _hub;
        public HomeController(AppDbContext context, IHubContext<YahootHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(string gameId)
        {
            var quiz =  _context.Quizzes.Include(x=>x.Users).FirstOrDefault(q => q.QuizCode == gameId);
            if (quiz is null) return Redirect(nameof(Index));

            return View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string yourName, string gameId)
        {
            var quiz = _context.Quizzes.Include(x => x.Users).FirstOrDefault(q => q.QuizCode == gameId);
            if (quiz is null) return Redirect(nameof(Index));
            var user = new User()
            {
                Name = yourName,
            };
            _context.Users.Add(user);
            quiz.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Quizzes.Add(quiz);
            _context.Degrees.Add(new Degree() { User = user, Quiz = quiz, UserDegree = 0 });

            await _context.SaveChangesAsync();
            return View(user);
        }

        public async Task<ActionResult> CheckCorrectAnswer(StudentAnswer model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (user is null) return NotFound();

            var answer = await _context.Answers.AsNoTracking().Include(q => q.Question).Where(q => q.Question.QuizId == model.QuizId && q.QuestionId == model.QuestionId).ToListAsync();
            var index = answer.Select(x => x.IsCorrect).ToList();
            var i = index.IndexOf(true);

            if (i == model.Index)
            {
                var oldScore =
                    await _context.Degrees.FirstOrDefaultAsync(
                        x => x.QuizId == model.QuizId && x.UserId == model.UserId);
                if (oldScore is null)
                {
                    oldScore = new Degree();
                    oldScore.UserDegree = 0;
                }

                oldScore.UserDegree = 10 + oldScore.UserDegree;
                oldScore.Time = DateTime.Now;
                
                _context.Degrees.Update(oldScore);
                await _context.SaveChangesAsync();
                await _hub.Clients.All.SendAsync("StudentSendAnswerToAdmin", model.Index);
                return Ok(new { success = true ,score = oldScore.UserDegree });
            }
            await _hub.Clients.All.SendAsync("StudentSendAnswerToAdmin", model.Index);
            return Ok(new { success = false });
        }
       
    }
}