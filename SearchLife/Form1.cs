using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SearchLife
{
    public partial class Form1 : Form
    {
        private List<string> _loadedWords;
        private Dictionary<string, string> _loadedWordsDictionary;
        private Dictionary<string, string> _createdDictionary = new Dictionary<string, string>();
        private int _maxLength = 0;
        private bool _dictionaryCreate = true;

        public Form1()
        {
            InitializeComponent();
            _loadedWords = LoadJson<List<string>>("words.json");
            _loadedWordsDictionary = LoadJson<Dictionary<string, string>>("words2.json");
            _loadedWords = _loadedWords.GetRange(0, 56969);
            FindLongestWord(_loadedWords);


            //_loadedWords.ForEach(x => ala.Add(x, x));

            /*for (char c = 'a'; c <= 'z'; c++)
            {
                kot.Add(c.ToString(), _loadedWords.First(a => a.StartsWith(c.ToString())));
            }*/
        }

        private void FindLongestWord(List<string> words)
        {
            foreach (string word in words)
            {
                if (word.Length > _maxLength)
                    _maxLength = word.Length;
            }
        }

        private void MakeDictionary(object word)
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                var looked = _loadedWords.FirstOrDefault(a => a.StartsWith((word as string) + c));
                if (looked != null && looked != String.Empty)
                {
                    _createdDictionary.Add((word as string) + c, looked);
                    _createdDictionary.TryGetValue((word as string) + c, out string name);
                    if (name.Length >= ((word as string) + c).Length)
                        if ((word as string).Length <= _maxLength)
                            ThreadPool.QueueUserWorkItem(MakeDictionary, (word as string) + c);
                }
            }
        }

        private void textBox1_TextChangedList(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            textBox2.Text = _loadedWords.Find(x => x.StartsWith(textBox1.Text));
            sw.Stop();
            label1.Text = $"{sw.ElapsedMilliseconds} ms";
        }

        private void textBox1_TextChangedDictionary(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            _loadedWordsDictionary.TryGetValue(textBox1.Text, out var looked);
            if (looked != null && looked != string.Empty)
                textBox3.Text = looked;
            else
                textBox3.Text = textBox1.Text;

            sw.Stop();
            label2.Text = $"{sw.ElapsedMilliseconds} ms";
        }

        T LoadJson<T>(string name)
        {
            using (StreamReader r = new StreamReader(name))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        void SaveJson(Dictionary<string, string> dictionary, string name)
        {
            using (StreamWriter r = new StreamWriter(name))
            {
                try
                {
                    var jsonString = JsonConvert.SerializeObject(dictionary);
                    r.Write(jsonString);
                }
                catch
                { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_dictionaryCreate)
            {
                MakeDictionary(String.Empty);
                button1.Text = "Save Dictionary";
                _dictionaryCreate = false;
            }
            else
            {
                SaveJson(_createdDictionary, "words3.json");
                button1.Text = "Make Dictionary";
                _dictionaryCreate = true;
                _createdDictionary = new Dictionary<string, string>();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
