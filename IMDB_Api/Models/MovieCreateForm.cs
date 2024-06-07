﻿using IMDB_Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace IMDB_Api.Models
{
    public class MovieCreateForm
    {
        [Required(ErrorMessage = "Un film ça a un titre abruti")]
        public string Title { get; set; }

        [Required]
        [MinLength(40, ErrorMessage = "La description doit faire un minimum de 40 caractères")]
        public string Description { get; set; }
        [Required]
        public int RealisatorId { get; set; }

        public IEnumerable<Actor> Casting{ get; set; }


    }
}
