using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Models;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implemintation
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _dbContext;

        public CategoryRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public int Remove(int id)
        {
            var category = GetCategoryById(id);
            if (category == null) return 0;
            _dbContext.Categories.Remove(category);
            return _dbContext.SaveChanges();
        }

        public int Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return _dbContext.SaveChanges();
        }
    }
}
