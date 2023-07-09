using GiftFormAPI.Models;
using GiftFormAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildrenController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        public IActionResult GetChildren()
        {
            var children = _childService.GetAllChildren();
            return Ok(children);
        }

        [HttpPost]
        public IActionResult AddChild(Child child)
        {
            _childService.CreateChild(child);
            return Ok(child);
        }

        [HttpGet("{childId}")]
        public IActionResult GetChild(int childId)
        {
            var gifts = _childService.GetChild(childId);
            return Ok(gifts);
        }
    }
}