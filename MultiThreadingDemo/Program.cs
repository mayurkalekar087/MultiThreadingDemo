using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MultiThreadingDemo

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome TO Multi Threading Program!");

            string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");

            #region ParallelTasks
            Parallel.Invoke(() =>
            {
                Console.WriteLine("Begin first task...");
                GetLongestWord(words);
            },// close 1st action
            () =>
            {
                Console.WriteLine("Begin second task...");
                GetMostCommonWords(words);
            },
            () =>
            {
                Console.WriteLine("Begin third task...");
                GetCountForward(words, "sleep");
            }// close third action
            );// close parallel.invoke
            #endregion
            Console.ReadKey();
        }
        private static void GetCountForward(string[] words, string task)
        {
            var findWord = from word in words
                           where word.ToUpper().Contains(task.ToUpper())
                           select word;
            Console.WriteLine($@"Task 3 --The word""{task}""occurs{findWord.Count()} times.");
        }
        private static void GetMostCommonWords(string[] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into q
                                 orderby q.Count() descending
                                 select q.Key;
            var commonWord = frequencyOrder.Take(10);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Task 2 -- The most common words are:");
            foreach (var V in commonWord)
            {
                sb.AppendLine("  " + V);
            }
            Console.WriteLine(sb.ToString());
        }
        private static string GetLongestWord(string[] words)
        {
            string longestWord = (from w in words
                                  orderby w.Length descending
                                  select w).First();

            Console.WriteLine($"Task 1 -- The longest word is {longestWord}.");
            return longestWord;
        }
        static string[] CreateWordArray(string url)
            {
                Console.WriteLine($"Retrieving from {url}");
                //Download a web page the easy way
                string blog = new WebClient().DownloadString(url);
                //Seperate string into Array Of words,removing Some Common punctuations
                return blog.Split(
                    new char[] { ' ', '\u000A', '.', ',', '-', '_', '/' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }

