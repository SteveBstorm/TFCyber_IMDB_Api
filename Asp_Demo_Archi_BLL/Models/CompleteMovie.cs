using IMDB_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Demo_Archi_BLL.Models
{
    public class CompleteMovie : Movie
    {
        public Person Realisator { get; set; }

        public IEnumerable<Actor> Casting { get; set; }
    }
}
