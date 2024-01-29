using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebDiary.Models;

namespace WebDiary.Services.FilterAttributes
{
    public class GroupValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)//before, onactionexecuted - after
        {
            var commentModel = context.ActionArguments.SingleOrDefault(p => p.Value is GroupViewModel);
            if (commentModel.Value == null)
            {
                context.Result = new BadRequestObjectResult("Sending parameters is null or wrong!");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState.ValidationState);
            }
        }
    }
}