using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Dto
{
    public class ResultDto<T>
    {
        public ResultDto(T? data)
        {
            Data = data;
            IsSuccess = true;
        }

        public ResultDto(T? data, string message)
        {
            Data = data;
            Message = message;
            IsSuccess = true;
        }

        public ResultDto(bool isSuccess, T? data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public ResultDto(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
