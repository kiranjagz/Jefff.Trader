using Jefff.Solid.Customer;
using Jefff.Solid.Database;
using Jefff.Solid.Fruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid
{
    class Program
    {
        private static List<ICustomer> _customers = new List<ICustomer>();
        private static List<Fruit.Fruit> _fruit = new List<Fruit.Fruit>();

        static void Main(string[] args)
        {
            var time = TimeSpan.FromHours(0.02);
            var time2 = TimeSpan.FromHours(0.5);
            var add = DateTime.Now.Add(time);
            var add2 = DateTime.Now.Add(time2);

            _fruit.Add(new Apple());
            _fruit.Add(new Grape());

            _customers.Add(new GoldCustomer(new SqlDatabase()));
            _customers.Add(new PrivateCustomer(new MongoDatabase()));

            _customers.ForEach(x =>
            {
                var discount = x.GetDiscount();
                var type = x.GetType();

                Console.WriteLine($"{type} has a discount of: {discount}");
            });

            _fruit.ForEach(x =>
            {
                var whatIsType = x.GetType();
                var concat = ($"{whatIsType}, is off colour, {x.GetColour()},,, and makes a noise of lol, {x.GetNoise()}");
                Console.WriteLine(concat);
            });

            Console.Read();
        }
    }
}
