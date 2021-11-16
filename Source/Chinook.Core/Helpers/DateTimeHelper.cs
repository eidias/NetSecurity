using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggingAndTesting.Core.Helpers
{
    public class DateTimeHelper
    {
        public const string Utc = "UTC";
        public const string GmtStandardTime = "GMT Standard Time";
        public const string GreenwichStandardTime = "Greenwich Standard Time";
        public const string WesternEuropeStandardTime = "W. Europe Standard Time";
        public const string CentralEuropeStandardTime = "Central Europe Standard Time";
        public const string CentralEuropeanStandardTime = "Central European Standard Time";
        public const string RomanceStandardTime = "Romance Standard Time";
        public const string EasternEuropeStandardTime = "E. Europe Standard Time";

        //Wouldn't it be better to create the TimeZoneInfo directly?
        public readonly TimeZoneInfo GmtStandardTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

        public bool IsDateBetween(DateTime left, DateTime right)
        {
            throw new NotImplementedException();
        }
    }
}
