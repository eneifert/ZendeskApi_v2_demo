using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zendesk_v2_Demo.ViewModels
{
    public class CreateTicketViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string TicketPriority { get; set; }
        public string RequesterEmail { get; set; }

        public string TicketId { get; set; }
        public string ZendeskUrl { get; set; }
    }
}