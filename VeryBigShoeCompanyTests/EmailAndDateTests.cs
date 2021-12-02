using Shared;
using System;
using Xunit;

namespace VeryBigShoeCompanyTests;

public class EmailAndDateTests
{
    [Theory]
    [InlineData(true, "a@b.com")]
    [InlineData(true, "a@b.co.uk")]
    [InlineData(true, "a@b.org")]
    [InlineData(false, "@b.com")]
    [InlineData(false, "a@com")]
    [InlineData(false, "a@b..com")]
    public void EmailAddressValidation(bool expected, string emailAddress)
    {
        Assert.Equal(expected, RegexUtilities.IsValidEmail(emailAddress));
    }

    [Theory]
    [InlineData(true, 2021, 11, 15)]
    [InlineData(false, 2021, 11, 12)]

    public void IsDateValidFrom2021_11_01(bool expected, int year, int month, int day)
    {
        DateTime testDate = new(year, month, day);
        DateUtilities dateUtilities = new(new DateTime(2021, 11, 1));
        Assert.Equal(expected, dateUtilities.IsWorkingDaysInFutureValid(testDate, 10));
    }

    [Theory]
    [InlineData(true, 2021, 11, 18)]
    [InlineData(false, 2021, 11, 17)]

    public void IsDateValidFrom2021_11_04(bool expected, int year, int month, int day)
    {
        DateTime testDate = new(year, month, day);
        DateUtilities dateUtilities = new(new DateTime(2021, 11, 4));
        Assert.Equal(expected, dateUtilities.IsWorkingDaysInFutureValid(testDate, 10));
    }

    [Theory]
    [InlineData(true, 2021, 12, 30)]
    [InlineData(false, 2021, 12, 29)]

    public void IsDateValidFrom2021_12_14(bool expected, int year, int month, int day)
    {
        DateTime testDate = new(year, month, day);
        DateUtilities dateUtilities = new(new DateTime(2021, 12, 14));
        dateUtilities.AddHoliday(new DateTime(2021, 12, 27));
        dateUtilities.AddHoliday(new DateTime(2021, 12, 28));
        Assert.Equal(expected, dateUtilities.IsWorkingDaysInFutureValid(testDate, 10));
    }
}
