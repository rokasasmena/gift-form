using GiftFormAPI.Models;
using GiftFormAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;
        private readonly IChildService _childService;

        public GiftController(IGiftService giftService, IChildService childService)
        {
            _giftService = giftService;
            _childService = childService;
        }

        [HttpGet]
        public IActionResult GetAllGifts()
        {
            var gifts = _giftService.GetAllGifts();
            return Ok(gifts);
        }

        [HttpGet("{id}")]
        public IActionResult GetGiftById(int id)
        {
            var gift = _giftService.GetGiftById(id);
            if (gift == null)
            {
                return NotFound();
            }
            return Ok(gift);
        }

        [HttpGet("{childId}/child")]
        public async Task<IActionResult> GetGiftsForChild(int childId)
        {
            if ((await _childService.GetChild(childId)) == null) return NotFound();

            var gifts = _giftService.GetGiftsForChild(childId);

            return Ok(gifts);
        }

        [HttpPost]
        public IActionResult AddGift(Gift gift)
        {
            _giftService.CreateGift(gift);
            return Ok(gift);
        }

        [HttpGet("children")]
        public IActionResult GetChildren()
        {
            var children = _childService.GetAllChildren();
            return Ok(children);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGift(int id, Gift updatedGift)
        {
            var existingGift = await _giftService.GetGiftById(id);
            if (existingGift == null)
            {
                return NotFound();
            }

            existingGift.Name = updatedGift.Name;
            existingGift.ChildId = updatedGift.ChildId;
            await _giftService.UpdateGift(existingGift);

            return Ok(existingGift);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGift(int id)
        {
            var gift = _giftService.GetGiftById(id);
            if (gift == null)
            {
                return NotFound();
            }

            _giftService.DeleteGift(id);
            return Ok();
        }
    }
}