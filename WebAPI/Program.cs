using Application;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using FileData;
using FileData.DAOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7084")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserFileDao>();
builder.Services.AddScoped<IUserLogic, Logic>();
builder.Services.AddScoped<ITodoDao, TodoFileDao>();
builder.Services.AddScoped<ITodoLogic, TodoLogic>();
builder.Services.AddScoped(
    sp =>
        new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7084")
        }
);

var app = builder.Build();

app.UseCors("AllowBlazorClient");

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

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