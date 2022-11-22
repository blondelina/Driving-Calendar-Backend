using System.Runtime.Serialization;

namespace DrivingCalendar.Business.Constants
{
    public enum DrivingLessonStatus
    {
        [EnumMember(Value = "Confirmed")]
        Confirmed,

        [EnumMember(Value = "Rejected")]
        Rejected,

        [EnumMember(Value = "Pending")]
        Pending
    }
}
