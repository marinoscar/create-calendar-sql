using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    public class ArgsHelper
    {
        public ArgsHelper(IEnumerable<string> args)
        {
            Args = new List<string>(args);
        }

        protected List<string> Args { get; private set; }
        public static string ShowHelp()
        {
            return @"
Use the following commands
/start      {num}  Year to start for example 2018
/years      {num}  Count of years to increase, for example 2
/format     {text} csv or sql
/output     {text} the location of the file to store the values
/local      {text} the locale to use i.e en-US or es-ES
";
        }

        public string GetArg(string sw)
        {
            var idx = Args.IndexOf(sw);
            if (idx < 0) throw new ArgumentException("Invalid switch");
            var valIdx = idx + 1;
            if (valIdx >= Args.Count) throw new ArgumentException("Value is missing");
            var val = Args[valIdx];
            if (val.Trim().StartsWith("/")) throw new ArgumentException("Value is missing");
            return val;
        }


        public short Start
        {
            get { return ToNum(GetArg("/start")); }
        }

        public short Years
        {
            get { return ToNum(GetArg("/years")); }
        }

        public string Format
        {
            get { return GetArg("/format").Trim().ToLowerInvariant(); }
        }

        public string Output
        {
            get { return GetArg("/output").Trim(); }
        }

        public string Locale
        {
            get { return GetArg("/locale").Trim(); }
        }

        private short ToNum(string val)
        {
            if (!short.TryParse(val, out short res)) throw new ArgumentException("Invalid value");
            return res;
        }


    }
}
