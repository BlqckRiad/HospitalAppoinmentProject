using System.ComponentModel.DataAnnotations;

namespace HospitalApp.WiewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }

}