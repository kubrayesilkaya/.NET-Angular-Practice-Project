using Practice.Model.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Model.Entities
{
    [Table("Users")]
    public class Users:BaseEntity
    {
        [Key]
        public int IdUser { get; set; }
        public Guid GuidUser {  get; set; }
        public int IdClient { get; set; }
        public int IdUserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PasswordClearCode { get; set; }
        public bool IsActive { get; set; }
    }

}
