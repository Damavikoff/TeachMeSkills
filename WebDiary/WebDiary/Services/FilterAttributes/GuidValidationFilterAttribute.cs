using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebDiary.Models;

namespace WebDiary.Services.FilterAttributes
{
    public class GuidValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var guid = context.ActionArguments.SingleOrDefault(p => p.Value is Guid);
            if (guid.Value == null)
            {
                context.Result = new BadRequestObjectResult("Sending parameters is null or wrong!");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
