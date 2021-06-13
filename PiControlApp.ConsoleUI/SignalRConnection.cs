// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 13
// by Olaaf Rossi

using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace PiControlApp.ConsoleUI
{
    public class SignalRConnection
    {
        public async void Start(string user, string msg)
        {
            var url = "http://192.168.1.138:5000/ChatHub";

            var connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            // receive a message from the hub
            connection.On<string, string>("ReceiveMessage", (user, message) => OnReceiveMessage(user, message));

            var t = connection.StartAsync();

            t.Wait();

            // send a message to the hub
            await connection.InvokeAsync("SendMessage", user, msg);
        }

        private void OnReceiveMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
        }
    }
}