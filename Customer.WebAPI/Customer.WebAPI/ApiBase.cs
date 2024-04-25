using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Customer.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiBase : ControllerBase
    {
        protected void Validate<T>(T Dto)
        {
            string assemblyQualifiedName = $"{typeof(T).Namespace}.{typeof(T).Name}Validator, {typeof(T).Assembly}";

            Type type = Type.GetType(assemblyQualifiedName);

            if (type == null)
            {
                return;
            }

            var validator = (IValidator<T>)Activator.CreateInstance(type, GetArgs(type.GetConstructors()[0]));

            var context = new ValidationContext<T>(Dto);

            var result = validator.Validate(context);

            if (!result.IsValid)
            {
                throw new ArgumentNullException(string.Join(" ", result.Errors));
            }
        }

        private object[] GetArgs(ConstructorInfo constructor)
        {
            var args = new List<object>();

            var parameters = constructor?.GetParameters();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    args.Add(HttpContext.RequestServices.GetService(Type.GetType(parameter.ParameterType.AssemblyQualifiedName)));
                }
            }

            return args.ToArray();
        }
    }
}