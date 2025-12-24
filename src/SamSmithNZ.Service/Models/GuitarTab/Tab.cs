using System;

namespace SamSmithNZ.Service.Models.GuitarTab
{
    public class Tab
    {
        public int TabCode { get; set; }
        public int AlbumCode { get; set; }
        public string TabName { get; set; }
        public string TabNameTrimed { get; set; }
        public string TabText { get; set; }
        public int TabOrder { get; set; }
        public int? Rating { get; set; }
        public int TuningCode { get; set; }
        public string TuningName { get; set; }
        public DateTime LastUpdated { get; set; }
        
    }
}

