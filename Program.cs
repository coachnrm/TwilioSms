using _2FaSms.Services;
using Twilio.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<ITwilioRestClient, TwilioClient>();
builder.Services.AddScoped<OtpService>(provider => new OtpService("xxxx"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => 
{
    option.AddDefaultPolicy(
        Policy =>
        {
            Policy.AllowAnyOrigin();
        }
    );
});;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithMethods("GET, PATCH, DELETE, PUT, POST, OPTIONS"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
