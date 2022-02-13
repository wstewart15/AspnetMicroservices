﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordering.domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = String.Empty;
        public DateTime? LastModifiedDate { get; set; }
    }
}
