namespace HRM.Data.Base
{
    public interface IEntityBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string CreateBy { get; set; }
    }
}
