using AutoMapper;
using Component.TestCompetion.DAL.Entity;
using Component.TestCompletion.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.TestCompletion.BLL.Impl
{
	internal class TestCompletionMapperProfile : Profile
	{
		public TestCompletionMapperProfile()
		{
			CreateMap<AttemptDto, Result>()
				.ForMember(r => r.ResultTasks, a => a.MapFrom(x => x.Tasks));
			CreateMap<Result, AttemptDto>()
				.ForMember(r => r.Tasks, a => a.MapFrom(x => x.ResultTasks));

			CreateMap<TaskDto, ResultTask>()
				.ForMember(t => t.TestTaskId, t => t.MapFrom(e => e.TaskId))
				.ForMember(t => t.ResultId, t => t.MapFrom(e => e.AttemptId));
			CreateMap<ResultTask, TaskDto>()
				.ForMember(t => t.TaskId, t => t.MapFrom(e => e.TestTaskId))
				.ForMember(t => t.AttemptId, t => t.MapFrom(e => e.ResultId));

			CreateMap<Answer, AnswerDto>().ForMember(t => t.TaskDtoId, t => t.MapFrom(e => e.ResultTaskId));
			CreateMap<AnswerDto, Answer>().ForMember(t => t.ResultTaskId, t => t.MapFrom(e => e.TaskDtoId));
		}
	}
}
