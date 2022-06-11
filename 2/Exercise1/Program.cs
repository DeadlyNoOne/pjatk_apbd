using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


//args
//    "D:\School\APBD\2\data.csv" "D:\School\APBD\2" json

namespace mp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string logPath = @"log.txt";
            Loger loger = new Loger(logPath);
            try
            {
                if (args.Length < 3) throw new ArgumentException("Input data error. Requires: CSV file address, output address, data format");
                string sourcePath = args[0];
                string resultFormat = args[2];
                string resultPath = args[1] + @"\result." + resultFormat;

                if (!File.Exists(@sourcePath)) throw new FileNotFoundException("The given file does not exist: " + @sourcePath);
                if (!Directory.Exists(@args[1])) throw new ArgumentException("The given location is incorrect: " + @args[1]);

                var studentHashSet = new HashSet<Student>(new StudentComparer());
                var studiaHashSet = new HashSet<Studies>(new StudiesComparer());

                using (StreamReader stream = new StreamReader(@sourcePath))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] b = line.Split(',');
                        try
                        {
                            Student student = new Student(b);
                            studentHashSet.Add(student);

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                            loger.log("Student's data error: " + line);

                        }
                    }
                }

                foreach (Student s in studentHashSet)
                {
                    List<Subjects> kierunekStudias = s.studies;

                    kierunekStudias.ForEach(k =>
                    {
                        studiaHashSet.Add(new Studies(k.name));
                    });
                }

                JArray jArrayStudenci = new JArray();
                JArray jArrayStudia = new JArray();

                foreach (Student s in studentHashSet)
                {
                    jArrayStudenci.Add(s.toJson());
                }


                foreach (Studies s in studiaHashSet)
                {
                    jArrayStudia.Add(s.toJson());
                }




                JObject result = new JObject(
                    new JObject(
                        new JProperty("uczelnia", 
                            new JObject(
                                new JProperty("createdAt", DateTime.Today.ToString("dd.MM.yyyy")),
                                new JProperty("author", "Patryk Starus"),
                                new JProperty("studenci", jArrayStudenci),
                                new JProperty("activeStudies", jArrayStudia)
                            )
                        )
                    )
                );


                File.WriteAllText(@resultPath, result.ToString());

  
            }
            catch (Exception e)
            {
                Console.WriteLine("---------");
                Console.Write(e.ToString());
                loger.log("Student's data error: " + e.Message);
                throw;
            }

        }
    }
}


