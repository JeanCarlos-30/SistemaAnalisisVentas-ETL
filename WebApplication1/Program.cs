using SistemaAnalisisVentas.IoC;

var builder = WebApplication.CreateBuilder(args);

// ===============================================
// 1. REGISTRAR SERVICIOS DEL PROYECTO (IoC)
// ===============================================
builder.Services.AddSistemaAnalisisVentas(builder.Configuration);

// ===============================================
// 2. AGREGAR CONTROLLERS Y SWAGGER
// ===============================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===============================================
// 3. CONFIGURAR SWAGGER
// ===============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ===============================================
// 4. MIDDLEWARES
// ===============================================
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ===============================================
// 5. EJECUCIÓN
// ===============================================
app.Run();
