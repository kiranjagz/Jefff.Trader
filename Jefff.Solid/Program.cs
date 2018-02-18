using Jefff.Solid.Customer;
using Jefff.Solid.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid
{
    class Program
    {
        private static List<ICustomer> customers = new List<ICustomer>();
        static void Main(string[] args)
        {

            customers.Add(new GoldCustomer(new SqlDatabase()));
            customers.Add(new PrivateCustomer(new MongoDatabase()));

            customers.ForEach(x =>
            {
                var discount = x.GetDiscount();
                var type = x.GetType();

                Console.WriteLine($"{type} has a discount of: {discount}");
            });

            Console.Read();
        }
    }
}
