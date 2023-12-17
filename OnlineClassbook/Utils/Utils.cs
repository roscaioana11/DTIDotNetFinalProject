using OnlineClassbook.DTOs;
using OnlineClassbook.Entities;

namespace OnlineClassbook.Utils;

public static class Utils
{
    public static Address ToEntity(this AddressToUpdateDTO updateStudentAddress)
    {
        if (updateStudentAddress == null)
            return null;

        return new Address
        {
            Number = updateStudentAddress.Number,
            City = updateStudentAddress.City,
            Street = updateStudentAddress.Street
        };
    }
}