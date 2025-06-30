using Microsoft.EntityFrameworkCore;
using NewsfeederApi.Data;
using NewsfeederApi.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; // CORS policy namn för att tillåta frontend-anrop

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<NewsfeederContext>(options =>
    options.UseSqlite("Data Source=newsfeeder.db"));
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SourceService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Vite dev server adress
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins); // Använd CORS-policyn

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

