using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Builder
{
    internal class Team
    {
        public IPerson Boss { get; }
        public IPerson[] Member { get; }
        public bool WorksAtCustomerSite { get; }
        public bool AllowHomeOffice { get; }

        public Team(IPerson boss, IPerson[] member, bool worksAtCustomerSite, bool allowHomeOffice)
        {
            Boss = boss;
            Member = member;
            WorksAtCustomerSite = worksAtCustomerSite;
            AllowHomeOffice = allowHomeOffice;
        }
    }
}
