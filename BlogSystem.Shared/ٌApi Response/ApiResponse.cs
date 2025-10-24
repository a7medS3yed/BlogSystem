﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Shared._ٌApi_Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, string? message = null, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
            => new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> FailResponse(string message)
            => new ApiResponse<T>(false, message, default);
    }
}
