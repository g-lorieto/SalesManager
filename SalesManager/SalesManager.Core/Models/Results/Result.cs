using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Models.Results
{
    public class Result<T>
    {
        public T Data  { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Result(T data, bool isSuccess, string message = "")
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
