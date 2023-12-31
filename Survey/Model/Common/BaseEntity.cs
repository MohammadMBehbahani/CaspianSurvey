namespace Model.Common
{


    public class BaseEntity : BaseEntity<Guid>
    {
    }

    public class BaseEntity<T>
    {
        public required T Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
