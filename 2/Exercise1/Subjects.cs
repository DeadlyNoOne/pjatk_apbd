using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    public class Subjects
    {
        public string name;
        public string mode;

        public Subjects(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }

        internal JToken toJson()
        {
            return new JObject(
                new JProperty("name", name),    
                new JProperty("mode", mode)
                );
        }
    }
}
