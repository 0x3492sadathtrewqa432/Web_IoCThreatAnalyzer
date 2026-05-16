using IoCThreatAnalyzer.Data;
using IoCThreatAnalyzer.Models;
using Microsoft.EntityFrameworkCore;

namespace IoCThreatAnalyzer.Repositories;

public class ScanRepository : IScanRepository
{
    private readonly AppDbContext _context;

    public ScanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ScanResult result)
    {
        _context.ScanResults.Add(result);

        await _context.SaveChangesAsync();
    }

    public async Task<List<ScanResult>> GetAllAsync()
    {
        return await _context.ScanResults
            .Include(x => x.Indicators)
            .ToListAsync();
    }
}