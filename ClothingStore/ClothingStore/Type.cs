using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore
{
    public class Type
    {
        [Key]
        public int Type_Id { get; set; }
        public string Type_Name { get; set; }
        public List<Clothing> Clothes { get; set; }
    }
}
