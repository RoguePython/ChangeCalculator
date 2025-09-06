using ChangeCalculator.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS for your portfolio site
const string FrontendCors = "FrontendCors";
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(FrontendCors, p =>
        p.WithOrigins("https://benjaminjoubert.co.za", "https://www.benjaminjoubert.co.za")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChangeCalculatorService, ChangeCalculatorService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(FrontendCors);
app.MapControllers();

app.Run();
