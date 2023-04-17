using Component.TestManagement.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Dto
{
	public class TaskDto
	{
		public int Id { get; set; }
		public int TaskId { get; set; }
		public int AttemptId { get; set; }
		public int Score { get; set; }
		public TestTaskDto? Task { get; set; }
		public List<AnswerDto>? Answers { get; set; }
	}
}
