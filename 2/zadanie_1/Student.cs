using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace mp
{
    public class Student
    {
        public string indexNumber;
        public string fname;
        public string lname;
        public string birthdate;
        public string email;
        public string mothersName;
        public string fathersName;
        public List<KierunekStudia> studies;

        public Student(string[] data)
        {
            this.indexNumber = data[4];
            this.fname = data[0];
            this.lname = data[1];
            this.birthdate = data[5];
            this.email = data[6];
            this.mothersName = data[7];
            this.fathersName = data[8];
            this.studies = new List<KierunekStudia>();
            this.studies.Add(new KierunekStudia(data[2], data[3]));

        }

        public JObject toJson()
        {
            return new JObject(
                new JProperty("indexNumber", this.indexNumber),
                new JProperty("fname", this.fname),
                new JProperty("lname", this.lname),
                new JProperty("birthdate", this.birthdate),
                new JProperty("email", this.email),
                new JProperty("mothersName", this.mothersName),
                new JProperty("fathersName", this.fathersName),
                new JProperty("studies", studiesToJson())
            );


        }
        public JArray studiesToJson()
        {
            JArray jArray = new JArray();  
            for(int i = 0; i < this.studies.Count; i++)
                jArray.Add(this.studies[i].toJson());

            return jArray;
        }
    }
}
