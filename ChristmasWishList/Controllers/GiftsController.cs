using ChristmasWishList.BusinessLayer.Services.Interfaces;
using ChristmasWishList.Shared.Requests;
using ChristmasWishList.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasWishList.Controllers
{
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService giftService;
        public GiftsController(IGiftService giftService)
        {
            this.giftService = giftService;
        }

        /// <summary>
        /// Get the gifts list
        /// </summary>
        /// <response code="200">The gits list</response>
        [HttpGet]
        [ProducesResponseType(typeof(GiftResponse), StatusCodes.Status200OK)]
        public ActionResult<GiftResponse> GetGift()
        {
            var result = giftService.Get();
            return result;
        }



        /// <summary>
        /// Get a specific gift
        /// </summary>
        /// <param name="id"> Id of the gift to retrive</param>
        /// <response code="200">The desired gift</response>
        /// <response code="404">Gift not found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Gift), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Gift), StatusCodes.Status404NotFound)]
        public ActionResult<Gift> GetGiftById(Guid id)
        {
            var response = giftService.GetGift(id);

            if (response is not null)
            {
                return Ok(response);
            }

            return NotFound();
        }

        /// <summary>
        /// Create a new gift
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Gift created successfully</response>
        /// <response code="400">Unable to create the gift due to validation error</response>
        [HttpPost]
        [ProducesResponseType(typeof(Gift),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Gift> CreateGift(SaveGiftRequest request)
        {
            var response = giftService.CreateGift(request);
            
            return CreatedAtAction(
                actionName: nameof(GetGift),
                routeValues: new {id = response.Id},
                value: request);
        }

        /// <summary>
        /// Update a gift with specific id
        /// </summary>
        /// <param name="id">Id of gift to edit</param>
        /// <param name="request"></param>
        /// <response code="204">The gift updated successfully</response>
        /// <response code="404">The gift to update was not found</response>
        /// <response code="400">Unable to edit the gift due to validation error</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Gift), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Gift), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Gift), StatusCodes.Status400BadRequest)]
        public ActionResult<Gift> UpdateGift(Guid id,SaveGiftRequest request)
        {
            var update = giftService.GetGift(id);

            if (update is null)
            {
                return NotFound();
            }
            
            giftService.UpdateGifit(id,request);
            return NoContent();

        }

        /// <summary>
        /// Delete a gift with a specific Id
        /// </summary>
        /// <param name="id">Id of the gift to delete</param>
        /// <response code="204">The gift was successfully deleted</response>
        /// <response code="404">Gift not found</response>
        /// <response code="400">The client’s request is not valid based on the server’s input validations.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(GiftResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Gift> DeleteGift(Guid id)
        {
            var gift = giftService.GetGift(id);
            
            if (gift is null)
            {
                return NotFound();
            }
            
            giftService.DeleteGift(id);
            return NoContent();
        }


        /// <summary>
        /// Delete all gift
        /// </summary>
        /// <response code="204">The list gift was successfully deleted</response>
        /// <response code="400">The client’s request is not valid based on the server’s input validations.</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteListGift()
        {
            giftService.DeleteGiftList();
            return NoContent();
        }
    }
}
