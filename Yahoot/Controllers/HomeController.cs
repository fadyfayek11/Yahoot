using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Yahoot.AppContext;
using Yahoot.Models;

namespace Yahoot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
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
        public async Task<IActionResult> Join([Required]string yourName, string gameId)
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
            var answer = await _context.Answers.AsNoTracking().Include(q => q.Question).Where(q => q.Question.QuizId == model.QuizId && q.QuestionId == model.QuestionId).ToListAsync();
            var index = answer.Select(x => x.IsCorrect).ToList();
            var i = index.IndexOf(true);
            if (i == model.Index) return Ok(new { success = true });
            return Ok(new { success = false });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}