using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public static class FileReader
    {
        public static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public static string[] ReadStopWordsFile(string path)
        {
            string[] words = File.ReadAllLines(path);
            for (var i = 0; i < words.Length; i++)
            {
                // removing extra whitespace and convert to lower for string comparison 
                words[i] = words[i].Trim().ToLower();
            }
            return words;
        }
    }
}
