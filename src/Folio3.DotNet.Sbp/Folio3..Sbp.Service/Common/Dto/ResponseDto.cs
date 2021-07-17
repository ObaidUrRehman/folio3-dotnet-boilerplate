using System.Collections.Generic;
using System.Linq;

namespace Folio3.DotNet.Sbp.Service.Common.Dto
{
    public class ResponseDto
    {
        public ResponseDto(bool success = false, string message = null)
        {
            Message = message;
            Success = success;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string TraceId { get; set; }

        public static ResponseDto Successful()
        {
            return new ResponseDto {Success = true};
        }

        public static ResponseDto Failure(string message = null, IEnumerable<string> errors = null)
        {
            var result = new ResponseDto {Success = false, Message = message};
            if (errors != null)
                result.Errors = errors.ToList();
            return result;
        }

        public static ResponseDto<T> Failure<T>(string message = null)
        {
            return new ResponseDto<T> {Success = false, Message = message};
        }

        public static ResponseDto<T> Successful<T>(T result)
        {
            return new ResponseDto<T> {Result = result, Success = true};
        }
    }

    public class ResponseDto<T> : ResponseDto
    {
        public ResponseDto()
        {
        }

        public ResponseDto(T result, bool success = false, string message = null)
            : base(success, message)
        {
            Result = result;
        }

        public virtual T Result { get; set; }
    }
}