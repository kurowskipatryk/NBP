using Models;

namespace NBP.Data
{
    public interface INBPRepository
    {
        Task<NBPTable> GetNBPTable(string type);
        Task SetNBPTable(NBPTable table);
    }
}
