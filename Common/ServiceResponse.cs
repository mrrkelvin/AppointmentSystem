using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new();
        public T? Data { get; set; }
        public int StatusCode { get; set; } = 200;

        public static ServiceResponse<T> Ok(T data) => new ServiceResponse<T> { Success = true, Data = data, StatusCode = 200 };
        public static ServiceResponse<T> Fail(int statusCode, params string[] errors)
            => new ServiceResponse<T> { Success = false, ErrorMessages = errors.ToList(), StatusCode = statusCode };

        public void AddError(string error, int statusCode = 400)
        {
            Success = false;
            StatusCode = statusCode;
            ErrorMessages.Add(error);
        }

        internal void AddError(object value)
        {
            throw new NotImplementedException();
        }
    }
}