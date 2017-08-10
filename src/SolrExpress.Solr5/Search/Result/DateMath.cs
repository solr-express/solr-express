// Copied from https://github.com/dalenewman/DateMath/blob/master/src/Shared/DateMath.cs
// Waiting https://github.com/dalenewman/DateMath/issues/3

#region license
// DateMath
// Date Math for .NET
// Copyright 2016 Dale Newman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System;
using System.Text.RegularExpressions;

namespace DaleNewman
{

    public static class DateMath
    {

        private const string AnchorDatePattern = @"^now|.{6,}\|\|";
        private const string OperatorPattern = @"[/+/-]{1}\d+[dhMmswy]{1}";
        private const string RoundingPattern = @"/[dhMmswy]{1}";

#if NS10
        private static readonly Regex AnchorDate = new Regex(AnchorDatePattern);
        private static readonly Regex Operator = new Regex(OperatorPattern);
        private static readonly Regex Rounding = new Regex(RoundingPattern);
#else
        private static readonly Regex AnchorDate = new Regex(AnchorDatePattern, RegexOptions.Compiled);
        private static readonly Regex Operator = new Regex(OperatorPattern, RegexOptions.Compiled);
        private static readonly Regex Rounding = new Regex(RoundingPattern, RegexOptions.Compiled);
#endif

        public static string Parse(string expression, string format)
        {
            string result;
            TryParse(expression, out result, format);
            return result;
        }

        public static DateTime Parse(string expression)
        {
            DateTime result;
            TryParse(expression, out result);
            return result;
        }

        public static bool TryParse(string expression, out string result, string format)
        {
            DateTime date;
            if (TryParse(expression, out date))
            {
                result = date.ToString(format);
                return true;
            }
            result = expression;
            return false;
        }

        public static bool TryParse(string expression, out DateTime result)
        {

            var matchAnchorDate = AnchorDate.Match(expression);
            if (matchAnchorDate.Success)
            {
                string operators;
                DateTime date;

                var value = matchAnchorDate.Value.ToLower();

                if (value == "now")
                {
                    date = DateTime.UtcNow;
                    operators = expression.Substring(3);
                }
                else
                {
                    value = value.TrimEnd(new[] { '|' });
                    if (!DateTime.TryParse(value, out date))
                    {
                        result = DateTime.MinValue;
                        return false;
                    }
                    operators = expression.Substring(matchAnchorDate.Value.Length);
                }

                result = Apply(date, operators);
                return true;
            }

            result = DateTime.MinValue;
            return false;
        }

        private static TimeSpan UnitToInterval(DateTime input, char unit)
        {
            var daysInYear = DateTime.IsLeapYear(input.Year) && input < new DateTime(input.Year, 2, 29) ? 366 : 365;
            switch (unit)
            {
                case 'y': // year
                    return new TimeSpan(daysInYear, 0, 0, 0);
                case 'M': // month
                    return new TimeSpan(daysInYear / 12, 0, 0, 0);
                case 'w': // week
                    return new TimeSpan(7, 0, 0, 0);
                case 'd': // day
                    return new TimeSpan(1, 0, 0, 0);
                case 'h':// hour
                    return new TimeSpan(0, 1, 0, 0);
                case 'm': // minute
                    return new TimeSpan(0, 0, 1, 0);
                default: // second
                    return new TimeSpan(0, 0, 0, 1);
            }
        }

        private static DateTime ApplyOperator(DateTime input, string @operator)
        {

            var numberPart = @operator.Substring(1, @operator.Length - 2);
            var number = int.Parse(numberPart);
            var add = @operator[0] == '+';
            var unit = @operator[@operator.Length - 1];
            var interval = UnitToInterval(input, unit);

            if (number > 1)
            {
                interval = new TimeSpan(number * interval.Ticks);
            }

            return add ? input.Add(interval) : input.Subtract(interval);

        }

        private static DateTime Floor(DateTime input, TimeSpan interval)
        {
            return input.AddTicks(-(input.Ticks % interval.Ticks));
        }


        private static DateTime ApplyRounding(DateTime input, char unit)
        {
            switch (unit)
            {
                case 'y':
                    return new DateTime(input.Year, 1, 1);
                case 'M':
                    return new DateTime(input.Year, input.Month, 1);
                default:
                    return Floor(input, UnitToInterval(input, unit));
            }

        }

        public static DateTime Apply(DateTime input, string math)
        {
            foreach (Match match in Operator.Matches(math))
            {
                input = ApplyOperator(input, match.Value);
            }

            var matchRounder = Rounding.Match(math);
            if (matchRounder.Success)
            {
                input = ApplyRounding(input, matchRounder.Value[1]);
            }
            return input;
        }
    }
}