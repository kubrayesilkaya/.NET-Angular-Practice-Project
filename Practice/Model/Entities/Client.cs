using Practice.Model.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Model.Entities
{
    [Table("Client")]
    public class Client:BaseEntity
    {
        [Key]
        public int IdClient { get; set; }
        public Guid GuidClient { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
