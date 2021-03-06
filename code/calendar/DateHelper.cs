﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    public class DateHelper
    {
        public static List<DateEntity> GetDates(string locale, short startYear, int yearCount)
        {
            var culture = new CultureInfo(locale);
            var res = new List<DateEntity>();
            var startDate = new DateTime(startYear, 1, 1);
            var endDate = startDate.AddYears(yearCount);
            var d = startDate;
            var id = 1;
            while (d < endDate)
            {
                res.Add(new DateEntity(d, culture) { Id = id });
                d = d.AddDays(1);
                id++;
            }
            return res;
        }

        public static string GetSqlScript(string locale, short startYear, int yearCount)
        {
            var sb = new StringBuilder();
            foreach (var i in GetDates(locale, startYear, yearCount))
            {
                sb.AppendLine(i.ToSql());
            }
            return sb.ToString();
        }

        public static string GetCsv(string locale, short startYear, int yearCount)
        {
            var sb = new StringBuilder();
            sb.AppendLine(DateEntity.ToCsvHeader());
            foreach (var i in GetDates(locale, startYear, yearCount))
            {
                sb.AppendLine(i.ToCsvValue());
            }
            return sb.ToString();
        }
    }
}
