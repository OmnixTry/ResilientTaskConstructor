using Component.Groups.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Dto
{
	public class StudentResultDto
	{
		public GroupUserDto Student { get; set; }

		public AttemptDto Attempt { get; set; }
	}
}
