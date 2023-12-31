namespace Model
{
    public class UserRollCall
    {
        public virtual Guid UserId { get; set; }
        public required virtual User User { get; set; }
        public virtual Guid RollCallId { get; set; }
        public required virtual RollCall RollCall { get; set; }
    }
}
