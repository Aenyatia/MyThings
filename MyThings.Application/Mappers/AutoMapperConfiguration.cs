using AutoMapper;
using MyThings.Application.Dtos;
using MyThings.Core.Domain;

namespace MyThings.Application.Mappers
{
	public static class AutoMapperConfiguration
	{
		public static IMapper Configure()
			=> new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Category, CategoryDto>();
				cfg.CreateMap<Task, TaskDto>();
			}).CreateMapper();
	}
}
