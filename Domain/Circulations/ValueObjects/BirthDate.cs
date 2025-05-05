using System.Globalization;

namespace Domain.Circulations.ValueObjects;

public sealed record BirthDate
{
    public DateTimeOffset Date { get; }
    public string FormattedDate { get; }

    public BirthDate(string dateString)
    {
        if (!DateTimeOffset.TryParseExact(dateString, format: "yyyy.MM.dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw new Exception(message: "Formato de fecha inválido");
        }
        
        Date = date;
        FormattedDate = dateString;
    }

    public override string ToString() => FormattedDate;
}