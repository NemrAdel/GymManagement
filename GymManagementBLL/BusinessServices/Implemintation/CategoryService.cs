using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.CategoryVM;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            var Categories =_unitOfWork.GetRepository<Category>().GetAll();
            var Category = Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name=c.CategoryName,
            });
            return Category;
        }
    }
}
