using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace LaboratornayaRabota5
{
    internal class Program
    {
        static void Main()
        {

            string[] input = File.ReadAllLines(@"1.txt");
            for (int i = 0; i < input.Length; ++i)
            {
                Console.Write(input[i]);
                if (new Regex(@"(^а$)|(аааааа)|(а аа а)").IsMatch(input[i]))
                    Console.Write(" - Строка подходит");
                Console.WriteLine(String.Empty);
            }

            Console.WriteLine("\nЗадание 2");
            Console.WriteLine("Введи не менее 5 алфавитно-цифровых символов:");
            string str = Console.ReadLine();                                
            Console.WriteLine(new Regex(@"[0-9a-zA-Z]{5,}").IsMatch(str)); 
            
            Console.WriteLine("\nЗадание 3");
            input = File.ReadAllLines(@"3.txt");
            foreach (string line in input)
            {
                Console.Write(line + " - ");
                Console.WriteLine(new Regex(@"^[a-zA-Z]\w{3,20}@[a-z]{2,10}\.[a-z]{2,5}").IsMatch(line));
            }
            
            Console.WriteLine("\nЗадание 4");
            //Console.OutputEncoding = Encoding.UTF8; 
            input = File.ReadAllLines(@"4.txt");
            var patternFor4Exs = new Regex(@"(?<city>[А-Я][а-я]+([-][А-Я][а-я]+)*)[:]\s+(?<shirota>(широта\s+)?[0-9]{1,2}(\.[0-9]+)?,?)\s(?<dolgota>(долгота\s)?[0-9]{1,2}(\.[0-9]+)?)");
            var city = new Regex(@"[А-Я][а-я]+([-][А-Я][а-я]+)?");
            var digit = new Regex(@"[0-9]{1,2}(\.[0-9]+)?");
            foreach (string line in input)
            {
                if (patternFor4Exs.IsMatch(line))
                    Console.WriteLine(patternFor4Exs.Match(line).Groups["city"] + " Ш: " +
                        patternFor4Exs.Match(line).Groups["shirota"] + " Д: " +
                        patternFor4Exs.Match(line).Groups["dolgota"]);
            }
            Console.WriteLine("\nДополнительное задание");
            string price = "Добро пожаловать в наш магазин, вот наши цены: 1 кг. яблоки - 90 руб., 2 кг. апельсины - 130 руб. Также в ассортименте орехи в следующей фасовке: 0.5 кг. миндаль - 5000 руб";
            Regex regex = new Regex(@"((?:\d+)?[.]?\d+)?\sкг[.]\s(\w+)\s-\s(\d+)\sруб[.]?");
            var products = regex.Matches(price);
            foreach (Match product in products)
            {
                foreach (Match Products in regex.Matches(product.Value))
                {
                    var SplitProduct = Products.Groups;
                    Console.WriteLine("цена 1. " + SplitProduct[2] + " " + Convert.ToDouble(SplitProduct[3].Value) / Convert.ToDouble(SplitProduct[1].Value.Replace('.', ',')) + " руб/кг");


                }

            }
            Console.WriteLine("\nЗадание 5");
            input = File.ReadAllLines(@"Лабораторная работа 5 - testDatsa.xml");
            Console.WriteLine("Пункт а");
            foreach (string line in input)
            {
                if (patternFor4Exs.IsMatch(line))
                {
                    Console.WriteLine(city.Match(line) + " Ш: " + digit.Match(line) + " Д: " + digit.Match(line));
                }
            }
            Console.WriteLine("Пункт b,c,d");
            Regex nodeSpace = new Regex(@"\s+<node1>");
            Regex openTag = new Regex(@"<node\d>");
            Regex closeTag = new Regex(@"</node\d>");
            string buff;
            string tag1;
            string tag2;
            foreach (string line in input)
            {
                buff = Regex.Replace(line, @"^\d+.", String.Empty);
                if (nodeSpace.IsMatch(buff))
                    buff = Regex.Replace(buff, @"\s+<node1>", "..<node1>");
                tag1 = openTag.Match(line).Value;
                tag2 = closeTag.Match(line).Value;
                if (tag1 != tag2)
                    buff = Regex.Replace(buff, @"</node\d>", tag1);
                Console.WriteLine(buff);
            }
        }
    }
}