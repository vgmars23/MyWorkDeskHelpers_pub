using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Infrastructure.Telegram;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<INotificationHandler, NotificationHandler>(); 
builder.Services.AddTransient<INotificationConsumer, NotificationConsumer>(); 


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:50841") 
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var consumer = scope.ServiceProvider.GetRequiredService<INotificationConsumer>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); 

app.UseAuthorization();

app.MapControllers();

app.Run();
