using GreenswampRazorPages;
using GreenswampRazorPages.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Настройки SMTP из appsettings.json
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseRouting();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/NotFound");
app.MapRazorPages();

app.Run();