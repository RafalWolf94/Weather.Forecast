using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Adapters.Out.TechnicalStuff.Json;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    private const string DateTimeFormat = "dd.MM.yyyy HH:mm:ss";

    private static readonly string[] DateTimeFormats =
    {
        "dd.MM.yyyy HH:mm:ss",
        "M/d/yyyy h:mm:ss tt",
        "MM/dd/yyyy h:mm:ss tt",
        "yyyy-MM-ddTHH:mm:ss.fffZ"
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var date = reader.GetString();
        if (string.IsNullOrWhiteSpace(date))
            throw new InvalidOperationException();

        try
        {
            return DateTime.ParseExact(date, DateTimeFormat, CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            foreach (var format in DateTimeFormats)
            {
                if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate;
                }
            }

            throw new InvalidOperationException($"Date '{date}' was not recognized as a valid DateTime.");
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateTimeFormat));
    }
}