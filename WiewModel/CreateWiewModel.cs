using System.ComponentModel.DataAnnotations;

namespace HospitalApp.WiewModel
{
    public class CreateWiewModel
    {

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolalar Eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }
    }
}