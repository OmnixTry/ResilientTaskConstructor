using AutoMapper;
using Component.TestManagement.BLL.Dto;
using Component.TestManagement.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestManagement.BLL.Impl
{
	public class TestMgmtMapperProfile : Profile
	{
		public TestMgmtMapperProfile()
		{
			CreateMap<TestDto, Test>();
			CreateMap<Test, TestDto>();
			CreateMap<TestTask, TestTaskDto>();
			CreateMap<TestTaskDto, TestTask>();
			CreateMap<OptionDto, TaskOption>();
			CreateMap<TaskOption, OptionDto>();
			CreateMap<AnswerDto, Answer>();
			CreateMap<Answer, AnswerDto>();
		}
	}
}
