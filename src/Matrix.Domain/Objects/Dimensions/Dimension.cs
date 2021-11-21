﻿using Matrix.Common.Domain;
using System.Collections.Generic;

namespace Matrix.Domain.Objects
{
    public class Dimension : Entity
    {
        public string Name { get; set; }

        public ICollection<FactDimension> FactDimensions { get; set; }
    }
}