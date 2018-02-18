using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Database
{
    public class MongoDatabase : IDatabase
    {
        public bool Save(object value)
        {
            int test = (int)value;
            Console.WriteLine($"Saved to datbase @@$$, {test}");
            return true;
        }
    }
}
