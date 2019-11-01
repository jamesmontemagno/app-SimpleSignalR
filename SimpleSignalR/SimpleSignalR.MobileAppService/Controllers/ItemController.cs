using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SimpleSignalR.MobileAppService.Hubs;
using SimpleSignalR.Models;

namespace SimpleSignalR.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {

        private readonly IItemRepository ItemRepository;

        IHubContext<MessagesHub> Hub;
        Random random = new Random();
        public ItemController(IItemRepository itemRepository, IHubContext<MessagesHub> hub)
        {
            ItemRepository = itemRepository;
            Hub = hub;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public Item GetItem(string id)
        {
            Item item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                item.Description += " " + random.Next(1, 100).ToString();
                ItemRepository.Add(item);

                await Hub.Clients.All.SendAsync("NewItem", JsonConvert.SerializeObject(item));

            }
            catch (Exception ex)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ItemRepository.Remove(id);
        }
    }
}
