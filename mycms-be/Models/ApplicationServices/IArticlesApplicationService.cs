using System.Collections.Generic;
using mycms.Models.ViewModels.Articles;

namespace mycms.Models.ApplicationServices
{
    public interface IArticlesApplicationService
    {
        IEnumerable<ArticleListItemViewModel> GetAll();
        ArticleViewModel Get(int id);
        void Create(ArticleViewModel model);
        void Update(ArticleViewModel model);
        void Delete(ArticleViewModel model);
    }
}