using Ical.Net.CalendarComponents;

namespace DataLogic.DbMethods
{
    internal class Event : CalendarEvent
    {
        public string Class { get; set; }
        public string Summary { get; set; }
        public object Created { get; set; }
        public object Description { get; set; }
        public object Start { get; set; }
        public object End { get; set; }
        public int Sequence { get; set; }
        public string Uid { get; set; }
        public object Location { get; set; }
    }
}