using System.Text;
using System.Text.RegularExpressions;

namespace PortfolioBackend.Helpers;

public static class InputSanitizer
{
    private static readonly Regex ControlChars = new(@"[\u0000-\u0008\u000B\u000C\u000E-\u001F]", RegexOptions.Compiled);

    public static string NormalizeWhitespace(string s) =>
        Regex.Replace((s ?? string.Empty).Trim(), @"\s+", " ");

    public static string RemoveControlChars(string s) =>
        ControlChars.Replace(s ?? string.Empty, string.Empty);

    // For anything that will end up in headers (Subject, etc.)
    public static string StripCrlf(string s) =>
        (s ?? string.Empty).Replace("\r", " ").Replace("\n", " ");

    // Compose a safe plain-text field (not HTML encoding needed since I send Text)
    public static string SafeText(string s) => RemoveControlChars(NormalizeWhitespace(s));
}

