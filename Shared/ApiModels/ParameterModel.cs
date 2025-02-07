﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class ParameterModel
    {
        public int ID { get; set; }

        public int RecipeId { get; set; }

        public string Name { get; set; }

        public string UnitOfMeasurement { get; set; }

        public decimal Value { get; set; }
    }
}
