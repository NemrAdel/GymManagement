using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAllTrainers();
        Trainer? GetTrainerById(int id);
        int Add(Trainer trainer);  // return number of affected rows
        int Update(Trainer trainer);
        int Remove(int id);
    }
}
