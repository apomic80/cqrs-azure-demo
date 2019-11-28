using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mycms.Data.Entities;
using mycms.Data.Infrastructure;
using mycms.Models;
using mycms.Models.ViewModels.Home;

namespace mycms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IModelReader<Article> reader = null;

        public HomeController(
            IModelReader<Article> reader)
        {
            this.reader = reader;
        }

        public IActionResult Index()
        {
            var result = this.reader
                .Select(x => new ArticleItemViewModel(){
                    Id = x.Id,
                    Title = x.Title,
                    Subtitle = x.Subtitle,
                    Author = x.Author
                }).ToList();

            return View(result);
        }
        public IActionResult Article(int id)
        {
            var result = this.reader
                .Where(x => x.Id == id)
                .Select(x => new ArticleViewModel(){
                    Id = x.Id,
                    Title = x.Title,
                    Subtitle = x.Subtitle,
                    Content = x.Content,
                    Author = x.Author
                }).SingleOrDefault();

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
