using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Hubs
{
    /*
    Hub as a Chat Room:
    Think of a hub as a chat room in a building.
    This chat room is a special place where people can talk to each other.

    Server as the Building:
    The server is like the entire building.
    It hosts different chat rooms (hubs) for different conversations.

    Clients as People:
    Clients are like individual people who want to join a conversation.
    They are like visitors in the building.
    */
    public class ChatHub:Hub
	{
        //This 'Share' method is consumed by connection.invoke in the chat.js file.
        public async Task Share (string user, string message)
		{
            /*
            Listening to Others (Receiving Messages):
            Everyone in the chat room can hear what others are saying.
            The server (chat room) helps in making sure messages are shared with everyone.

            connection.on("receive", function (user, message) {
    
            });
            */

            //await Clients.All.SendAsync("receive", user, message);

            /*
			Sends a message to the client (caller) who invoked the Share method.
			The message is sent with the event name "receive" and includes the user as "-----" (a placeholder) and the actual message.
			*/
            //This "receive" is used in the connection.on("receive", function (user, message) in the chat.js file.
            await Clients.Caller.SendAsync("receive", "-----", message);

            /*
			Sends the same message to all other clients connected to the hub (excluding the caller).
			The event name is "receive," and it includes the actual user and message.
			*/
            await Clients.Others.SendAsync("receive", user, message);			
		}
	}
}
