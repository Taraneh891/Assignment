using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment
{
    public class WordLogic
    {
        private readonly PorterStemmer PorterStemmer = new PorterStemmer();
        //private List<RootWord> rootWords = new List<RootWord>();

        public async Task<int> ProcessLine(string line, string[] stopWords, List<RootWord> rootWords)
        {
            var lineWords = GetWordListFromLine(line);
            var wordsList = RemoveStopWords(RemoveNonAlphaChars(lineWords), stopWords);
            UpdateRootList(wordsList, rootWords);
            return rootWords.Count;
        }

        public List<string> GetWordListFromLine(string line)
        {
            return line.ToLower().Split(' ').ToList();
        }

        public List<string> RemoveStopWords(List<string> lineWords, string[] stopWords)
        {
            return lineWords.Except(stopWords).ToList();
        }

        public List<string> RemoveNonAlphaChars(List<string> wordList)
        {
            return wordList.Select(x => Regex.Replace(x, "[^a-zA-Z]", "")).ToList();
        }

        public void UpdateRootList(List<string> wordList, List<RootWord> rootWords)
        {
            foreach (var word in wordList)
            {
                if (word == string.Empty) continue;
                var root = PorterStemmer.StemWord(word);
                if (!rootWords.Any(x => x.Word == root))
                {
                    rootWords.Add(new RootWord
                    {
                        Word = root,
                        Count = 1
                    });
                }
                else
                {
                    rootWords.FirstOrDefault(x => x.Word == root).Count++;
                }
            }
        }


    }
}
