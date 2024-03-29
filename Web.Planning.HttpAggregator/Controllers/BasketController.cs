﻿using System;
using System.Linq;
using System.Net;
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
    public class BasketController : ControllerBase
    {
        private readonly ICatalogService _catalog;
        private readonly IBasketService _basket;

        public BasketController(ICatalogService catalogService, IBasketService plannerService)
        {
            _catalog = catalogService;
            _basket = plannerService;
        }

        [HttpPost]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BasketData), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketData>> UpdateAllBasketAsync([FromBody] UpdateBasketRequest data)
        {
            if (data.Items == null || !data.Items.Any())
            {
                return BadRequest("Need to pass at least one course.");
            }

            // Retrieve the current basket
            var basket = await _basket.GetByIdAsync(data.StudentId) ?? new BasketData(data.StudentId);

            var catalogItems = await _catalog.GetCatalogItemsAsync(data.Items.Select(x => x.ProductId));

            foreach (var bitem in data.Items)
            {
                var catalogItem = catalogItems.SingleOrDefault(ci => ci.Id == bitem.ProductId);
                if (catalogItem == null)
                {
                    return BadRequest($"Basket refers to a non-existing catalog item ({bitem.ProductId})");
                }

                basket.Items.Add(new BasketDataItem()
                {
                    Id = bitem.Id,
                    ProductId = catalogItem.Id.ToString(),
                    
                    
                });
            }

            await _basket.UpdateAsync(basket);

            return basket;
        }

        [HttpPut]
        [Route("courses")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BasketData), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketData>> UpdateQuantitiesAsync([FromBody] UpdateBasketItemsRequest data)
        {
            if (!data.Updates.Any())
            {
                return BadRequest("No updates sent");
            }

            // Retrieve the current basket
            var currentPlanner = await _basket.GetByIdAsync(data.CoursePlannerId);
            if (currentPlanner == null)
            {
                return BadRequest($"Course Planner with id {data.CoursePlannerId} not found.");
            }

            // Update with new quantities
            foreach (var update in data.Updates)
            {
                var course = currentPlanner.Items.SingleOrDefault(c => c.Id == update.CourseId);
                if (course == null)
                {
                    return BadRequest($"Course with id {update.CourseId} not found");
                }

                //TODO basketItem.Quantity = update.NewQty;
            }

            // Save the updated basket
            await _basket.UpdateAsync(currentPlanner);

            return currentPlanner;
        }

        [HttpPost]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCourseAsync([FromBody] AddBasketItemRequest data)
        {
            if (data == null )  //TODO  || data.Quantity == 0)
            {
                return BadRequest("Invalid payload");
            }

            // Step 1: Get the item from catalog
            var course = await _catalog.GetCatalogItemAsync(data.CatalogItemId);

            //item.PictureUri = 

            // Step 2: Get current basket status
            var currentBasket = (await _basket.GetByIdAsync(data.BasketId)) ?? new BasketData(data.BasketId);
            // Step 3: Merge current status with new product
            currentBasket.Items.Add(new BasketDataItem()
            {
                //UnitPrice = course.Price,
                //PictureUrl = course.PictureUri,
                ProductId = course.Id.ToString(),
                //ProductName = course.Name,
                //Quantity = data.Quantity,
                Id = Guid.NewGuid().ToString()
            });

            // Step 4: Update basket
            await _basket.UpdateAsync(currentBasket);

            return Ok();
        }
    }
}
