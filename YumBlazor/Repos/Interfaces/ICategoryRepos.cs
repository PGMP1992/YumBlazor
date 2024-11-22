using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface ICategoryRepos
    {
        public Category Create(Category category);
        public Category Update(Category category);
        public bool Delete(int id);
        public Category Get(int id);
        public IEnumerable<Category> GetAll();
    }
}
