using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Service.Common.Dto
{
    public class PagedResponseDto<T> : ResponseDto<IEnumerable<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }

        public PagedResponseDto()
        { }

        public PagedResponseDto(IEnumerable<T> result, bool success, string message, int page, int size, long total)
            : base(result, success, message)
        {
            PageNumber = page;
            PageSize = size;
            TotalRecords = total;
        }
    }
}
