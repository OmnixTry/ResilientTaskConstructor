using Component.TestCompletion.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompetion.DAL.Entity
{
	[Table("ResultTask")]
	[EfEntity(typeof(TestCompletionContext))]
	public class ResultTask : IEntity<int>
	{
		public int Id { get; set; }
		public int TestTaskId { get; set; }
		public int ResultId { get; set; }
		public int Score { get; set; }
		public byte Hash { get; set; }
		public ICollection<Answer> Answers { get; set; }

		public ResultTask() { }
		public ResultTask(int Id_, int TestTaskId_, int ResultId_)
		{
			this.Id = Id_;
			this.TestTaskId = TestTaskId_;
			this.ResultId = ResultId_;
		}
	}
}
