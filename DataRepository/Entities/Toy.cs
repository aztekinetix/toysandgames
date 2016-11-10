using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataRepository.Entities

{
    public class Toy:IEntity
    {
        
        //[Required]
        //public int Id { get; set; } Inherited from IEntity
        [Required]
        [MaxLength(50,ErrorMessage ="The maximum lenght is 50 for Name")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "The maximum lenght is 100 for Description")]
        public string Description { get; set; }
        [Range(0, 100, ErrorMessage = "Max age is 100 years :)")]
        public int? AgeRestriction { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The maximum lenght is 50 for Company")]
        public string Company { get; set; }
        [Required]
        [Range(1,1000,ErrorMessage ="Only amounts from $1 to $1000")]
        public decimal Price { get; set; }
    }

}
