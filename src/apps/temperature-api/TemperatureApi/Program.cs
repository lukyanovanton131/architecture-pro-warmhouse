var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/temperature", (string location) =>
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return Results.BadRequest("Location parameter is required");
        }
        var random = new Random();
        var temperature = random.Next(-50, 50); // Диапазон от -20°C до 40°C
    
        return Results.Ok(temperature);
    })
    .WithName("GetTemperature")
    .WithOpenApi();

app.Run();

