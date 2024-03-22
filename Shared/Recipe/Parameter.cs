using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Recipe
{
    public class Parameter
    {
        public int Id { get; set; }

        public int RecipeId {  get; set; }

        public string Name { get; set; }

        public string Mesure { get; set; }

        public decimal Value { get; set; }
    }
}
