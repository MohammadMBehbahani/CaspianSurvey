using Model.Common;

namespace Model
{
    public class RollCall : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<UserRollCall>? UserRollCall { get; set; }

        public TimeSpan? TotalTimeOfwork
        {
            get
            {
                return (StartTime - EndTime) - (BreakStartTime?.Add(BreakEndTime!.Value));
            }
        }
    }
}
