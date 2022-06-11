using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    internal class Loger
    {
        string path;
        public Loger(string path)
        {
            this.path = path;   
        }


        public void log(string message)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                DateTime now = DateTime.Now;
                writer.WriteLine(now.ToString() + " : " + "Błąd danych studenta: " + message);
            }
        }
    }
}
