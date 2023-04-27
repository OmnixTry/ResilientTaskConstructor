using Component.TestCompletion.BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Component.TestCompletion.PL.Receiver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		IMongoCollection<AttemptDto> attemptCollection;
		IModel channel;
		public MessageController() 
		{
			MongoClient client = new MongoClient("mongodb://localhost:27017");
			IMongoDatabase database = client.GetDatabase("TestCompletion");
			attemptCollection = database.GetCollection<AttemptDto>("Attempts");

			var factory = new ConnectionFactory
			{
				HostName = "localhost",
				//Ssl = new SslOption()
				//{
				//	ServerName = "localhost",
				//	Enabled = true
				//},
				RequestedHeartbeat = TimeSpan.FromSeconds(10)
			};
			//using var connection = factory.CreateConnection();
			//using var channel = connection.CreateModel();

			var connection = factory.CreateConnection();
			IModel channel = connection.CreateModel();
			channel.QueueDeclare(//queue: "AsyncCheckMessages",
								 queue: "PeopleMessages",
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);
			this.channel = channel;
		}
		// GET: api/<MessageController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			MongoClient client = new MongoClient("mongodb://localhost:27017");
			IMongoDatabase database = client.GetDatabase("TestCompletion");
			var collection = database.GetCollection<AttemptDto>("Attempts");

			var attempt = new AttemptDto() { Id = 1 , Date = DateTime.Now, MaxScore = 1, Score = 1, StudentId = "ssssss", TestId = 1 };
			collection.InsertOne(attempt);
			return new string[] { "value1", "value2" };
		}

		// POST api/<MessageController>
		[HttpPost]
		public void Post([FromBody] AttemptDto value)
		{
			value.AsyncCheckId = Guid.NewGuid().ToString();
			attemptCollection.InsertOne(value);

			//var factory = new ConnectionFactory { HostName = "localhost",
			//	//Ssl = new SslOption()
			//	//{
			//	//	ServerName = "localhost",
			//	//	Enabled = true
			//	//},
			//	RequestedHeartbeat = TimeSpan.FromSeconds(10)
			//};
			//using var connection = factory.CreateConnection();
			//using var channel = connection.CreateModel();

			channel.QueueDeclare(//queue: "AsyncCheckMessages",
								 queue: "PeopleMessages",
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);

			var message = new AsyncCheckMessageDto() { AsyncCheckId = value.AsyncCheckId };
			var serialized = JsonSerializer.Serialize(message);
			var body = Encoding.UTF8.GetBytes(serialized);
			
			channel.BasicPublish(exchange: string.Empty,
						 //routingKey: "AsyncCheckMessages",
						 routingKey: "PeopleMessages",
						 basicProperties: null,
						 body: body);

		}
	}
}
