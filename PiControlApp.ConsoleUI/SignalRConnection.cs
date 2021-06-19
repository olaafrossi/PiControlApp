// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 13
// by Olaaf Rossi

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PiControlApp.ConsoleUI
{
    public class SignalRConnection
    {
        private HubConnection _chatHub;

        public async void Start()
        {
            string url = "http://192.168.1.138:5000/ChatHub";

            try
            {
                HubConnection connection = new HubConnectionBuilder()
                    .WithUrl(url)
                    .WithAutomaticReconnect()
                    .Build();
                _chatHub = connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("No SignalR");
            }

            // receive a message from the hub
            _chatHub.On<string, string>("ReceiveMessage", (user, message) => OnReceiveMessage(user, message));
            _chatHub.On<string, int>("IntReceive", (s, i) => OnIntReceive(s, i));
            Task t = _chatHub.StartAsync();

            t.Wait();

            // send a message to the hub
            //await connection.InvokeAsync("SendMessage", user, msg);
        }

        private async void OnIntReceive(string s, int i)
        {
            Console.WriteLine($"{s}: {i}");
        }

        public async void SendMessage(string user, string msg)
        {
            await _chatHub.InvokeAsync("SendMessage", user, msg);
        }

        public async void SendInt(string user, int msg)
        {
            await _chatHub.InvokeAsync("SendMessage", user, msg);
        }

        private void OnReceiveMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}");
        }
    }
}