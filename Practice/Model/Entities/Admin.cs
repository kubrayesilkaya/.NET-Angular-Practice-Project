using Practice.Model.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Model.Entities
{
    [Table("Admin")]

    public class Admin:BaseEntity
    {
        [Key]
        public int IdAdmin { get; set; }
        public Guid GuidAdmin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
