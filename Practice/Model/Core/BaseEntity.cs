namespace Practice.Model.Core
{
    public class BaseEntity
    {
        public DateTime CreDate { get; set; }
        public int CreUser { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
