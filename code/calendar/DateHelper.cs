using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    public class DateHelper
    {
        public static List<DateEntity> GetDates(short startYear, int yearCount)
        {
            var res = new List<DateEntity>();
            var startDate = new DateTime(startYear, 1, 1);
            var endDate = startDate.AddYears(yearCount);
            var d = startDate;
            var id = 1;
            while (d < endDate)
            {
                res.Add(new DateEntity(d) { Id = id });
                d.AddDays(1);
                id++;
            }
            return res;
        }

        public static string GetSqlScript(short startYear, int yearCount)
        {
            var sb = new StringBuilder();
            foreach (var i in GetDates(startYear, yearCount))
            {
                sb.AppendLine(i.ToSql());
            }
            return sb.ToString();
        }

        public static string GetCsv(short startYear, int yearCount)
        {
            var sb = new StringBuilder();
            sb.AppendLine(DateEntity.ToCsvHeader());
            foreach (var i in GetDates(startYear, yearCount))
            {
                sb.AppendLine(i.ToCsvValue());
            }
            return sb.ToString();
        }
    }
}
