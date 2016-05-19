using System.ComponentModel.DataAnnotations;

namespace TeacherWebApp.ViewModels
{
    public class LogOnViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}