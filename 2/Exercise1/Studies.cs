using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace mp
{
    public class Studies
    {
        public string name;
        public int numberOfStudents;

        public Studies(string name)
        {
            this.name = name;
            this.numberOfStudents = 1;
        }

        public JObject toJson() { 
            return new JObject(
                new JProperty("name", this.name),
                new JProperty("numberOfStudents", this.numberOfStudents)

            );
        }
    }
}
