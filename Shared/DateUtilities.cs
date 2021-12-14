namespace Shared;

public class DateUtilities
{
    private readonly DateTime CurrentDate;
    public DateUtilities()
    {
        CurrentDate = DateTime.Today;
    }
    public DateUtilities(DateTime dateTime)
    {
        CurrentDate = dateTime;
    }

    // Future enhancement get dates of holidays from https://www.gov.uk/bank-holidays.json either as a 
    // download or as it will not change that often as a json file that can be downloaded and stored locally.
    private readonly List<DateTime> Holidays = new();
    public void AddHoliday(DateTime dateTime)
    {
        Holidays.Add(dateTime);
    }

    public bool IsWorkingDaysInFutureValid(DateTime dateRequired, int futureDays)
    {
        DateTime workDate = CurrentDate;

        while (futureDays > 0) 
        { 
            if (workDate.DayOfWeek != DayOfWeek.Saturday && workDate.DayOfWeek != DayOfWeek.Sunday && !Holidays.Contains(workDate))
            {
                futureDays -= 1;
            }

            workDate = workDate.AddDays(1);
        }

        return dateRequired.CompareTo(workDate) >= 0;
    }
}
