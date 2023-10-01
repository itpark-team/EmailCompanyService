using System.Net.Sockets;
using ConsoleAppServerSide.CommunicationEngine;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();

while (true)
{
    Socket clientSocket = serverEngine.AcceptClient();

    ClientEngine clientEngine = new ClientEngine(clientSocket);

    Task.Run(() => clientEngine.Communicate());
}