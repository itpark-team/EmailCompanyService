using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.Services;
using NLog;

namespace ConsoleAppServerSide.CommunicationEngine;

public class ClientEngine
{
    private Socket _clientSocket;
    private ServicesManager _servicesManager;

    private static ILogger Logger = LogManager.GetCurrentClassLogger();

    public ClientEngine(Socket clientSocket)
    {
        _clientSocket = clientSocket; 
        _servicesManager = new ServicesManager();
    }

    public void Communicate()
    {
        while (true)
        {
            string messageFromClient = ReceiveMessage();

            ClientRequest clientRequest = JsonSerializer.Deserialize<ClientRequest>(messageFromClient);

            ServerResponse serverResponse = _servicesManager.ProcessClientRequest(clientRequest);

            string messageToClient = JsonSerializer.Serialize(serverResponse);

            SendMessage(messageToClient);
        }
    }

    private void SendMessage(string messageToClient)
    {
        byte[] outputBytes = Encoding.Unicode.GetBytes(messageToClient);
        _clientSocket.Send(outputBytes);

        Logger.Debug($"MESSAGE TO CLIENT SENT: {messageToClient}");
    }

    private string ReceiveMessage()
    {
        StringBuilder messageBuilder = new StringBuilder();
        do
        {
            byte[] inputBytes = new byte[1024];
            int countBytes = _clientSocket.Receive(inputBytes);
            messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
        } while (_clientSocket.Available > 0);

        string messageFromClient = messageBuilder.ToString();

        Logger.Debug($"MESSAGE FROM CLIENT RECIEVED: {messageFromClient}");

        return messageFromClient;
    }

    private void CloseClientSocket()
    {
        _clientSocket.Shutdown(SocketShutdown.Both);
        _clientSocket.Close();

        Logger.Debug($"CLIENT FINISHED");
    }
}