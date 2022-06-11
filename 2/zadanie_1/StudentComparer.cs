using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    public class StudentComparer: IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (x.fname == y.fname && (x.lname == y.lname) && (x.indexNumber == y.indexNumber) ){
                x.studies.Add(y.studies.First());
                return true;
            }
            
            return false;   
        }
        public int GetHashCode(Student obj)
        {
            return obj.indexNumber.GetHashCode();
        }
    }
}
