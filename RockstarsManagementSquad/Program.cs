using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services;
using RockstarsManagementSquad.Services.Interfaces;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RockstarsManagementSquad.Configuration;
using System;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Azure AD authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration);

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddMicrosoftIdentityUI();

// mail
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

// Add services to the container.
builder.Services.AddHttpClient<ICompanyViewModelService, CompanyViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));
builder.Services.AddHttpClient<ISquadViewModelService, SquadViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));
builder.Services.AddHttpClient<IRockstarViewModelService, RockstarViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));
builder.Services.AddHttpClient<ISurveyViewModelService, SurveyViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));
builder.Services.AddHttpClient<IUserViewModelService, UserViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));
builder.Services.AddHttpClient<IAnswerViewModelService, AnswerViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:7259"));

// SSL configuration

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 443, listenOptions =>
    {
        listenOptions.UseHttps("./certs/certificate.pem", "./certs/key.pem");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
