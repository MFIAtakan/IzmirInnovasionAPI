using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table(name:"User Operations")]
    public class UserOperations : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set ; } = DateTime.Now;
        public DateTime UpdatedAt { get ; set ; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public List<int> Entries { get; set; }
        public int Result { get; set; }

    }
}
