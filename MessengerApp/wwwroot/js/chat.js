// Reference the SignalR JavaScript library 
/// <reference path="../lib/microsoft/signalr/dist/browser/signalr.js" />

// Prompt the user to enter their name and set a default value
var username = prompt("enter your name", "DITC");

// Create a SignalR HubConnectionBuilder instance
var hubBuilder = new signalR.HubConnectionBuilder();

// Build a SignalR connection to the "/chat" hub endpoint
//Client Side:
//Imagine the client as a person who wants to join a specific conversation in that house.
//The line var connection = hubBuilder.withUrl("/chat").build(); is like the person finding the room labeled "Chat" and entering it to join the conversation.
// See the program.cs file >> app.MapHub<ChatHub>("/chat");
var connection = hubBuilder.withUrl("/chat").build();


/*This sets up a client-side event handler for the "receive" event. When the server sends a "receive" event, this function is called. It creates a new list item (li) with the received user and message, and appends it to the element with the ID "chatLog" using jQuery.*/
connection.on("receive", function (user, message)
{
    // Create a new list item element
    var li = document.createElement("li"); // In this line, document.createElement("li") creates a new HTML element in JavaScript. The argument "li" specifies that the new element should be an unordered list item (<li>). 

    // Set the text content of the list item with the received user and message
    li.textContent = `${user} : ${message}`;

    // Append the list item to the chat log
    $('#chatLog').append(li);  
});

// Start the SignalR connection
connection.start();

/*This line sets up an event handler for the click event on the element with the ID "btnSend". When the button is clicked, it retrieves the value of the input element with the ID "txtInput" and sends it to the server using the SignalR hub connection. The server method invoked is named "Share", and it includes the username and message as parameters. After sending the message, it clears the input field.*/
$('#btnSend').on('click', function ()
{
    // Get the message from the input field
    var message = $('#txtInput').val();

    // Invoke the "Share" method on the server with the username and message.
    // Here, "Share" is a method which is defined in the ChatHub.cs file.
    connection.invoke("Share", username, message); 

    // Clear the input field
    $('#txtInput').val(null);
})
