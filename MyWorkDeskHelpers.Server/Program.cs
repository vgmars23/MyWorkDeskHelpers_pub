using MongoDB.Driver;
using MyWorkDeskHelpers.Application.Interfaces;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Infrastructure.Configurations;
using MyWorkDeskHelpers.Server.Infrastructure.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("BirthdayReminderJob");

    q.AddJob<BirthdayReminderJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("BirthdayReminderJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(1) 
            .RepeatForever()));
});
builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);


builder.Services.AddScoped<BirthdayReminderJob>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
var mongoSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDbSettings>();
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoSettings.ConnectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoSettings.DatabaseName);
});

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IBirthdayService, BirthdayService>();
builder.Services.AddScoped<IUserContactService, UserContactService>();
builder.Services.AddScoped<INotificationPublisher, NotificationPublisher>();

builder.Services.AddHttpClient();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:50841")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var contactService = scope.ServiceProvider.GetRequiredService<IUserContactService>();
    await contactService.EnsureDefaultContactExistsAsync();
}



app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
