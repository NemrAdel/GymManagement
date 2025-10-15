using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers is null || !trainers.Any()) return [];

            var TrainersViewModels = trainers.Select(t => new TrainerViewModel
            {
                Name=t.Name,
                Email =t.Email,
                Phone=t.Phone,
                Specialties =t.Specialties.ToString(),
            });
            return TrainersViewModels;
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            var EmailExists = _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Email == createTrainer.Email);
            var PhoneExists = _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Phone == createTrainer.Phone);
            if (EmailExists.Any() || PhoneExists.Any()) return false;

            var trainer = new Trainer
            {
                Name = createTrainer.Name,
                Email = createTrainer.Email,
                Phone = createTrainer.Phone,
                Specialties = createTrainer.Specialties,
                DateOfBirth = createTrainer.DateOfBirth,
                Address = {
                    BuildingNumber = createTrainer.BuildingNumber,
                    Street = createTrainer.Street,
                    City = createTrainer.City,
                }
            };
            _unitOfWork.GetRepository<Trainer>().Add(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }


        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;
            return new TrainerViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialties = trainer.Specialties.ToString(),
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                Address = trainer.Address is not null ? $"{trainer.Address.BuildingNumber}, {trainer.Address.Street}, {trainer.Address.City}" : null
            };

        }

        public TrainerToUpdateViewModel? GetTrainerDetailsToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if(trainer is null) return null;
            return new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address?.BuildingNumber ?? 0,
                Street = trainer.Address?.Street ?? string.Empty,
                City = trainer.Address?.City ?? string.Empty,
                Specialties = trainer.Specialties
            };

        }

        public bool RemoveTrainer(int trainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
                if (trainer is null || HasActiveSessions(trainerId)) return false;
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
                if (trainer is null) return false;
                //var emailExists = _unitOfWork.GetRepository<Trainer>()
                //    .GetAll(t => t.Email == trainerToUpdate.Email).Any();
                //var phoneExists = _unitOfWork.GetRepository<Trainer>()
                //    .GetAll(t => t.Phone == trainerToUpdate.Phone).Any();

                if (IsEmailExist(trainer.Email) || IsPhoneExist(trainer.Phone)) return false;

                trainer.Email = trainerToUpdate.Email;
                trainer.Phone = trainerToUpdate.Phone;
                trainer.Specialties = trainerToUpdate.Specialties;
                trainer.Address.BuildingNumber = trainerToUpdate.BuildingNumber;
                trainer.Address.Street = trainerToUpdate.Street;
                trainer.Address.City = trainerToUpdate.City;
                trainer.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Trainer>().Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }


        }
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        } // to use in phone and email validation as short method


        private bool HasActiveSessions(int trainerId)
        {
            var hasActiveSessions = _unitOfWork.GetRepository<Session>()
                .GetAll(x => x.TrainerId == trainerId && x.EndDate >= DateTime.Now);
            return hasActiveSessions.Any();
        }
    }
}
