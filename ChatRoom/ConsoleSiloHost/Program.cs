using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using ChatGrainInterfaces;

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
            var silo = new SiloHost("TestSilo", siloConfig);
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
            var usr1 = new User() { Id = Guid.NewGuid(), Nickname = "eugene" };
            var usr2 = new User() { Id = Guid.NewGuid(), Nickname = "petro" };

            var usr1Grain = client.GetGrain<IUserGrain>(usr1.Id);
            var usr2Grain = client.GetGrain<IUserGrain>(usr2.Id);

            usr1Grain.Create(usr1).Wait();
            usr2Grain.Create(usr2).Wait();

            var chatRoom = new ChatRoom() { Id = Guid.NewGuid() };
            var chatRoomGrain = client.GetGrain<IChatRoomGrain>(chatRoom.Id);
            chatRoomGrain.Create(chatRoom).Wait();

            chatRoomGrain.Join(usr1, usr2);

            chatRoomGrain.AddMessage(new Message { Id = Guid.NewGuid(), SenderId = usr1.Id, Content = "Hey!" });

            //chatRoomGrain.

            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
            silo.ShutdownOrleansSilo();
        }
    }
}
