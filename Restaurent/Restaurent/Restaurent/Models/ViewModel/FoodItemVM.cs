using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurent.Models.ViewModel
{
    public class FoodItemVM
    {
        public FoodItemVM()
        {
            this.OrderItemsList = new List<int>();
        }
        public int FoodItemId { get; set; }
        [Required, StringLength(50), Display(Name = "Food Name")]
        public string? FoodName { get; set; }
        [Required, Display(Name = "Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }
        public string? Picture { get; set; }
        public IFormFile Picturefile { get; set; } = default!;
        public bool IsAvailable { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public List<int> OrderItemsList { get; set;}
    }
}
