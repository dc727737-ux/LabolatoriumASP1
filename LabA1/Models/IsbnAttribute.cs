using System.ComponentModel.DataAnnotations;

namespace LabA1.Models;

public class IsbnAttribute : ValidationAttribute
{
    private readonly bool _requiredControlSum;

    public IsbnAttribute(bool requiredControlSum = true)
    {
        _requiredControlSum = requiredControlSum;

    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            ErrorMessage = "Isbn nie może mieć wartości null";
            return false;
        }

        if (value is not string isbn)
        {
            ErrorMessage = "Isbn musi być podany jako łańcuch";
            return false;
        }

        if (isbn.Length != 10 && isbn.Length != 13)
        {
            ErrorMessage = "Isbn musi miec długosc 10 lub 13";
            return false;
        }
        return true;
    }
}

    
    