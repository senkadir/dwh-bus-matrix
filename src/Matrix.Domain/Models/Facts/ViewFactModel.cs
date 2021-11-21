﻿using System;
using System.Collections.Generic;

namespace Matrix.Domain.Models
{
    public class ViewFactModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ViewDimensionModel> Dimensions { get; set; }
    }
}