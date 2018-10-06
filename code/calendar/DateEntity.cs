using System;
using System.Collections.Generic;
using System.Globalization;
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
    }
}
