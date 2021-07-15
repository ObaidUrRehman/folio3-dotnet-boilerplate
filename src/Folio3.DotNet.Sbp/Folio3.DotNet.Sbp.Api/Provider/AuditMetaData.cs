using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Folio3.DotNet.Sbp.Data.AuditLogging;

namespace Folio3.DotNet.Sbp.Api.Provider
{
    public class AuditMetaData : IAuditMetaData
    {
        public string UserEmail { get; }
        public string UserName { get; }
    }
}
