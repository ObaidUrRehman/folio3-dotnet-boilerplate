using System.Collections.Generic;

namespace Folio3.Sbp.Service.Common.Dto
{
    public class PagedResponseDto<T> : ResponseDto<IEnumerable<T>>
    {
        public PagedResponseDto()
        {
        }

        public PagedResponseDto(IEnumerable<T> result, bool success, string message, int page, int size, long total)
            : base(result, success, message)
        {
            PageNumber = page;
            PageSize = size;
            TotalRecords = total;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
    }
}