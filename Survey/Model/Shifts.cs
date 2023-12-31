using Model.Common;

namespace Model
{
    public class Shifts : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<UserShifts>? UserShifts { get; set; }

    }
}
