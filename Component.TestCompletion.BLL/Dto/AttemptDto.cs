using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Dto
{
	public class AttemptDto
	{
		public int Id { get; set; }
		public DateTime? Date { get; set; }
		public int TestId { get; set; }
		public string? StudentId { get; set; }
		public int? Score { get; set; }
		public int? MaxScore { get; set; }
		public short Hash { get; set; }
		public List<TaskDto>? Tasks { get; set; }

		[BsonId]
		public string? AsyncCheckId { get; set; }
	}
}
