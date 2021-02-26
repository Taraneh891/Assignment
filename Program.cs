using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        private static string[] stopWords;
        private static List<RootWord> rootWords = new List<RootWord>();
        private static readonly WordLogic wordLogic = new WordLogic();
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Write("Accepts only 2 arguments!");
                return;
            }
            SetStopWords(args[0]);
            ProcessDocument(args[1]);
            PrintMostCommonRoots();
        }

        public static void ProcessDocument(string path)
        {
            // TODO: verify the path
            var lines = FileReader.ReadFile(path);
            //Console.WriteLine(String.Join("\n", lines));

                     
            var tasks = new Task[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                tasks[i] = wordLogic.ProcessLine(lines[i], stopWords, rootWords);
            }

            Task.WaitAll(tasks);
        }

        public static void PrintMostCommonRoots()
        {
            // sort and take the first 20
            var maxCounts = rootWords.OrderByDescending(r => r.Count).Take(20);

            foreach(var root in maxCounts)
            {
                Console.WriteLine($"{root.Word} - {root.Count}");
            }
        }

        public static void SetStopWords(string path)
        {
            // TODO: add file as a resource or config the file path
            stopWords = FileReader.ReadStopWordsFile(path);
            //Console.WriteLine(String.Join("|", stopWords));
        }
    }
}
