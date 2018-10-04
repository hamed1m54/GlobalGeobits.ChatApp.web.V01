using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.SignalR.hubs
{
    public class ChatHub : Hub
    {
        // a broadcast message
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }





        public void Connect(string UserName, int UserID)
        {

            //TooDo Connect on user private chat

        }



        public void SendPrivateMessage(string ReciverId, string message)
        {
            //TOODo send message to a client with id
        }

        public void ReceivePrivateMessage(string SenderId, string message)
        {
            //TOODo Receive message to a client with id
        }


    }

}