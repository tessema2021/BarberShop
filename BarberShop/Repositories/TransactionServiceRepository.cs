using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Repositories
{
    public class TransactionServiceRepository : BaseRepository
    {
        public TransactionServiceRepository(IConfiguration config) : base(config) { }



    }
}
