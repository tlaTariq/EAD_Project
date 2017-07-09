using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EADProject.Models
{
    public class SongsDTO
    {
        public int SongID { get; set; }
        public string Name { get; set; }
        public string Singer { get; set; } 
        public string Link { get; set; }
    }
}