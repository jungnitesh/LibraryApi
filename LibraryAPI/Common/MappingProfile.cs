using AutoMapper;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<AddBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<BookLendDto, BookLend>().ReverseMap();
            CreateMap<ReviewDto, Review>().ReverseMap();
        }
    }
}
