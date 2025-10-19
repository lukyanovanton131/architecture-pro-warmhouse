using DeviceControlApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/command", (CommandRequest cmd) =>
{
    if (cmd.Value > 0.5)
    {
        return  Results.Ok(new CommandResponse()
        {
            RelayId = cmd.RelayId,
            Value = cmd.Value,
            Status = "faild",
            Timestamp = DateTime.Now
        });
    }
    return Results.Ok(new CommandResponse()
    {
        RelayId = cmd.RelayId,
        Value = cmd.Value,
        Status = "success",
        Timestamp = DateTime.Now
    });
});

app.Run();