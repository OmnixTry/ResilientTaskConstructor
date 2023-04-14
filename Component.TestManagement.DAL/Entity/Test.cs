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
    [Table("Test")]
    [EfEntity(typeof(TestManagementContext))]
    public class Test : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string UserId { get; set; }
        public ICollection<TestTask> Tasks { get; set; }
	}
}
