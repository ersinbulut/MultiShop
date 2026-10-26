using System.Reflection;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductDetailDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
// Eğer başka servisleriniz varsa onların using'lerini de buraya ekleyin:
// using MultiShop.Catalog.Services.ProductServices;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. VERİTABANI AYARLARI (appsettings.json'dan okuma)
// ============================================================
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// ============================================================
// 2. AUTOMAPPER AYARLARI
// ============================================================
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// ============================================================
// 3. SERVİS BAĞIMLILIKLARI (DEPENDENCY INJECTION)
// ============================================================
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

// ============================================================
// 4. CONTROLLER VE SWAGGER (OPENAPI) AYARLARI
// ============================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ============================================================
// 5. HTTP İSTEK HATTI (MIDDLEWARE PIPELINE)
// ============================================================
if (app.Environment.IsDevelopment())
{
    // Swagger JSON dökümanını oluşturur
    app.MapOpenApi();

    // Swagger UI arayüzünü etkinleştirir
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "MultiShop Catalog API v1");
    });
}

app.UseHttpsRedirection();

// Eğer JWT Token ile kimlik doğrulama kullanacaksanız buraya app.UseAuthentication(); eklenecek.
app.UseAuthorization();

app.MapControllers();

app.Run();