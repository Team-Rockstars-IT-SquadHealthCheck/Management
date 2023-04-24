using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services;
using RockstarsManagementSquad.Services.Interfaces;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RockstarsManagementSquad.Configuration;


var builder = WebApplication.CreateBuilder(args);


//Azure AD authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(builder.Configuration);

//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});


builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddMicrosoftIdentityUI();
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
//====================================
//BIJ LOCALHOST MOET DE LOCAL API PORT
//====================================



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();