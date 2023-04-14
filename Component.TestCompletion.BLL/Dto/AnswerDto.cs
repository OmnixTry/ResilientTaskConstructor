using Component.TestManagement.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Dto
{
	public class AnswerDto
	{
		public int Id { get; set; }
		public int? TaskOptionId { get; set; }
		public OptionDto Option { get; set; }
		public int TaskDtoId { get; set; }
		public string? Value { get; set; }
		public bool Corect { get; set; }
	}
}
