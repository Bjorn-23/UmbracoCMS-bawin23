using System.ComponentModel.DataAnnotations;

namespace UmbracoCMS_bawin23.Models;

public class ContactFormModel
{
    public string Name { get; set; } = null!;

    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "You must enter a valid email address")]
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
}
