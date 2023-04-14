using Component.Groups.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Component.Groups.DAL.Entity
{
	[Table("Group")]
	[EfEntity(typeof(GroupContext))]
	public class Group: IEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TeacherId { get; set; }
		public User Teacher { get; set; }
		public ICollection<GroupTest> Tests { get; set; }
		public ICollection<GroupStudent> GroupStudents { get; set; }

		public Group() { }
		public Group(int Id_, string Name_, string TeacherId_)
		{
			this.Id = Id_;
			this.Name = Name_;
			this.TeacherId = TeacherId_;
		}
	}
}
