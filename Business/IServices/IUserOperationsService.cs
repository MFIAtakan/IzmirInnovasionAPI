using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IUserOperationsService
    {
        public void ApplyUserEntrances(List<int> values, int maxVal, ApplicationUser user);
        public int CalculateMaxValue(List<int> values);
        public List<UserOperations> GetAllUserOperations();

    }
}
