using System.Text.RegularExpressions;

namespace Core.Domain.Models.ValueObjects;

public readonly record struct Email : IValueObject<string>
{
    private static readonly Regex emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private readonly string _value;

    public string Value
    {
        get => _value;
        init => _value = Validate(value);
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format.", nameof(value));

        return value;
    }

    public Email(string value)
    {
        _value = Validate(value);
    }

    public static bool TryParse(string value, out Email email)
    {
        email = new Email(value);
        return true;
    }

    private static bool IsValidEmail(string value)
        => emailRegex.IsMatch(value);
}