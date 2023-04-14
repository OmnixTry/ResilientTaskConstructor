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
	[Table("Result")]
	[EfEntity(typeof(TestCompletionContext))]
	public class Result : IEntity<int>
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public int TestId { get; set; }
		public string StudentId { get; set; }
		public int Score { get; set; }
		public ICollection<ResultTask> ResultTasks { get; set; }

		public Result() { }
		public Result(int Id_, DateTime Date_, int TestId_, string StudentId_)
		{
			this.Id = Id_;
			this.Date = Date_;
			this.TestId = TestId_;
			this.StudentId = StudentId_;
		}
	}
}
