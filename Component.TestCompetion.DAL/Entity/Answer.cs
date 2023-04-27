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
	[Table("Answer")]
	[EfEntity(typeof(TestCompletionContext))]
	public class Answer : IEntity<int>
	{
		public int Id { get; set; }
		public int? TaskOptionId { get; set; }
		public int ResultTaskId { get; set; }
		public string Value { get; set; }
		public bool Corect { get; set; }
		public byte Hash { get; set; }


		public Answer() { }
		public Answer(int Id_, int TaskOptionId_, int ResultTaskId_, string Value_, bool Corect_)
		{
			this.Id = Id_;
			this.TaskOptionId = TaskOptionId_;
			this.ResultTaskId = ResultTaskId_;
			this.Value = Value_;
			this.Corect = Corect_;
		}
	}
}
