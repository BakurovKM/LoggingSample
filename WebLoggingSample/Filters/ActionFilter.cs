using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebLoggingSample.Attributes;

namespace WebLoggingSample.Filters
{
    public class ActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<ActionFilter> _logger;

        public ActionFilter(ILogger<ActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"OnActionExecuting {DateTime.Now:dd/MM/yyyy hh-mm-ss}");
            _logger.LogInformation(
                $"{context.HttpContext.Request.Scheme} {context.HttpContext.Request.Host}{context.HttpContext.Request.Path} {context.HttpContext.Request.QueryString} {DateTime.Now:dd/MM/yyyy hh-mm-ss}");

            foreach (var param in context.ActionArguments)
            {
                var argType = param.Value.GetType();
                var loglessAttr = argType.GetCustomAttribute<LoglessAttribute>();
                if (loglessAttr != null)
                {
                    var props = argType.GetProperties()
                        .Where(_ => !loglessAttr.Properties.Contains(_.Name))
                        .ToArray();

                    var logArg =
                        $"{{\"{param.Key}\":\"{{{string.Join(",", props.Select(p => $"\"{p.Name}\":\"{p.GetValue(param.Value)}\""))}\"}}}}";
                    _logger.LogInformation(logArg);
                }
                else
                {
                    _logger.LogInformation(JsonConvert.SerializeObject(param));
                }
            }

            await next();
            
            _logger.LogInformation(
                $"OnActionExecuted Status:{context.HttpContext.Response.StatusCode} {DateTime.Now:dd/MM/yyyy hh-mm-ss}");
        }
    }
}
