using Shared;
using System.Text;

namespace DataUploadValidation;

internal class OrderValidator
{
    public string Validate(VeryBigShoeOrder t)
    {
        StringBuilder errors = new();

        if (string.IsNullOrWhiteSpace(t.CustomerName))
            errors.AppendLine("Customer Name must be provided!");

        if (!RegexUtilities.IsValidEmail(t.CustomerEmail))
            errors.AppendLine($"Customer Email \"{t.CustomerEmail}\", must be a valid email address!");

        DateUtilities dateUtilities = new(); // use new(new DateTime(2021, 4, 19)); or earlier to pass test data
        if (!dateUtilities.IsWorkingDaysInFutureValid(t.DateRequired, 10))
            errors.AppendLine($"Date \"{t.DateRequired.ToString("dd/MM/yyyy")}\", must be valid and at least 10 working days into the future!");

        if (!(t.Size >= 11.5) && !(t.Size <= 15) && Math.Round(t.Size * 10) % 5 != 0)
            errors.AppendLine($"Size \"{t.Size}\", must be 11.5 to 15 including half sizes!");

        if (t.Quantity % 1000 != 0)
            errors.AppendLine($"Quantity \"{t.Quantity}\", must be in multiples of 1000!");

        return errors.ToString();
    }
}

