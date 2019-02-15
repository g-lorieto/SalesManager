using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Models.Results
{
    public abstract class Result<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }        

        public virtual Result<T> TaskOk(T data = null, string message = "")
        {
            return new TaskOk<T>(data, message);            
        }
        public virtual Result<T> TaskError(T data = null, string message = "")
        {
            return new TaskError<T>(data, message);
        }
    }

    public class TaskError<T> : Result<T> where T : class
    {
        public TaskError(T data, string message)
        {
            Data = data;
            Message = message;
            IsSuccess = false;
        }
    }

    public class TaskOk<T> : Result<T> where T : class
    {
        public TaskOk(T data, string message)
        {
            Data = data;
            Message = message;
            IsSuccess = true;
        }
    }
}
