using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Models
{
    public abstract class AggregateRoot: Entity
    {
        public bool IsActive { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        [StringLength(50)]
        public string LastModifiedBy { get; set; }
    }
}
