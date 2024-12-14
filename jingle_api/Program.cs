using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS - add this before your endpoints
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

string[] colors = { "red", "green", "yellow", "blue", "purple", "orange", "pink", "brown", "gray", "cyan" };
var random = new Random();
int x = 0;
var dateTimeSinceLastRequest = DateTime.Now;
app.MapGet("/api/circle", () =>
{
    string color = colors[random.Next(colors.Length)];
    string html = $"<div class='circle' style='background-color: {color};'></div>";
    //Console.WriteLine(color);
    x++;
    if (x % 1000 == 0)
    {        
        Console.WriteLine(x + " " + dateTimeSinceLastRequest);

        //print the delta time
        Console.WriteLine("Delta Time: " + (DateTime.Now - dateTimeSinceLastRequest).TotalSeconds);
        dateTimeSinceLastRequest = DateTime.Now;        
    }
    return Results.Content(html, "text/html");
})
.WithName("GetCircle");

app.Run();
