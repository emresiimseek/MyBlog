using FrameworkCore.CrossCuttingConcerns.Logging;
using FrameworkCore.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Aspect.PostSharp.ExpectionLogAspect
{
    [Serializable]
    public class ExpectionLogAspect : OnMethodBoundaryAspect
    {
        private LoggerService _loggerService;
        private Type _loggerType;
        public ExpectionLogAspect(Type loggerTyype)
        {
            _loggerType = loggerTyype;
        }
        public override void RuntimeInitialize(MethodBase method)
        { _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);

        }
        public override void OnException(MethodExecutionArgs args)
        {
            var logparameter = args.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name,
                Type = t.ParameterType.Name,
                Value = args.Arguments.GetArgument(i)
            }).ToList();
            var LogDedatil = new LogDetail
            {
                FullName = args.Method.DeclaringType.FullName == null ? null : args.Method.ReflectedType.FullName,
                MethodName = args.Method.Name,
                Parameters = logparameter
            };
            base.OnException(args);
            _loggerService.Debug(LogDedatil);
        }
    }
}
