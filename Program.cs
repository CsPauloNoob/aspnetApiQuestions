using Microsoft.EntityFrameworkCore;
using question_api.Data; // Certifique-se de usar o namespace correto para AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o Entity Framework com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Database/app.db"));

var app = builder.Build();

// Criar banco de dados automaticamente (opcional, mas recomendado para desenvolvimento)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Garante que o banco de dados ser� criado se n�o existir
}

// Configura��o de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Iniciar a aplica��o
app.Run();
