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
            var endDate = new DateTime(startYear + yearCount, 12, 31);
            var d = startDate;
            while(d < endDate)
            {
                res.Add(new DateEntity(d));
                d.AddDays(1);
            }
            return res;
        }
    }
}
