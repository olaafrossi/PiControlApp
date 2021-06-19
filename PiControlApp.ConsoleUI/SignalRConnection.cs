// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 13
// by Olaaf Rossi

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace PiControlApp.ConsoleUI
{
    public class SignalRConnection
    {
        private readonly HubConnection _sensorHub;

        public SignalRConnection(string url)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();
            _sensorHub = connection;
            Task task = StartAsync();
        }

        private async Task StartAsync()
        {
            // receive a message from the hub
            _sensorHub.On<string, string>("ReceiveMessage", (user, message) => OnReceiveMessage(user, message));
            _sensorHub.On<string, int>("ReceiveInt", (user, i) => OnIntReceive(user, i));
            Task t = _sensorHub.StartAsync();

            _sensorHub.Reconnecting += error =>
            {
                Debug.Assert(_sensorHub.State == HubConnectionState.Reconnecting);
                WriteMessages();
                Console.WriteLine(_sensorHub.State);
                return Task.CompletedTask;
            };

            _sensorHub.Reconnected += connectionId =>
            {
                Debug.Assert(_sensorHub.State == HubConnectionState.Connected);
                WriteMessages();
                Console.WriteLine(_sensorHub.State);
                return Task.CompletedTask;
            };

            t.Wait();
        }

        private void OnIntReceive(string user, int i)
        {
            Console.WriteLine($"{user}: {i}: {DateTime.Now}");
        }

        private void OnReceiveMessage(string user, string message)
        {
            Console.WriteLine($"{user}: {message}: {DateTime.Now}");
        }

        public Task SendMessageAsync(string user, string msg)
        {
            return _sensorHub.InvokeAsync("SendMessage", user, msg);
        }

        public Task SendIntAsync(string user, int msg)
        {
            return _sensorHub.InvokeAsync("SendInt", user, msg);
        }

        private void WriteMessages()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
        }
    }
}