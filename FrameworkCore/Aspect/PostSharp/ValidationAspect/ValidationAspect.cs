using FluentValidation;
using FrameworkCore.CrossCuttingConcerns.Validation.Fluent;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Aspect.PostSharp.ValidationAspect
{
    [Serializable]
    public class ValidationAspect : OnMethodBoundaryAspect
    {
        private Type _validatiorType;
        public ValidationAspect(Type validatiorType)
        {
            _validatiorType = validatiorType;
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            var validatior = (IValidator)Activator.CreateInstance(_validatiorType);
            var entityType = _validatiorType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(t => t.GetType() == entityType).ToList();
            foreach (var entity in entities)
            {
                ValidatiorTool.FluentValidate(validatior, entities);
            }
            base.OnEntry(args);
        }
    }
}
