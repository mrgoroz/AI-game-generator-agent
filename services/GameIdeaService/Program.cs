using GameIdeaService.Application;
using GameIdeaService.Application.Consumers;
using GameIdeaService.Domain;
using GameIdeaService.Infrastructure;
using MassTransit;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Supabase
builder.Services.AddScoped<Supabase.Client>(provider =>
    new Supabase.Client(
        builder.Configuration["Supabase:Url"],
        builder.Configuration["Supabase:Key"],
        new Supabase.SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        }));

// Groq
builder.Services.AddHttpClient<IGameIdeaGenerator, GroqGameIdeaGenerator>();

// Repositories & Services
builder.Services.AddScoped<IGameIdeaRepository, SupabaseGameIdeaRepository>();
builder.Services.AddScoped<GameIdeaManager>();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TrendConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"] ?? "localhost", "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });

        cfg.ReceiveEndpoint("game-idea-service", e =>
        {
            e.ConfigureConsumer<TrendConsumer>(context);
        });
    });
});

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
