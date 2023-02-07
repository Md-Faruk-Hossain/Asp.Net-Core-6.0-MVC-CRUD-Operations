using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Restaurent.Models.ViewModel
{
    public class CustomerVM
    {
        public CustomerVM()
        {
            this.FoodItemList = new List<int>();
        }
        public int CustomerId { get; set; }
        [Required, Display(Name = "Name"), StringLength(70)]
        public string? CustomerName { get; set; }
        [Required, Display(Name = "Phone")]
        public int CustomerPhone { get; set; }
        public bool IsOrder { get; set; }
        public string? Picture { get; set; }
        public IFormFile PicturFile { get; set; } = default!;
        [Required, Display(Name = "Address"), StringLength(200)]
        public string? Address { get; set; }
        public  List<int>? FoodItemList { get; set; }
    }
    
}
