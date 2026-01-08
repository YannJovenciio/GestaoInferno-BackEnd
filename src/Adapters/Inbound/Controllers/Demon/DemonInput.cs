using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Core.Application.DTOs.Request.Demon
{
    public class DemonInput
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "DemonName is required")]
        public string DemonName { get; set; }

        public DemonInput() { }

        public DemonInput(string demonName, Guid? categoryId)
        {
            DemonName = demonName;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"DemonInput{{DemonName={DemonName}, CategoryId={CategoryId}}}";
        }
    }
}
