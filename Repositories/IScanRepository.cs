using IoCThreatAnalyzer.Models;

namespace IoCThreatAnalyzer.Repositories;

public interface IScanRepository
{
    Task AddAsync(ScanResult result);

    Task<List<ScanResult>> GetAllAsync();
}