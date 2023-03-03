using System.Text;
using BooksMgmt.API;
using BooksMgmt.API.Controllers;
using BooksMgmt.API.Filters;
using BooksMgmt.API.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<LoggingFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ExceptionHandlingFilterAttribute>();

builder.Services.AddSingleton<InMemoryData>();

//builder.Services.AddProblemDetails();

builder.Services.AddAuthentication();
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    }).AddScheme<AuthenticationSchemeOptions, BasicAutheticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//else
//{
//    app.UseExceptionHandler("/error");
//}
//app.UseExceptionHandler(c => c.Run(async context =>
//{
//    var exception = context.Features
//        .Get<IExceptionHandlerPathFeature>()!
//        .Error;
//    var response = new { error = exception.Message };
//    await context.Response.WriteAsJsonAsync(response);
//}));
//}
//app.UseExceptionHandler();

// Inline exception handler


app.UseAuthorization();
//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
