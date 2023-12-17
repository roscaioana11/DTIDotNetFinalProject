﻿using System.ComponentModel.DataAnnotations;

namespace OnlineClassbook.Entities;

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
}
