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
    [Table("TaskOption")]
    [EfEntity(typeof(TestManagementContext))]
    public class TaskOption : IEntity
    {
        public int Id { get; set; }
        public int TestTaskId { get; set; }
        public TestTask TestTask { get; set; }
        public string Value { get; set; }
        public bool Correct { get; set; }
    }
}
