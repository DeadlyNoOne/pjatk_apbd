using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


//args
//    "C:\PRV\PJWSTK\APBD\cwiczenia2_mp-s21461\dane.csv" "C:\PRV\PJWSTK\APBD\cwiczenia2_mp-s21461" json

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
                if (args.Length < 3) throw new ArgumentException("Błąd danych wejściowych. Oczekuje: Adres pliku CSV, Adres ścieżki docelowej, Format danych");
                string sourcePath = args[0];
                string resultFormat = args[2];
                string resultPath = args[1] + @"\result." + resultFormat;

                if (!File.Exists(@sourcePath)) throw new FileNotFoundException("Plik nazwa nie istnieje: " + @sourcePath);
                if (!Directory.Exists(@args[1])) throw new ArgumentException("Podana ścieżka jest niepoprawna: " + @args[1]);

                var studentHashSet = new HashSet<Student>(new StudentComparer());
                var studiaHashSet = new HashSet<Studia>(new StudiaComparer());

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
                            loger.log("Błąd danych studenta: " + line);

                        }
                    }
                }

                foreach (Student s in studentHashSet)
                {
                    List<KierunekStudia> kierunekStudias = s.studies;

                    kierunekStudias.ForEach(k =>
                    {
                        studiaHashSet.Add(new Studia(k.name));
                    });
                }

                JArray jArrayStudenci = new JArray();
                JArray jArrayStudia = new JArray();

                foreach (Student s in studentHashSet)
                {
                    jArrayStudenci.Add(s.toJson());
                }


                foreach (Studia s in studiaHashSet)
                {
                    jArrayStudia.Add(s.toJson());
                }




                JObject result = new JObject(
                    new JObject(
                        new JProperty("uczelnia", 
                            new JObject(
                                new JProperty("createdAt", DateTime.Today.ToString("dd.MM.yyyy")),
                                new JProperty("author", "Patryk Kępisty"),
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
                loger.log("Błąd danych studenta: " + e.Message);
                throw;
            }

        }
    }
}


