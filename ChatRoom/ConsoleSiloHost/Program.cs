using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using ChatGrainInterfaces;
using Orleans.Providers;
using System.Collections.Generic;
using ChatGrains;

namespace ConsoleSiloHost
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // First, configure and start a local silo
            var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            var props = new Dictionary<string, string> {
                { "UseJsonFormat", "true" }
            };
            IProviderConfiguration pConfig = new ProviderConfiguration(props, "Orleans.Storage.DynamoDBStorageProvider", "DynamoDBStorage");
            var catConfig = new ProviderCategoryConfiguration("Storage")
            {
                Providers = new Dictionary<string, IProviderConfiguration> {
                    { "DynamoDBStorage", pConfig }
                }
            };
            siloConfig.Globals.ProviderConfigurations.Add("Storage", catConfig);
            //siloConfig.Add.AddMemoryStorageProvider();
            //siloConfig.LoadFromFile("DevTestServerConfiguration.xml");
            var silo = new SiloHost("TestSilo", siloConfig);
            //silo.ConfigFileName = "DevTestServerConfiguration.xml";

            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            // Then configure and connect a client.
            var clientConfig = ClientConfiguration.LocalhostSilo();
            var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            client.Connect().Wait();

            Console.WriteLine("Client connected.");

            //
            // This is the place for your test code.
            //
            var usr1 = new User() { Id = Guid.Parse("8ac512e4-a618-4ba0-b69b-c3051d53888a"), Nickname = "eugene" };
            var usr2 = new User() { Id = Guid.Parse("8ac512e4-a618-4ba0-b69b-c3051d53888b"), Nickname = "petro" };

            var usr1Grain = client.GetGrain<IUserGrain>(usr1.Id);
            var usr2Grain = client.GetGrain<IUserGrain>(usr2.Id);

            usr1Grain.Create(usr1).Wait();
            usr2Grain.Create(usr2).Wait();

            var chatRoom = new ChatRoom() { Id = Guid.Parse("8ac512e4-a618-4ba0-b69b-c3051d53888c"), Name = "eugene&petro" };
            var chatRoomGrain = client.GetGrain<IChatRoomGrain>(chatRoom.Id);
            chatRoomGrain.Create(chatRoom).Wait();

            chatRoomGrain.Join(usr1Grain, usr2Grain).Wait();

            SubscribeToMessageNotifications(client, chatRoom, chatRoomGrain);

            usr1Grain.SendMsg("Hi, group!", chatRoomGrain).Wait();

            var msgs = chatRoomGrain.GetMessages().Result;

            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
            silo.ShutdownOrleansSilo();
        }

        private static void SubscribeToMessageNotifications(IClusterClient client, ChatRoom chatRoom, IChatRoomGrain chatRoomGrain)
        {
            var msgHub = new MessageHub(chatRoom);
            var msgHubObj = client.CreateObjectReference<IMessageHub>(msgHub).Result;
            chatRoomGrain.Subscribe(msgHubObj);
        }
    }
}
