using System;
using System.Collections.Generic;

namespace BackendServiceDispatcher.Models
{

    public class AppointmentModel
    {
        public string OrgnizerName { get; set; }
        public string OrgnizerEmail { get; set; }
        public string Subject { get; set; }
        public string TextContent { get; set; }
        public string HTMLContent { get; set; }
        public List<Atteendee> Atteendees { set; get; }
        public string Location { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        //Cancel Existing Event or New Event 
        public bool IsCanlcel { set; get; }
        public bool UsingHtmlContent
        {
            get
            {
                return HTMLContent == String.Empty;
            }
        }

        public bool ValidAppoitment
        {
            get
            {
                return DateTime.Compare(StartTime, EndTime) <= 0;
            }
        }
    }

    public class Atteendee
    {
        public string Name { set; get; }
        public string Email { set; get; }
    }
}

