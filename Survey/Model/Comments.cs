using Model.Common;

namespace Model
{
    public class Comments: BaseEntity
    {
        public required string Comment { get; set; }
        public virtual Guid? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
