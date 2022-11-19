using System.Runtime.Serialization;

namespace DrivingCalendar.Business.Enums
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
