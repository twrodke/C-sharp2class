using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Collector_TWR
{
    public class UnitsEnumeration  // uses default constructor only
    {
        internal UnitsEnumeration cm;
        internal UnitsEnumeration inches;

        // This enumerated type defines the different measuring systems
        // that will be used in this solution/project.
        enum Units { cm, inches };
    }
}
