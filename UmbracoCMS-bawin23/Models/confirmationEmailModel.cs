﻿namespace UmbracoCMS_bawin23.Models;

public class ConfirmationEmailModel
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string HtmlBody { get; set; } = null!;
    public string PlainText { get; set; } = null!;
}

