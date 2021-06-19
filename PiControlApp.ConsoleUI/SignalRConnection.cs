﻿// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 13
// by Olaaf Rossi

using System;
using System.Diagnostics;
using System.Threading;
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
            Task task = ConnectWithRetryAsync(_sensorHub, CancellationToken.None);
        }

        private static async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken token)
        {
            connection.Reconnecting += error =>
            {
                Debug.Assert(connection.State == HubConnectionState.Reconnecting);
                WriteMessages();
                Console.WriteLine(connection.State);
                return Task.CompletedTask;
            };

            connection.Reconnected += connectionId =>
            {
                Debug.Assert(connection.State == HubConnectionState.Connected);
                WriteMessages();
                Console.WriteLine(connection.State);
                return Task.CompletedTask;
            };

            // receive a message from the hub
            connection.On<string, string>("ReceiveMessage", OnReceiveMessage);
            connection.On<string, int>("ReceiveInt", OnIntReceive);

            // Keep trying to until we can start or the token is canceled.
            while (true)
            {
                try
                {
                    await connection.StartAsync(token).ConfigureAwait(false);
                    Debug.Assert(connection.State == HubConnectionState.Connected);
                    return true;
                }
                catch when (token.IsCancellationRequested)
                {
                    return false;
                }
                catch
                {
                    // Failed to connect, trying again in 5000 ms.
                    Debug.Assert(connection.State == HubConnectionState.Disconnected);
                    await Task.Delay(5000).ConfigureAwait(false);
                }
            }
        }

        private static void OnIntReceive(string user, int i)
        {
            Console.WriteLine($"{user}: {i}: {DateTime.Now}");
        }

        private static void OnReceiveMessage(string user, string message)
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

        private static void WriteMessages()
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