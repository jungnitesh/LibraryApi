using AutoMapper;
using LibraryAPI.Data.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
