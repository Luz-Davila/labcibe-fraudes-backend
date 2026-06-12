using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.WebAPI.Services
{
    public interface IFraudService
    {
        Task<IEnumerable<Fraud>> Get(int[] ids);
        Task<Fraud> Add(Fraud fraud);
    }

    public class FraudService : IFraudService
    {
        private readonly LibraryContext _libraryContext;

        public FraudService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Fraud>> Get(int[] ids)
        {
            var frauds = _libraryContext.Frauds.AsQueryable();

            if (ids != null && ids.Any())
                frauds = frauds.Where(x => ids.Contains(x.Id));

            return await frauds
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Fraud> Add(Fraud fraud)
        {
            fraud.CreatedAt = DateTime.UtcNow;

            await _libraryContext.Frauds.AddAsync(fraud);
            await _libraryContext.SaveChangesAsync();

            return fraud;
        }
    }
}
