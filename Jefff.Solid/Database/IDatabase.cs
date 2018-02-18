using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Database
{
    public interface IDatabase
    {
        bool Save(object value);
    }
}
