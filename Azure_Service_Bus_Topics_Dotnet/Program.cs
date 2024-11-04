using Azure_Service_Bus_Topics_Dotnet;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITopicClient>(serviceProvider => new TopicClient(
    connectionString: builder.Configuration.GetValue<string>("servicebus:connectionstring"),
    entityPath: builder.Configuration.GetValue<string>("serviceBus:topicname")));
builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<ISubscriptionClient>(serviceProvider => new SubscriptionClient(
connectionString: "<ConnectionString Here>",
topicPath: "<Topic Name here>", subscriptionName: "ProductSubscription"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
