using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category? GetCategoryById(int id);
        int Add(Category category);  // return number of affected rows
        int Update(Category category);
        int Remove(int id);
    }
}
