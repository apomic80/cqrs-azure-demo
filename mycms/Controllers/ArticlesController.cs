using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mycms.Models.ApplicationServices;
using mycms.Models.ViewModels.Articles;

namespace mycms.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesApplicationService service = null;

        public ArticlesController(
            IArticlesApplicationService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View(this.service.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Create(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(article);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = service.Get(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(article);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
            }
            return View(article);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(this.service.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(ArticleViewModel article)
        {
            try
            {
                this.service.Delete(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("",  
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.");
                return View(this.service.Get(article.Id));
            }
        }
    }
}