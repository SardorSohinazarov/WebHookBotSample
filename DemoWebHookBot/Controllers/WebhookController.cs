using DemoWebHookBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace DemoWebHookBot.Controllers
{
    public class WebhookController : ControllerBase
    {
        private readonly HandleUpdateService _updateService;

        public WebhookController(HandleUpdateService updateService)
            => _updateService = updateService;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            await _updateService.HandleUpdateAsync(update);

            return Ok();
        }

    }
}
