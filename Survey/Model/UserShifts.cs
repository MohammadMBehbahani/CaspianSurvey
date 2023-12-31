namespace Model
{
    public class UserShifts
    {
        public virtual Guid UserId { get; set; }
        public required virtual User User { get; set; }
        public virtual Guid ShiftsId { get; set; }
        public required virtual Shifts Shifts { get; set; }

    }
}
