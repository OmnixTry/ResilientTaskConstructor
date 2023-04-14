using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL.Dto
{
	public class TestDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Topic { get; set; }
		public ICollection<TestTaskDto> Tasks { get; set; }
	}
}
