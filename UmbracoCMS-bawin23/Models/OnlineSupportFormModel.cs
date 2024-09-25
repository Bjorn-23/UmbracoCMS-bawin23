using System.ComponentModel.DataAnnotations;

namespace UmbracoCMS_bawin23.Models;

public class OnlineSupportFormModel
{
	[Required]
	[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w{2,}$", ErrorMessage = "You must enter a valid email address")]
	public string Email { get; set; } = null!;
}
