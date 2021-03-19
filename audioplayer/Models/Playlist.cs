using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace audioplayer.Models
{
    public class Playlist
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "Playlist Id")]
        public int Id { get; set; }

        [Required]
        //[Column(TypeName = "TEXT")]
        public string Name { get; set; }

        //[Column(TypeName = "TEXT")]
        public string Image { get; set; }

        //[Column(TypeName = "TEXT")]
        public string Titles { get; set; }
    }
}
