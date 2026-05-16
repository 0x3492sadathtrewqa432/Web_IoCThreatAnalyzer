using System.Text.RegularExpressions;

namespace IoCThreatAnalyzer.Parsers;

public class IocParser
{
    private const string IpPattern =
        @"\b(?:(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.){3}(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\b";

    private const string UrlPattern =
        @"https?:\/\/(?:www\.)?[a-zA-Z0-9\-._~:/?#\[\]@!$&'()*+,;=%]+";

    private const string EmailPattern =
        @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

    private const string DomainPattern =
        @"\b(?:[a-zA-Z0-9-]+\.)+(?:com|net|org|io|ru|cc|xyz|biz|info|co|tv|me|pro|gov|edu)\b";

    private const string Md5Pattern =
        @"\b[a-fA-F0-9]{32}\b";

    private const string Sha1Pattern =
        @"\b[a-fA-F0-9]{40}\b";

    private const string Sha256Pattern =
        @"\b[a-fA-F0-9]{64}\b";

    public List<IocItem> Parse(string content)
    {
        var result = new List<IocItem>();

        result.AddRange(ParseRegex(content, IpPattern, "IP"));

        result.AddRange(ParseRegex(content, UrlPattern, "URL"));

        result.AddRange(ParseRegex(content, EmailPattern, "Email"));

        result.AddRange(ParseRegex(content, DomainPattern, "Domain"));

        result.AddRange(ParseRegex(content, Md5Pattern, "MD5"));

        result.AddRange(ParseRegex(content, Sha1Pattern, "SHA1"));

        result.AddRange(ParseRegex(content, Sha256Pattern, "SHA256"));

        return result
            .DistinctBy(x => $"{x.Type}:{x.Value}")
            .ToList();
    }

    private List<IocItem> ParseRegex(
        string content,
        string pattern,
        string type)
    {
        return Regex.Matches(content, pattern)
            .Select(match => match.Value.Trim().ToLower())
            .Where(IsValidIndicator)
            .Select(value => new IocItem
            {
                Type = type,
                Value = value
            })
            .ToList();
    }

    private bool IsValidIndicator(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        string[] invalidFragments =
        [
            ".js",
            ".css",
            ".png",
            ".jpg",
            ".jpeg",
            ".svg",
            ".woff",
            ".map",
            ".json",

            ".length",
            ".split",
            ".indexof",
            ".includes",
            ".replace",
            ".substr",
            ".pathname",
            ".hostname",
            ".protocol",
            ".cookie",
            ".queryselector",
            ".add",
            ".remove",
            ".parse",
            ".stringify",
            ".now",
            ".error",
            ".debug",
            ".send",
            ".open",
            ".response",
            ".status",
            ".push",
            ".splice"
        ];

        bool containsInvalidFragment =
            invalidFragments.Any(value.Contains);

        if (containsInvalidFragment)
        {
            return false;
        }

        if (value.StartsWith("window."))
        {
            return false;
        }

        if (value.StartsWith("document."))
        {
            return false;
        }

        if (value.StartsWith("console."))
        {
            return false;
        }

        if (value.StartsWith("object."))
        {
            return false;
        }

        if (value.StartsWith("json."))
        {
            return false;
        }

        if (value.StartsWith("math."))
        {
            return false;
        }

        if (value.Length > 120)
        {
            return false;
        }

        return true;
    }
}

public class IocItem
{
    public string Type { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}