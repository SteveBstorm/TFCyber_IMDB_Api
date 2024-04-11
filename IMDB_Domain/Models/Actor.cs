using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_Domain.Models
{
    public class Actor : Person
    {
        //public Person Acteur { get; set; }
        public string Role { get; set; }
    }
}
