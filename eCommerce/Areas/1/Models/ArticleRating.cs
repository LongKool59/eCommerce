using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Areas.User.Models
{
    public class ArticleRating
    {
        public int ArticleID { get; set; }
        public int Rating { get; set; }
        public int TotalRaters { get; set; }
        public double AverageRating { get; set; }
    }
}