using Business.IServices;
using Data.Data;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserOperationsServiceImpl : IUserOperationsService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserOperationsServiceImpl(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void ApplyUserEntrances(List<int> values,int maxVal,ApplicationUser user)
        {
            var userOperationsRepo = _unitOfWork.UserOperationsRepository;
            UserOperations operations = new UserOperations()
            {
                CreatedAt = DateTime.Now,
                Entries = values,
                Result = maxVal,
                UpdatedAt = DateTime.Now,
                UserId = user.Id,
                UserName = user.UserName


            };
            try
            {

                userOperationsRepo.Insert(operations);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int CalculateMaxValue(List<int> values)
        {
            if (values == null || values.Count == 0)
            {
                throw new ArgumentException("Liste boş olamaz!");
            }

            int maxValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                }
            }

            return maxValue;
        }

        public List<UserOperations> GetAllUserOperations()
        {
            try
            {
                var userOperationsRepo = _unitOfWork.UserOperationsRepository;
                return userOperationsRepo.Get().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu.", ex);
            }
        }
    }
}
