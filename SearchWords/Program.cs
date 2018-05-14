using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var _loadedWords = LoadJson();
            int? add = null;
            string looked = String.Empty;
            while (!Equals(add, ConsoleKey.Enter))
            {
                add = Console.In.Read();
                looked += add;
            }
            Console.Out.Write(_loadedWords.Find(x => x.StartsWith(looked)));
            Console.In.ReadLine();


        }
        public static List<string> LoadJson()
        {
            using (StreamReader r = new StreamReader("words.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<string>>(json);
            }
        }
    }
}
