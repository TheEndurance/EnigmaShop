using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EnigmaShop.QueryConstraint
{
    public class RequiredQueryAttribute : FromQueryAttribute, IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.Action.Selectors != null && parameter.Action.Selectors.Any())
            {
                parameter.Action.Selectors.Last().ActionConstraints.Add(
                    new RequiredQueryActionConstraint(parameter.BindingInfo?.BinderModelName ??
                                                      parameter.ParameterName));
            }
        }
    }
}
