using Component.Groups.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Component.Groups.DAL.Entity
{
	[Table("GroupTest")]
	[EfEntity(typeof(GroupContext))]
	public class GroupTest : IEntity<int>
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public int TestId { get; set; }

		public GroupTest() { }
		public GroupTest(int Id_, int GroupId_, int TestId_)
		{
			this.Id = Id_;
			this.GroupId = GroupId_;
			this.TestId = TestId_;
		}
	}
}
