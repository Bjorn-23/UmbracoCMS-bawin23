using System.ComponentModel.DataAnnotations;

namespace UmbracoCMS_bawin23.Models;

public class CallbackRequestFormModel
{
    public string Name { get; set; } = null!;

    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,63}$", ErrorMessage = "You must enter a valid email address")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^\+?\d[\d\s]{7,20}$", ErrorMessage = "You must enter a valid phone number")]
    public string Phone { get; set; } = null!;
    public string Subject { get; set; } = null!;
}
