using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.CommunicationEngine;
using ConsoleAppServerSide.DbConnector;
using ConsoleAppServerSide.Models;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage();

ClientRequest clientRequest = JsonSerializer.Deserialize<ClientRequest>(messageFromClient);

RequestRegisterUserDto requestRegisterUserDto =
    JsonSerializer.Deserialize<RequestRegisterUserDto>(clientRequest.JsonData);

EcsDbContext db = new EcsDbContext();

User user = new User()
{
    Login = requestRegisterUserDto.Login,
    Password = requestRegisterUserDto.Password,
    Email = requestRegisterUserDto.Email,
    Name = requestRegisterUserDto.Name
};

db.Users.Add(user);
db.SaveChanges();
