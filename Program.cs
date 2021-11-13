using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing;

using System.Collections.Generic;

namespace text
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + "myText.txt";
            FileInfo file = new FileInfo(filepath);
            //FileInfo file = new FileInfo(@"C:\Users\Public\אפרת\תכנות\Hadasim\myText.txt");
            LinesCount(file);
            Console.WriteLine();
            Console.WriteLine("nuber of the words: " +WordsCount(file));
            Console.WriteLine();
            CountsUniqueWords(file);
            Console.WriteLine();
            SentenceLength(file);
            Console.WriteLine();
            DividingToCleanWords(File.ReadAllText(file.FullName));
            Console.WriteLine();
            SequenceNoK(file);
            Console.WriteLine();
            Console.WriteLine("the colors and their amount:");
            CountColors(file);
        }

        public static void LinesCount(FileInfo file)
        {
            int count = File.ReadAllLines(file.FullName).Length;
            Console.WriteLine("total lines: " + count);
        }
        public static int WordsCount(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            string[] words = DividingToCleanWords(text);
            return words.Length;
        }
        public static void CountsUniqueWords(FileInfo file)
        {           
            string text = File.ReadAllText(file.FullName);
            string[] fields = new string[WordsCount(file)];
            Regex reg_exp = new Regex("[^a-zA-Z0-9']");
            Dictionary<string, int> objdict = new Dictionary<string, int>();
            string[] words = DividingToCleanWords(text);
            string temp;
            foreach (string item in words)
            {
                temp = reg_exp.Replace(item, "");
                if (objdict.ContainsKey(temp))
                {
                    objdict[temp] = objdict[temp] + 1;
                }
                else
                {
                    objdict.Add(temp, 1);
                }
            }
           
            int count = 0;
            foreach (var temp2 in objdict)
            {
                
                if (temp2.Value == 1)
                {
                    //Console.WriteLine("{0}: {1}", temp.Key, temp.Value);
                    count++;
                }
               
            }
            Console.WriteLine("The number of unique words: " + count);
        }


        public static void SentenceLength(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            string temp = ".\n";
            string[] arr = text.Split(temp.ToCharArray());
            int maxLength = arr[0].Length, sumLength = arr[0].Length;
            for (int i = 1; i < arr.Length; i++)
            {
                sumLength += arr[i].Length;
                if (arr[i].Length > maxLength)
                    maxLength = arr[i].Length;
            }
            Console.WriteLine("the average length sentence: "+ sumLength/arr.Length);
            Console.WriteLine("the max length sentence: " + maxLength);
        }

        public static string[] DividingToCleanWords(string text)
        {
            int iCleanWords=0;
            Regex reg_exp = new Regex("[^a-zA-Z0-9']");
            string temp = " \n";
            string[] words = text.Split(temp.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);           
            string[] cleanWords = new string[words.Length];
            foreach (string item in words)
            {
                cleanWords[iCleanWords] = reg_exp.Replace(item, "");
                if (cleanWords[iCleanWords] != "")
                    iCleanWords++;
            }
            
            Array.Resize(ref cleanWords, iCleanWords);
            return cleanWords;
        }
        public static void SequenceNoK(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            string[] words = DividingToCleanWords(text);
            int low = 0, high=0;
            for (int i = 0, j = 0; j < words.Length; j++)
            {
                if (words[j].IndexOf('k') != -1)
                {
                    if(j-i>high-low)
                    {
                        low = i;
                        high = j;
                    }                    
                    i=j+1;
                }                                   
            }
            Console.WriteLine("the max length sentence without 'k'");
            for (int i = low; i < high; i++)
            {
                Console.Write(words[i]+" ");
            }
            Console.WriteLine();
        }

        public static void CountColors(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            string[] words = DividingToCleanWords(text);
            Dictionary<string, int> colorsAmount = new Dictionary<string, int>();
            string color;
            foreach (string item in words)
            {
                if (Color.FromName(item).IsKnownColor)
                {
                    color = item.ToLower();
                    if (colorsAmount.ContainsKey(color))
                        colorsAmount[color] = colorsAmount[color] + 1;
                    else
                        colorsAmount.Add(color, 1);
                }
                                                   
            }
            foreach (var item in colorsAmount)
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }
        }
    }
}
