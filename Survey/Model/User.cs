using Model.Common;

namespace Model
{
    public class User: BaseEntity
    {
        public required string FName { get; set; }
        public required string SName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public required string HashPass { get; set; }
        public Guid PersonalCode { get; set; } = new Guid();
        public ICollection<Comments>? Comments { get; set; }
        public ICollection<UserShifts>? UserShifts { get; set; }
        public ICollection<UserRollCall>? UserRollCall { get; set; }

    }
}
