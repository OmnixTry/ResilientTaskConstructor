using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL.Dto
{
	public class OptionDto
	{
		public int Id { get; set; }
		public int TestTaskId { get; set; }
		public string Value { get; set; } 
		public bool? Correct { get; set; }
	}
}
