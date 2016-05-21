using System.ComponentModel.DataAnnotations;

namespace EduTestContract.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }       
        public string[] Roles { get; set; }
        [Required]
        public PersonalDetailModel PersonalDetail { get; set; }
    }
}