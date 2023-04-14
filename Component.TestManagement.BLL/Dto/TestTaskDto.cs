using Component.TestManagement.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL.Dto
{
	public class TestTaskDto
	{
        public int Id { get; set; }
        public int TestId { get; set; }
        public TaskType Type { get; set; }
        public AnswerType AnswerType { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public string Question { get; set; }
        public int? GapIndex { get; set; }
        public string GapText { get; set; }
        public bool Correct { get; set; }
        public bool AllowMultiple { get; set; }
        public ICollection<OptionDto> TaskOptions { get; set; }
    }
}
