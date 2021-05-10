using System;
using System.IO;
using System.Threading;

using Azure.Storage.Queues;

using Microsoft.Extensions.Configuration;

using SampleMessageApp.Producer.Models;

namespace SampleMessageApp.Producer
{
    class Program
    {
        private static string connectionString;
        private static string queueName;


        static void Main(string[] args)
        {
            InitializeConfiguration();

            ProduceMessages();

            Console.ReadLine();
        }

        private static void ProduceMessages()
        {
            var queueClient = new QueueClient(connectionString, queueName);

            var timer = new Timer(_ => {
                var message = $"This is a message from {DateTime.UtcNow}";
                Console.WriteLine($"Sending message : '{message}'");
                SendMessage(queueClient, message);
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
            
        }

        private static void SendMessage(QueueClient queueClient, string message)
        {
            queueClient.SendMessage(message);
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets("19b2912a-d0c8-4b2f-abd2-0593050019cf");

            IConfiguration configuration = builder.Build();
            var storageAccountSettings = new StorageAccountSettings();
            configuration.GetSection("StorageAccountSettings").Bind(storageAccountSettings);

            connectionString = storageAccountSettings.ConnectionString;
            queueName = storageAccountSettings.QueueName;
        }
    }
}
