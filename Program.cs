using Microsoft.EntityFrameworkCore;
using CodersCoffee.MinApi;
using CodersCoffee.MinApi.Service;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pedidos") ?? "Data Source=Pedidos.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddSqlite<PedidoDbContext>(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", builder =>
    {
        builder.AllowAnyOrigin();
    });
});
builder.Services.AddHttpClient();

var app = builder.Build();

await CreateDb(app.Services, app.Logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/pedidos/{id}", [AllowAnonymous] async (int id, IPedidoService pedidoService) =>
{
    var pedido = await pedidoService.GetByIdAsync(id);
    if (pedido == null)    
        return Results.NotFound();    

    return Results.Ok(pedido);
})
.WithName("Get Pedido by id")
.WithTags("Consulta");

app.MapGet("/pedidos", (IPedidoService pedidoService) =>
{
    return Results.Ok(pedidoService.Get());
})
.WithName("Get Pedidos")
.WithTags("Consulta");

app.MapPost("/pedidos", (Pedido pedido, IPedidoService pedidoService) =>
{
    var _pedido = pedidoService.Add(pedido);

    return Results.Created($"/pedidos/{_pedido.Id}", _pedido);
})
.WithName("Post Pedido");

app.MapPut("/pedidos/{id}", (int id, Pedido pedido, IPedidoService pedidoService) =>
{
    pedidoService.Update(id, pedido);

    return Results.NoContent();
})
.WithName("Update Pedido");

app.MapDelete("/pedidos/{id}", (int id, IPedidoService pedidoService) =>
{
    pedidoService.Delete(id);

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.WithName("Delete Pedido");

app.Run();

async Task CreateDb(IServiceProvider services, ILogger logger)
{
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<PedidoDbContext>();
    await db.Database.MigrateAsync();
}