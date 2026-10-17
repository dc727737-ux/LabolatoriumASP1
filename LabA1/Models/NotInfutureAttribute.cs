using System.ComponentModel.DataAnnotations;

namespace LabA1.Models;

public class NotInFutureAttribute : ValidationAttribute
{
    public NotInFutureAttribute() => ErrorMessage = "Data nie może być z przyszłości";
    public override bool IsValid(object? value) => value is not DateTime d || d.Date <= DateTime.Today;
}