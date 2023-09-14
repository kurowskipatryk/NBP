using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace NBP.Data
{
    public class NBPRepository : INBPRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public NBPRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<NBPTable> GetNBPTable(string type)
        {
            var result = await _appDbContext.Tables.FirstOrDefaultAsync(x=>x.Table == type);
            return result;
        }

        public async Task SetNBPTable(NBPTable table)
        {
            var result = await _appDbContext.Tables.FirstOrDefaultAsync(x => x.Table == table.Table);

            if (result != null)
            {
                _appDbContext.Tables.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }

            await _appDbContext.Tables.AddAsync(table);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
