namespace IoCThreatAnalyzer.Models;

public class ScanResult
{
    public int Id { get; set; }

    public string Url { get; set; } = string.Empty;

    public DateTime ScanDate { get; set; }

    public ICollection<IocIndicator> Indicators { get; set; }
        = new List<IocIndicator>();
}