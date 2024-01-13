using DemoWebHookBot.Models;
using DemoWebHookBot.Services;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var botConfig = builder.Configuration.GetSection("BotConfiguration")
    .Get<BotConfiguration>();

builder.Services.AddHostedService<ConfigureWebhook>();

builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient
        => new TelegramBotClient(botConfig.Token, httpClient));

builder.Services.AddScoped<HandleUpdateService>();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
    {
        var token = botConfig.Token;

        endpoints.MapControllerRoute(
            name: "tgwebhook",
            pattern: $"bot/{token}",
            new { controller = "Webhook", action = "Post" }
            );

        endpoints.MapControllers();
    });

app.Run();