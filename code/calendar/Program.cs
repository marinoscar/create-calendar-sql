using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ArgsHelper(args);
            if (args.Length <= 0)
            {
                Console.WriteLine(ArgsHelper.ShowHelp());
                return;
            }
            try
            {
                CheckFormat(options.Format);
                if (options.Format == "csv") SaveToFile(() => DateHelper.GetCsv(options.Locale, options.Start, options.Years), options.Output);
                else SaveToFile(() => DateHelper.GetSqlScript(options.Locale, options.Start, options.Years), options.Output);
                Console.WriteLine();
                Console.WriteLine("File created");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ArgsHelper.ShowHelp());
            }
        }
        static void CheckFormat(string format)
        {
            if (format != "csv" && format != "sql") throw new ArgumentException("/format value needs to be either csv or sql");
        }

        static void SaveToFile(Func<string> generate, string file)
        {
            File.WriteAllText(file, generate());
        }
    }


}
