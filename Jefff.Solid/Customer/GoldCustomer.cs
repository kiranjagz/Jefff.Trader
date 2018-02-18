using Jefff.Solid.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Customer
{
    public class GoldCustomer : ICustomer
    {
        private IDatabase _database;
        public GoldCustomer(IDatabase database)
        {
            _database = database;
        }

        public int GetDiscount()
        {
            int value = 2;
            _database.Save(2);
            return value; ;
        }
    }
}
