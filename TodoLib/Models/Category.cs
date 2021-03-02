using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoLib.Models {
    
    public class Category {

        public int Id { get; set; }
        [StringLength(80), Required]
        public string Name { get; set; }

    }
}
