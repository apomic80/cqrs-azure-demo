using System.Collections.Generic;
using System.Linq;
using mycms.Data.Entities;
using mycms.Data.Infrastructure;
using mycms.Models.ViewModels.Articles;

namespace mycms.Models.ApplicationServices
{
    public class ArticlesApplicationService : IArticlesApplicationService
    {
        private readonly IRepository<Article, int> repository = null;

        public ArticlesApplicationService(
            IRepository<Article, int> repository)
        {
            this.repository = repository;
        }
        
        public IEnumerable<ArticleListItemViewModel> GetAll()
        {
            return this.repository.GetAll()
                .Select(x => new ArticleListItemViewModel() 
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author
                }).ToList();
        }

        public ArticleViewModel Get(int id)
        {
            var article = this.repository.Get(id);
            return new ArticleViewModel() 
            {
                Id = article.Id,
                Title = article.Title,
                Subtitle = article.Subtitle,
                Content = article.Content,
                Author = article.Author
            };
        }

        public void Create(ArticleViewModel model)
        {
            this.repository.Create(this.getArticle(model));
            this.repository.SaveChanges();
        }

        public void Update(ArticleViewModel model)
        {
            this.repository.Update(this.getArticle(model));
            this.repository.SaveChanges();
        }

        public void Delete(ArticleViewModel model)
        {
            this.repository.Delete(this.getArticle(model));
            this.repository.SaveChanges();
        }

        private Article getArticle(ArticleViewModel model)
        {
            return new Article()
            {
                Id = model.Id,
                Title = model.Title,
                Subtitle = model.Subtitle,
                Content = model.Content,
                Author = model.Author
            };
        }
    }
}