using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Data.Base
{
	public class BaseDbContext : DbContext
	{
		public BaseDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
