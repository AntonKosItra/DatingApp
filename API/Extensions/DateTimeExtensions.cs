namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob) //not the perfect way to calculate age, but it's good enough for this project
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year;
        
        if (dob > today.AddYears(-age)) age--;
        
        return age;
    }
}