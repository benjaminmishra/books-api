using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksMgmt.Domain.Tests
{
    public class DobFixture
    {
        public List<DateTime> dobs = new List<DateTime>()
        {
            new(2025,12,24),
            new(1989,01,05)
        };
    }
}
