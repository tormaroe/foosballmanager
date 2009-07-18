using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class UnixTimeParsing
    {
        [Test]
        public void Should_be_able_to_convert_DateTime_to_unix_time_and_back()
        {
            When_converting_a_known_date_to_unix_time();
            And_converting_it_back_to_a_datetime();
            dateFromUnixTime.should_equal(knownDate);
        }

        Action When_converting_a_known_date_to_unix_time = () =>
            unixTime = UnixTime.GetUnixTime(knownDate);

        Action And_converting_it_back_to_a_datetime = () =>
            dateFromUnixTime = UnixTime.GetDateFromUnixTime(unixTime);

        static DateTime knownDate = new DateTime(2009, 8, 18, 11, 34, 22, 10);
        static double unixTime;
        static DateTime dateFromUnixTime;
    }
}
