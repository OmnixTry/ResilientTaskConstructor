using AutoMapper;
using Component.Groups.BLL.Contract;
using Component.Groups.BLL.Dto;
using Component.Groups.DAL.Contract;
using Component.TestManagement.DAL.Contract;
using Component.TestManagement.DAL.Entity;
using Infrastructure.DAL.Contract;
using Infrastructure.DAL.Entity;

namespace Component.Groups.BLL.Impl
{
	internal class SearchService : ISearchService
	{
		private readonly IGroupUnitOfWork unitOfWork;
		private readonly ITestMgmtUnitOfWork testMgmtUnitOfWork;
		private readonly IMapper mapper;
		private readonly IUserProvider userProvider;

		public SearchService(IGroupUnitOfWork unitOfWork, 
			ITestMgmtUnitOfWork testMgmtUnitOfWork, 
			IMapper mapper, 
			IUserProvider userProvider)
		{
			this.unitOfWork = unitOfWork;
			this.testMgmtUnitOfWork = testMgmtUnitOfWork;
			this.mapper = mapper;
			this.userProvider = userProvider;
		}

		public List<GroupUserDto> SearchUser(string email)
		{
			var users = unitOfWork.UserRepository.ReadProcedure("[dbo].[SearchStudents] @p0", email);
			return mapper.Map<List<GroupUserDto>>(users);
		}

		public List<GroupTestDto> SearchTest(string name, string topic)
		{
			var filter = new Filter<Test, int>();
			filter.Take = 50;
			if(name != null)
			{
				filter.AddFilter(u => u.Name.Contains(name));
			}
			if(topic != null)
			{
				filter.AddFilter(u => u.Topic.Contains(topic));
			}

			var teacherId = userProvider.GetUserId();
			filter.AddFilter(u => u.UserId == teacherId);		

			var tests = testMgmtUnitOfWork.TestRepository.GetAll(filter);
			var mappedTests = tests.Select(t => new GroupTestDto { Id = t.Id, Name = t.Name, Topic = t.Topic }).ToList();
			return mappedTests;
		}
	}
}
