using Component.Groups.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Component.Groups.DAL.Entity
{
	[Table("GroupStudent")]
	[EfEntity(typeof(GroupContext))]
	public class GroupStudent : IEntity<int>
	{
		public int Id { get; set; }
		public int GroupId { get; set; }
		public string StudentId { get; set; }
		public User Student { get; set; }

		public GroupStudent() { }
		public GroupStudent(int Id_, int GroupId_, string StudentId_)
		{
			this.Id = Id_;
			this.GroupId = GroupId_;
			this.StudentId = StudentId_;
		}
	}
}
