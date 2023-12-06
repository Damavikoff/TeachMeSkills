using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebDiary.Models;

namespace WebDiary.Services.FilterAttributes
{
    public class EventValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)//before, onactionexecuted - after
        {
            var eventModel = context.ActionArguments.SingleOrDefault(p => p.Value is EventDTO);
            if (eventModel.Value == null)
            {
                context.Result = new BadRequestObjectResult("Sending parameters is wrong!");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
