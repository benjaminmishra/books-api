﻿using System;

namespace BooksMgmt.Data.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }
}
