using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

// ∆Ù”√ Cors 
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(opt =>
        opt.AddDefaultPolicy(b =>
            b.WithOrigins(urls)
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
        )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ∆Ù”√ CORS
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<MyHub>("/Hubs/MyHub");

app.MapControllers();

app.Run();
