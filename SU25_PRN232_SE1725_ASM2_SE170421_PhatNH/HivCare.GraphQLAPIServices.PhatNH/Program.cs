using HivCare.GraphQLAPIServices.PhatNH.GraphQLs;
using SmokeQuit.GraphQLAPIServices.LocDPX.GraphQLs;
using SmokeQuit.Services.LocDPX;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuration GraphQL
builder.Services.AddGraphQLServer()
    .AddQueryType<Queries>()
    .AddMutationType<Mutations>()
    .BindRuntimeType<DateTime, DateTimeType>();

builder.Services.AddScoped<IServiceProviders, ServiceProviders>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();
app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
app.UseAuthorization();

app.MapControllers();

app.Run();