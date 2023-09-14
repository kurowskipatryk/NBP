using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPApi
{
    public interface INBPClient
    {
        Task<NBPTable> GetCurrency(string type);
    }
}
