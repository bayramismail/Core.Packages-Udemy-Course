﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ProblemDetailExtensions
{
    public static string AsJson<TProblemDetail>(this TProblemDetail details) 
        where TProblemDetail : ProblemDetails=>JsonSerializer.Serialize(details);
}
