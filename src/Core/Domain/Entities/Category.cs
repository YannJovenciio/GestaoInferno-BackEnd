namespace Inferno.src.Core.Domain.Entities
{
    public class Category
    {
        public Guid IdCategory { get; set; }
        public required string CategoryName { get; set; }
        public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();

        public Category() { }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
