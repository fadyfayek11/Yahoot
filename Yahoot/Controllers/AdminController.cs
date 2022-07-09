using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yahoot.AppContext;
using Yahoot.Models;

namespace Yahoot.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult> DeleteUser(string name)
        {
            name = name.Trim();
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Name == name);
            if (user is null) return Ok();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        // GET: AdminController/Create
        public static int CurrentQuestion;
        public async Task<ActionResult> QuizQuestion(int id,int questionId)
        {
            var quizQuestion = await _context.Questions.AsNoTracking().Include(q=>q.Answers).FirstOrDefaultAsync(q => q.QuizId == id && questionId == 0? q.Id == 1:q.Id == questionId+1);
            if (quizQuestion is null) return Ok();
            var dto = new QuestionViewModel(quizQuestion.QuizId, quizQuestion.Id, quizQuestion.QuestionName,
                quizQuestion.Answers);
            return View(dto);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
