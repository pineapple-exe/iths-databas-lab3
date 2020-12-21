namespace SaintNicholas.Data
{
    public class ChristmasPresent
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public string ForGender { get; set; }
        public bool ForNaughtyChild { get; set; }
        public int? Receiver { get; set; }
        public int HandOutYear { get; set; }
    }
}
