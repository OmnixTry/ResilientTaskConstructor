using Component.Groups.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.BLL.Contract
{
	public interface ISearchService
	{
		List<GroupUserDto> SearchUser(string email);
		List<GroupTestDto> SearchTest(string name, string topic);
	}
}
