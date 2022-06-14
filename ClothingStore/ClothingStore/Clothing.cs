using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore
{
    public class Clothing
    {
        [Key]
        public int Clothes_number { get; set; }
        public string Clothes_Name { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public Type Type { get; set; }
    }
}