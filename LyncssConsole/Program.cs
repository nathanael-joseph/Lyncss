using System;
using System.Text.RegularExpressions;

namespace LyncssConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Lyncss!");

            Regex rgx = new Regex("^0{3}");
            bool res;
            res = rgx.IsMatch(" 000someother stuff");
            System.Console.WriteLine(res);
            
        }
    }
}
