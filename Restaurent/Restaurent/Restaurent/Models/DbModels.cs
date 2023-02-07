using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Models
{
    //[Authorize(Roles ="Admin,")]
    
    public class Customer
    {

        public Customer()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public int CustomerId { get; set; }
        [Required, Display(Name = "Name"), StringLength(70)]
        public string? CustomerName { get; set; }
        [Required, Display(Name = "Phone")]
        public int CustomerPhone { get; set; }
        public bool IsOrder { get; set; }
        public string? Picture { get; set; }
        [Required, Display(Name = "Address"), StringLength(200)]
        public string? Address { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }

    public class FoodItem
    {
        public FoodItem()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public int FoodItemId { get; set; }
        [Required, StringLength(50), Display(Name = "Food Name")]
        public string? FoodName { get; set; }
        [Required, Display(Name = "Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }
        public string? Picture { get; set; }
        
        public bool IsAvailable { get; set; }
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        
        

        //
        public virtual ICollection<OrderItem>? OrderItems { get; set; }

    }
    public class OrderItem
    {

        public int OrderItemId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }
        //[ForeignKey("Order")]
        //public int Order { get; set; }
      
        //Nev
        public virtual Customer? Customer { get; set; }
        public virtual FoodItem? FoodItem { get; set; }
        //public virtual Order? Orders { get; set; }
    }
    //public class Order
    //{
    //    public int OrderID { get; set; }
    //    [Required, Column(TypeName = "date"),
    //        Display(Name = "Order Date"),
    //        DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
    //        ApplyFormatInEditMode = true)]
    //    public DateTime OrderDate { get; set; }
    //    [Column(TypeName = "date"),
    //        Display(Name = "Delivery Date"),
    //        DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
    //        ApplyFormatInEditMode = true)]
    //    public DateTime DeliveryDate { get; set; }
    //    [Required, EnumDataType(typeof(Status))]
    //    public Status Status { get; set; }


    //    [Required, ForeignKey("Customer")]
    //    public int CustomerId { get; set; }
    //    [Required, ForeignKey("FoodItem")]
    //    public int FoodItemId { get; set; }
    //    public FoodItem? FoodItem { get; set; }
    //    public Customer? Customer { get; set; } 
    //    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    //}
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<FoodItem> FoodItems { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderItem>().HasKey(o => new { o.Customer, o.FoodItemId });
        //}
    }
}

