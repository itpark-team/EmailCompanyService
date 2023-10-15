using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.DbConnector;
using ConsoleAppServerSide.Services;
using NLog;

namespace ConsoleAppServerSide.CommunicationEngine;

public class ClientEngine
{
    private Socket _clientSocket;
    private ServicesManager _servicesManager;
    private EcsDbContext _dbContext;

    private static ILogger Logger = LogManager.GetCurrentClassLogger();

    public ClientEngine(Socket clientSocket)
    {
        _clientSocket = clientSocket;

        _dbContext = new EcsDbContext();

        _servicesManager = new ServicesManager(_dbContext);
    }

    public void Communicate()
    {
        while (true)
        {
            string messageFromClient = ReceiveMessage();

            ClientRequest clientRequest = JsonSerializer.Deserialize<ClientRequest>(messageFromClient)!;

            ServerResponse serverResponse = null;

            try
            {
                serverResponse = _servicesManager.ProcessClientRequest(clientRequest);
            }
            catch (Exception e)
            {
                serverResponse = new ServerResponse(Statuses.ServerError, e.ToString());
            }

            string messageToClient = JsonSerializer.Serialize(serverResponse);

            SendMessage(messageToClient);
        }
    }

    private void SendMessage(string messageToClient)
    {
        byte[] outputBytes = Encoding.Unicode.GetBytes(messageToClient);
        _clientSocket.Send(outputBytes);

        Logger.Info($"MESSAGE TO CLIENT SENT: {messageToClient}");
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
        _clientSocket.Close();

        Logger.Debug($"CLIENT FINISHED");
    }
}