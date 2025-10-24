using GymManagementBLL.View_Models.CategoryVM;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAllCategories();
    }
}
