using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrameworkCore.CrossCuttingConcerns.Validation.Fluent
{
    public class ValidatiorTool
    {
        public static void FluentValidate(IValidator validator, object Entity)
        {
            var result = validator.Validate(Entity);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);

            }
        }
    }
}
