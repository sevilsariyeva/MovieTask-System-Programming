using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTask.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string ImagePath { get; set; }
        public string About { get; set; }
        public double Rating { get; set; }
    }
}
