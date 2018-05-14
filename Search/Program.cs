using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Search
{
    public class Program
    {
        static void Main(string[] args)
        {
            void LoadJson()
            {
                using (StreamReader r = new StreamReader("words.json"))
                {
                    string json = r.ReadToEnd();
                    List<string> items = JsonConvert.DeserializeObject<List<string>>(json);
                }
            }
        }
    }
}