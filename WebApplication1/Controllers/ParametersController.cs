using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Parameter = Shared.Recipe.Parameter;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        [HttpGet]
        public IList<Parameter> GetAllParameters()
        {
            return new List<Parameter>
            {
                new Parameter { Id=1, RecipeId=1, Name="Parameter 1", Value= 134.1M},
                new Parameter { Id=2, RecipeId=2, Name= "Parameter Toto", Value= 342.0M},
                new Parameter { Id=3, RecipeId=3, Name = "Parameter 3", Value= 658.5M},
            };
        }
    }
}
