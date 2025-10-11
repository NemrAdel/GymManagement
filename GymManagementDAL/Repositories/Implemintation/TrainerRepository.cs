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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _dbContext;

        //public readonly GymDbContext _dbContext = new GymDbContext();
        public TrainerRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers()
        {
            return _dbContext.Trainers.ToList();
        }

        public Trainer? GetTrainerById(int id)
        {
            return _dbContext.Trainers.Find(id);
        }

        public int Remove(int id)
        {
            var trainer = GetTrainerById(id);
            if (trainer == null) return 0;
            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();
        }

        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
