using Component.TestManagement.DAL.EF;
using Infrastructure.DAL;
using Infrastructure.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.DAL.Entity
{
    [Table("TestTask")]
    [EfEntity(typeof(TestManagementContext))]
        public class TestTask : IEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; } 
        public TaskType Type { get; set; }
        public AnswerType AnswerType { get; set; }
        public int Score { get; set; }
        public string Description { get; set; } 
        public string Question { get; set; }
        public int? GapIndex { get; set; }
        public string? GapText { get; set; }
        public bool AllowMultiple { get; set; }
        //public Test Test { get; set; }
        public ICollection<TaskOption> TaskOptions { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }
        //public ICollection<Answer> Answers { get; set; }
    }
}
