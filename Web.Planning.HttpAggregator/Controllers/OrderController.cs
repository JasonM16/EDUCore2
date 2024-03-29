﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Shopping.HttpAggregator.Models;
using Web.Shopping.HttpAggregator.Services;

namespace Web.Shopping.HttpAggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBasketService _plannerService;
        private readonly IOrderApiClient _orderClient;

        public OrderController(IBasketService basketService, IOrderApiClient orderClient)
        {
            _plannerService = basketService;
            _orderClient = orderClient;
        }

        [Route("draft/{basketId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OrderData), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderData>> GetOrderDraftAsync(string basketId)
        {
            if (string.IsNullOrEmpty(basketId))
            {
                return BadRequest("Need a valid basketid");
            }
            // Get the basket data and build a order draft based on it
            var basket = await _plannerService.GetByIdAsync(basketId);

            if (basket == null)
            {
                return BadRequest($"No basket found for id {basketId}");
            }

            return await _orderClient.GetOrderDraftFromBasketAsync(basket);
        }
    }
}