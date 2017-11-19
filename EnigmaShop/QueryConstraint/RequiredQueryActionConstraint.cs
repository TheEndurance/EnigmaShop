using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace EnigmaShop.QueryConstraint
{
    public class RequiredQueryActionConstraint : IActionConstraint
    {
        private readonly string _parameter;

        public RequiredQueryActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        //High order so our constraint runs last after the framework built in constraints
        public int Order { get; } = 999;

        public bool Accept(ActionConstraintContext context)
        {
            if (context.RouteContext.HttpContext.Request.Query.ContainsKey(_parameter))
            {
                return true;
            }
            return false;
        }

       
    }
}
