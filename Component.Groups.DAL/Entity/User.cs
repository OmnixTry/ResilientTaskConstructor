using Component.Groups.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.DAL.Entity
{
	[Table("AspNetUsers", Schema = "dbo")]
	[EfEntity(typeof(GroupContext))]
	public class User: IEntity<string>
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	[Table("AspNetRoles", Schema = "dbo")]
	public class Role
	{
		string Id { get; set; }
		string Name { get; set; }

		ICollection<UserRole> UserRoles { get; set; }
	}

	[Table("AspNetUserRoles", Schema = "dbo")]
	public class UserRole
	{
		string UserId { get; set; }
		string RoleId { get; set; }

		User User { get; set; }
		Role Role { get; set; }
	}
}
