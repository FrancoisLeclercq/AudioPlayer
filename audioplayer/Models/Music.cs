using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace audioplayer.Models
{
    public class Music
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] FileData { get; set; }

        public string Date { get; set; }
    }
}
