using Jefff.Solid.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Customer
{
    public class PrivateCustomer : ICustomer
    {
        private IDatabase _database;
        public PrivateCustomer(IDatabase database)
        {
            _database = database;
        }

        public int GetDiscount()
        {
            int value = 12;
            _database.Save(12);
            return value; ;
        }
    }
}
