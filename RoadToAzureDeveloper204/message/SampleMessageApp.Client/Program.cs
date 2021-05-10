using System;
using System.IO;
using System.Threading;

using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

using Microsoft.Extensions.Configuration;

using SampleMessageApp.Client.Models;

namespace SampleMessageApp.Client
{
    class Program
    {
        private static string connectionString;
        private static string queueName;

        static void Main(string[] args)
        {
            InitializeConfiguration();



            CreateQueueIfNotExists(connectionString, queueName);

            PeekMessage();

            ConsumeMessage();

            //DeleteQueeuIfExists(connectionString, queueName);

            Console.ReadLine();
        }

        private static void ConsumeMessage()
        {
            var queueClient = new QueueClient(connectionString, queueName);

            var timer = new Timer(_ =>
            {
                Response<QueueMessage> message = queueClient.ReceiveMessage();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Received message: '{message.Value}'");
                Console.ForegroundColor = ConsoleColor.White;

            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));
        }

        private static void PeekMessage()
        {
            var queueClient = new QueueClient(connectionString, queueName);

            var timer = new Timer(_ =>
            {
                // Peek at the next message
                PeekedMessage[] peekedMessages = queueClient.PeekMessages(5);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Peek 5 messages");
                Console.ForegroundColor = ConsoleColor.White;
                // Display the message
                foreach (var peekedMessage in peekedMessages)
                {
                    Console.ForegroundColor = DateTime.UtcNow.Second % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.White;
                    Console.WriteLine($"Peeked message: '{peekedMessage.MessageText}'");

                }
                
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets("a0f7f069-a3fc-4afd-b6d5-5f749776a83c");

            IConfiguration configuration = builder.Build();
            var storageAccountSettings = new StorageAccountSettings();
            configuration.GetSection("StorageAccountSettings").Bind(storageAccountSettings);

            connectionString = storageAccountSettings.ConnectionString;
            queueName = storageAccountSettings.QueueName;
        }

        private static void DeleteQueeuIfExists(string connectionString, string queueName)
        {
            var queueClient = new QueueClient(connectionString, queueName);
            queueClient.DeleteIfExists();
        }

        private static void CreateQueueIfNotExists(string connectionString, string queueName)
        {
            var queueClient = new QueueClient(connectionString, queueName);
            queueClient.CreateIfNotExists();
        }
    }
}
