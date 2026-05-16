namespace IoCThreatAnalyzer.Models;

public class IocIndicator
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public int ScanResultId { get; set; }

    public ScanResult? ScanResult { get; set; }
}