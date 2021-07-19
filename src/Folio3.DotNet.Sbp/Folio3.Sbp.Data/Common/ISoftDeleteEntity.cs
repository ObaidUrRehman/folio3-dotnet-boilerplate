using System;

namespace Folio3.Sbp.Data.Common
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}
