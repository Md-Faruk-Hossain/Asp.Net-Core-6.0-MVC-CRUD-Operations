using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required,StringLength(60),Display(Name ="Name")]
        public string? EmployeeName { get; set; }
        [Required,  Display(Name = "Number")]
        public int PhoneNumber { get; set; }
        [Required, Display(Name = "DOB"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required, Display(Name = "Email")]
        public string? Email { get; set; }
        [Required, Display(Name = "Picture")]
        public string? Picture { get; set; }
        [Required, Display(Name = "City")]
        public string? City { get; set; }
        [Required, Display(Name = "Postal Code")]
        public int PostalCode { get; set;}
        [Required, Display(Name = "Address")]
        public string? Address { get; set; }
        [Required, Display(Name = "Country")]
        public string? Country { get; set; }
        public virtual ICollection<EmpEntry>? EmpEntries { get; set; } = new List<EmpEntry>();
    }
    //public class Gender
    //{
    //    public int GenderId { get; set; }
    //    public string? Name { get; set; }
    //}
    public class Designation
    {
        public int DesignationId { get; set; }
        [Required,StringLength(60),Display(Name ="Name")]
        public string? DesignationName { get; set; }
        public virtual ICollection<EmpEntry>? EmpEntries { get; set; } = new List<EmpEntry>();
    }
    public class EmpEntry
    {
        public int EmpEntryId { get; set;}
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Designation")]
        public int DesignationId { get; set; }
        //nev
        public virtual Employee? Employee { get; set; }
        public virtual Designation? Designation { get; set; }

    }
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Designation> Designations { get; set; } = default!;
        public DbSet<EmpEntry> EmpEntries { get; set; } = default!;


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderItem>().HasKey(o => new { o.Customer, o.FoodItemId });
        //}
    }
}
