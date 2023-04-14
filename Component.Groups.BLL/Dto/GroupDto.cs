using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.BLL.Dto
{
	public class GroupDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TeacherId { get; set; }
		public GroupUserDto Teacher { get; set; }
		public List<GroupTestDto> Tests { get; set; }
		public List<GroupUserDto> Students { get; set; }
	}
}
