using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    public class DateEntity
    {
        public DateEntity(DateTime date, CultureInfo culture)
        {
            Culture = culture;
            Init(date);
        }

        public DateEntity(DateTime date) : this(date, CultureInfo.CurrentCulture)
        {

        }

        public DateEntity() : this(DateTime.Today)
        {

        }

        public CultureInfo Culture { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set }
        public short DayOfYear { get; set; }
        public string DayName { get; set; }
        public short WeekOfYear { get; set; }
        public string WeekName { get; set; }
        public short Month { get; set; }
        public string MonthName { get; set; }
        public short Year { get; set; }

        private void Init(DateTime date)
        {
            var cal = Culture.Calendar;
            Date = date.Date;
            DayName = Date.ToString("dddd", Culture);
            DayOfYear = Convert.ToInt16(date.DayOfYear);
            WeekOfYear = Convert.ToInt16(Culture.Calendar.GetWeekOfYear(Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday));
            WeekName = GetWeekName();
            Month = Convert.ToInt16(date.Month);
            MonthName = Date.ToString("mmmm", Culture);
            Year = Convert.ToInt16(date.Year);
        }

        private string GetWeekName()
        {
            var diff = (7 + (Date.DayOfWeek - DayOfWeek.Monday)) % 7;
            var week = Date.AddDays(-1 * diff).Date;
            return week.ToString("d", Culture);
        }

        public string ToSql()
        {
            return string.Format(Culture, "INSERT INTO Calendar (Id, Date, DayOfYear, DayName, WeekOfYear, WeekName, Month, MonthName, Year) VALUES ({0});",
                string.Join(",", GetValues().Select(i => ToSql(i))));
        }

        public static string ToCsvHeader()
        {
            return ToCsvHeader(CultureInfo.CurrentCulture);
        }

        public static string ToCsvHeader(CultureInfo cul)
        {
            return string.Format(cul, "Id{0}Date{0}DayOfYear{0}DayName{0}WeekOfYear{0}WeekName{0}Month{0}MonthName{0}Year", cul.TextInfo.ListSeparator);
        }

        public string ToCsvValue()
        {
            return string.Join(Culture.TextInfo.ListSeparator, GetValues());
        }

        private object[] GetValues()
        {
            return new object[] { Id, Date, DayOfYear, DayName, WeekOfYear, WeekName, Month, MonthName, Year };
        }

        private string ToSql(object o)
        {
            return o.GetType().IsValueType ? Convert.ToString(o, Culture) : string.Format(Culture, "'{0}'", o);
        }
    }
}
