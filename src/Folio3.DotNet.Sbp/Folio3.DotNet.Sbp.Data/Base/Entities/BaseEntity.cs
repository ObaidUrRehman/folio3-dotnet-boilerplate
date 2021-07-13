using System;
using System.ComponentModel.DataAnnotations;

namespace Folio3.DotNet.Sbp.Data.Base.Entities
{
	public abstract class BaseEntity
	{
        [Display(Name = "Revisions", AutoGenerateField = false), ScaffoldColumn(false)]
        public int Version { get; set; } = 1;

        [Display(Name = "Created", AutoGenerateField = false), ScaffoldColumn(false)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated", AutoGenerateField = false), ScaffoldColumn(false)]
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public void Touch()
        {
            Version++;
            Updated = DateTime.UtcNow;
        }
    }
}
