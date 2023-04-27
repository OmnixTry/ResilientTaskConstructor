using System.Text;
using System.Text.Json;
using Component.TestCompletion.BLL.Dto;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Component.Groups.BLL;
using Component.Groups.DAL;
using Component.TestCompetion.DAL;
using Component.TestCompletion.BLL;
using Component.TestManagement.BLL;
using Component.TestManagement.DAL;
using Infrastructure.DAL.Extension;
using Component.TestCompletion.BLL.Conract;

// setup DI
var serviceCollection = new ServiceCollection();
string connectionString = "server=.; database=AdaptiveEnglishTrainer; Integrated Security=true";
//serviceCollection.RegisterAuthServices();
serviceCollection.RegisterEfRepositories();
serviceCollection.RegisterDAL(connectionString);
serviceCollection.RegisterGroupsDAL(connectionString);
serviceCollection.RegisterCompletionDAL(connectionString);
serviceCollection.RegisterTestManagementBll();
serviceCollection.RegisterGroupsBLL();
serviceCollection.RegisterTestCompletionnBll();

var serviceProvider = serviceCollection.BuildServiceProvider();

var testCheckService = serviceProvider.GetService<ITestCompletionService>();


var factory = new ConnectionFactory { HostName = "localhost", RequestedHeartbeat = TimeSpan.FromSeconds(10)
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "PeopleMessages",
					 durable: false,
					 exclusive: false,
					 autoDelete: false,
					 arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += ReactForMessage;

channel.BasicConsume(queue: "PeopleMessages",
					 autoAck: true,
					 consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

void ReactForMessage(object model, BasicDeliverEventArgs ea)
{
	var body = ea.Body.ToArray();
	var stringifiedMessage = Encoding.UTF8.GetString(body);
	var message = JsonSerializer.Deserialize<AsyncCheckMessageDto>(stringifiedMessage);
	//Console.WriteLine($" [x] Received a message from {message.Author.Name}");
	Console.WriteLine($"     He Says: \"{stringifiedMessage}\"");

	MongoClient client = new MongoClient("mongodb://localhost:27017");
	IMongoDatabase database = client.GetDatabase("TestCompletion");
	var attemptCollection = database.GetCollection<AttemptDto>("Attempts");

	Console.WriteLine("Collection has " + attemptCollection.CountDocuments(doc => doc.AsyncCheckId != null) + " documents");
	var attempt = attemptCollection.FindOneAndDelete(doc => doc.AsyncCheckId == message.AsyncCheckId);
	Console.WriteLine($"     received : \"{attempt.TestId}\"");
	Console.WriteLine("Collection has " + attemptCollection.CountDocuments(doc => doc.AsyncCheckId != null) + " documents");

	testCheckService.CheckTest(attempt.TestId, attempt);
};