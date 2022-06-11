using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    public class StudiesComparer : IEqualityComparer<Studies>
    {
        public bool Equals(Studies x, Studies y)
        {
            if (x.name == y.name){
                x.numberOfStudents++;
                return true;
            }
            
            return false;   
        }
        public int GetHashCode(Studies obj)
        {
            return obj.name.GetHashCode();
        }
    }
}
