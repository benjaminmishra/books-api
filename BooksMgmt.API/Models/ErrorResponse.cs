﻿namespace BooksMgmt.API.Models;

public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public DateTime Date { get; set; }
}

