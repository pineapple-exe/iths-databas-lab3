namespace SaintNicholas.Data
{
    public class BehavioralRecord
    {
        public int ChildID { get; set; }
        public int Year { get; set; }
        public bool Naughty { get; set; }

        public Child Child { get; set; }
    }
}
