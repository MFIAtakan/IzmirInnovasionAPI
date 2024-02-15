using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserOperationDTO
    {
        public IEnumerable<int> Entries { get; set; }
        public int Result { get; set; }
    }
}
