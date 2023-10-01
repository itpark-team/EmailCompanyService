using System.Net;
using System.Net.Sockets;
using System.Text;
using NLog;

namespace ConsoleAppServerSide.CommunicationEngine;

public class ServerEngine
{
    private Socket _serverSocket;
    private IPEndPoint _ipEndPoint;

    private static ILogger Logger = LogManager.GetCurrentClassLogger();

    public ServerEngine(string ip, int port)
    {
        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    public void StartServer()
    {
        _serverSocket.Bind(_ipEndPoint);
        _serverSocket.Listen(10);
        Logger.Debug("SERVER STARTED");
    }

    public Socket AcceptClient()
    {
        Socket clientSocket = _serverSocket.Accept();

        Logger.Debug($"CLIENT ACCEPT FROM {clientSocket.RemoteEndPoint}");

        return clientSocket;
    }

    public void CloseServerSocket()
    {
        _serverSocket.Close();

        Logger.Debug($"SERVER FINISHED");
    }
}