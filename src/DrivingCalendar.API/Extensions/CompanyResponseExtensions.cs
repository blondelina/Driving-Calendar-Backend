using DrivingCalendar.API.Models;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.API.Extensions
{
    internal static class CompanyResponseExtensions
    {
        public static CompanyResponse ToResponse(this Company company)
        {
            return new CompanyResponse
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                Email = company.Email
            };
        }
    }
}
