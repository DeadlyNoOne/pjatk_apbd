using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    public class StudiaComparer : IEqualityComparer<Studia>
    {
        public bool Equals(Studia x, Studia y)
        {
            if (x.name == y.name){
                x.numberOfStudents++;
                return true;
            }
            
            return false;   
        }
        public int GetHashCode(Studia obj)
        {
            return obj.name.GetHashCode();
        }
    }
}
