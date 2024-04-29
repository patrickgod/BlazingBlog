namespace BlazingBlog.Domain.Abstractions
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
    }
}
