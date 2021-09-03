using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Desai.Models
{
    public class UserOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto generates ID as primary key in database.
        public int ID { get; set; }
        [Required]
        [Display(Name = "Patty Type")]
        public string PattyType { get; set; }
        public int TotalPatties { get; set; }
        [Display(Name="Cheese")]
        public Boolean Cheese { get; set; }
        public Boolean Bacon { get; set; }
        [Required]
        [RegularExpression("Y|N|y|n", ErrorMessage ="Must enter y or n")] // only allows upper/lower case y/n into the Fries textbox.
        public string Fries { get; set; }
        
        public string Other { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Cost { get; set; }


    }
}
