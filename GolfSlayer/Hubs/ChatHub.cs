using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace GolfSlayer.Hubs
{

    public class ChatHub : Hub
    {
        static List<MessageDetail> CurrentMessages = new List<MessageDetail>();

        public void Send(String Name, String Message, String Room)
        {
            CurrentMessages.Add(new MessageDetail { Name = Name, Message = Message, Room = Room, TimeStamp = DateTime.Now });

            Clients.All.addNewMessageToPage(Name, Message, Room);
        }

        public void GetCachedMessages()
        {
            Clients.Caller.getCurrentMessages(CurrentMessages.OrderBy(x => x.TimeStamp));
        }

        public void ClearAllMessages()
        {
            CurrentMessages = new List<MessageDetail>();
        }
    }
    public class MessageDetail
    {
        public String Name {get; set;}
        public String Message {get; set;}
        public String Room {get; set;}
        public DateTime TimeStamp { get; set; }
    }
}