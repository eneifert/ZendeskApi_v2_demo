using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.AccountsAndActivities;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Users;
using Zendesk_v2_Demo.ViewModels;
using User = ZendeskApi_v2.Models.Users.User;

namespace Zendesk_v2_Demo.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            return View(new CreateTicketViewModel());
        }

        public ActionResult Create(CreateTicketViewModel model)
        {
            var zendesk = "https://csharpapi.zendesk.com/";
            //set up the api
            var api = new ZendeskApi(string.Format("{0}api/v2", zendesk), "eric.neifert@gmail.com", "pa55word");

            //create the user if they don't already exist
            var user = api.Users.SearchByEmail(model.RequesterEmail);
            if (user == null || user.Users.Count < 1)
                api.Users.CreateUser(new User()
                                         {
                                             Name = model.RequesterEmail,
                                             Email = model.RequesterEmail
                                         });
            
            //setup the ticket
            var ticket = new Ticket()
            {
                Subject = model.Subject,
                Description = model.Description,
                Priority = model.TicketPriority,
                Requester = new Requester() { Email = model.RequesterEmail }
            };

            //create the new ticket
            var res = api.Tickets.CreateTicket(ticket).Ticket;            

            return View("Index", new CreateTicketViewModel() {TicketId = res.Id.ToString(), ZendeskUrl = zendesk});
        }
    }
}
