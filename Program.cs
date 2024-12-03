using Microsoft.EntityFrameworkCore;

// Adicionando o DbContext e configurando a string de conexão
var builder = WebApplication.CreateBuilder(args);

// Defina a string de conexão diretamente na variável
var bancoDeDados = "Server=WP062692\\SQLEXPRESS,1433;Database=APICOREMAIN;Trusted_Connection=True;TrustServerCertificate=True;";

// Configure o DbContext para usar essa variável
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(bancoDeDados));

// Adicionando os serviços necessários
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configuração do CORS (permite todas as origens, métodos e cabeçalhos)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Criando o aplicativo
var app = builder.Build();

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.MapControllers();

// Exemplo de endpoint para previsão do tempo
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Rodando o aplicativo
app.Run();

// Classe de previsão do tempo (exemplo de endpoint)
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


