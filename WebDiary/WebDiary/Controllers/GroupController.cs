using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Services.Interfaces;

namespace WebDiary.Controllers
{
    public class GroupController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult ShowpopUp()
        {
            //specify the name or path of the partial view
            return PartialView("_GroupPartial");
        }
    }
}
