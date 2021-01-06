namespace SaintNicholas.Data.Entities
{
    public class ChristmasPresent
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public string ForGender { get; set; }
        public bool ForNaughtyChild { get; set; }
        public int? ReceiverId { get; set; }
        public Child Receiver { get; set; }

        public int HandOutYear { get; set; }
    }
}
