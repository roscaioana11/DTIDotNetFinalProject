using OnlineClassbook.DTOs;
using OnlineClassbook.Entities;

namespace OnlineClassbook;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Student, StudentToGetDTO>();
        CreateMap<StudentToCreateDTO, Student>();
        // CreateMap<AddressToUpdateDTO, Address>();
    }
    
}