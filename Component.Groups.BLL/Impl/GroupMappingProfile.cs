using AutoMapper;
using Component.Groups.BLL.Dto;
using Component.Groups.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Groups.BLL.Impl
{
	public class GroupMappingProfile : Profile
	{
		public GroupMappingProfile()
		{
			CreateMap<GroupDto, Group>();
			CreateMap<Group, GroupDto>();
			CreateMap<User, GroupUserDto>();
			CreateMap<GroupUserDto, User>();
			CreateMap<GroupStudent, GroupUserDto>();
			CreateMap<GroupUserDto, GroupStudent>();
		}
	}
}
