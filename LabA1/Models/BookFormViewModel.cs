using System.ComponentModel.DataAnnotations;

namespace LabA1.Models;

public class BookFormViewModel : IValidatableObject 
{
    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Tytuł: 2–200 znaków")]
    [Display(Name = "Tytuł")]
    public string Title { get; set; } = "";

    [Required, Isbn(false)]                        
    public string Isbn { get; set; } = "";

    [DataType(DataType.Date)] [NotInFuture]   
    [Display(Name = "Data wydania")]
    public DateTime? PublicationDate { get; set; }

    [Range(1, 20000)] [Display(Name = "Liczba stron")]
    public int NumPages { get; set; } = 100;

    [Range(0.01, 10000, ErrorMessage = "Cena musi być dodatnia")]
    public decimal Price { get; set; }

    [Required, Display(Name = "Autor")]
    public string Author { get; set; } = "";

    [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Kod musi być w formacie np. ABC-1234")]
    [Display(Name = "Kod wydawcy")]
    public string PublisherCode { get; set; } = "";

  
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
  
        if (PublicationDate.HasValue && PublicationDate.Value.Year < 1900 && Price > 100m)
        {
           
            yield return new ValidationResult(
                "Książka sprzed 1900 r. nie może kosztować więcej niż 100 zł.",
                new[] { nameof(Price) });
        }
    }

}