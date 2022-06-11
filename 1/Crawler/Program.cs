using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0) throw new ArgumentNullException();

            string websiteUrl = args[0];
            if(!Uri.IsWellFormedUriString(websiteUrl, UriKind.Absolute)) throw new ArgumentException();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try { response = await httpClient.GetAsync(websiteUrl); }
            catch (Exception) { Console.WriteLine("An error had occured when loading the website."); return ; }
            finally { httpClient.Dispose(); }

            string content = await response.Content.ReadAsStringAsync();
			
            Regex regex = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
            MatchCollection matchCollection = regex.Matches(content);

            HashSet<string> matches = new HashSet<string>();
            foreach (var match in matchCollection)
            {
                matches.Add(match.ToString());
            }
            if(matches.Count == 0) Console.WriteLine("No mail addresses had been found.");
            foreach (var match in matches)
            {
                Console.WriteLine(match);
            }
        }
    }
}
