using System.ComponentModel.DataAnnotations;

namespace DaisyStudy.ViewModels.System.Users;

public class UserViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Tên")]
    public string? FirstName { get; set; }

    [Display(Name = "Họ")]
    public string? LastName { get; set; }

    [Display(Name = "Số điện thoại")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Tài khoản")]
    public string? UserName { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Ngày sinh")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Dob { get; set; }

    public IList<string> Roles { get; set; }
}