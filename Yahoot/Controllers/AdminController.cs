using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Yahoot.AppContext;
using Yahoot.Hubs;
using Yahoot.Models;

namespace Yahoot.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<YahootHub> _hub;
        public AdminController(AppDbContext context, IHubContext<YahootHub> hub)
        {
            _context = context;
            _hub = hub;
        }
        
        // GET: AdminController
        public ActionResult Index()
        {
            return View(_context.Quizzes.ToList());
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            var quiz = _context.Quizzes.Include(x => x.Users).FirstOrDefault(q => q.Id == id);
            if (quiz is null) return Redirect(nameof(Index));

            return View(quiz);
        }
        //Delete user
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == Guid.Parse(id));
            if (user is null) return Ok();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        // GET: AdminController/Create
        public static int CurrentQuestion;
        public async Task<ActionResult> QuizQuestion(int id,int questionId)
        {
            int qId = 0;
            if (questionId == 0)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.QuizId == id);
                if (question != null)  qId = question.Id;
                CurrentQuestion = 0;
            }
            var quizQuestion = await _context.Questions.AsNoTracking().Include(q => q.Answers).FirstOrDefaultAsync(q => q.QuizId == id && (questionId == 0 ? q.Id == qId : q.Id > questionId));
           

            var total =  _context.Questions.Count(t => t.QuizId == id);
            if (quizQuestion == null)
            {
                CurrentQuestion = 0;
                await _hub.Clients.All.SendAsync("AdminSendQuestionId", 0, id);
                return View(new QuestionViewModel(0, 0, null, null, 0, 0));
            }
            TempData["questionId"] = quizQuestion.Id;
            TempData["quizId"] = quizQuestion.QuizId;
            CurrentQuestion +=1;
            var dto = new QuestionViewModel(quizQuestion.QuizId, quizQuestion.Id, quizQuestion.QuestionName,
                quizQuestion.Answers,total,CurrentQuestion);

            await _hub.Clients.All.SendAsync("AdminSendQuestionId", quizQuestion.Id, id);
            return View(dto);
        }
        
        //Admin Show the right answer 
        public async Task<ActionResult> ShowCorrectAnswer(int quizId,int questionId)
        {
            var answer = await _context.Answers.AsNoTracking().Include(q => q.Question).Where(q => q.Question.QuizId == quizId &&  q.QuestionId == questionId).ToListAsync();
            var index = answer.Select(x => x.IsCorrect).ToList();
            var i = index.IndexOf(true);
            await _hub.Clients.All.SendAsync("AdminSendTheRightAnswer", answer[i].AnswerName);
            return Ok(new{success = true,data=$"{i}"});
        }

        public async Task<ActionResult> GetTopPlayers(int quizId)
        {
            var players =  _context.Degrees.Include(x=>x.User).Where(q => q.QuizId == quizId).OrderByDescending(x=>x.UserDegree).ThenBy(d=>d.Time).Take(15);
            return View(players.Select(x=>new UserDegree(){Degree = x.UserDegree,Name = x.User.Name}));
        }
    }
}
