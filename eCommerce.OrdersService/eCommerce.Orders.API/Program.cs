using System.Text.Json.Serialization;
using eCommerce.Orders.API.Logs;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBll(builder.Configuration);
builder.Services.AddDal(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRefitClient<IUsersMicroserviceClient>()
    .ConfigureHttpClient(c => { c.BaseAddress = new Uri(builder.Configuration["Api:Users"]!); })
    .AddHttpMessageHandler(() => new LoggingHandler());

builder.Services.AddRefitClient<IProductsMicroserviceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Api:Products"]!))
    .AddHttpMessageHandler(() => new LoggingHandler());


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x =>
    {
        x.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

await app.RunAsync();