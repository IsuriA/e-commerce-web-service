using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.model.Models;
using e_commerce_web.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddDbContext<ECommerceDbContext>(options =>
   options.UseSqlServer(connectionString));

builder.Services.AddScoped<InquiryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<LookupService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<LookupDataManager>();
builder.Services.AddScoped<InquiryDataManager>();
builder.Services.AddScoped<ProductDataManager>();
builder.Services.AddScoped<OrderDataManager>();
builder.Services.AddScoped<UserDataManager>();

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseCors(options =>
                options.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var contentRoot = builder.Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, "Uploads")),
    RequestPath = "/assets"
});

app.UseAuthorization();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();

app.UseStaticFiles();