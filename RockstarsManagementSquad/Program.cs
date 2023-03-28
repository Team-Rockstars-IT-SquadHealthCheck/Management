using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services;
using RockstarsManagementSquad.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICompanyViewModelService, CompanyViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:6001"));
builder.Services.AddHttpClient<ISquadViewModelService, SquadViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:6001"));
builder.Services.AddHttpClient<IRockstarViewModelService, RockstarViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:6001"));
builder.Services.AddHttpClient<ISurveyViewModelService, SurveyViewModelService>(c =>
    c.BaseAddress = new Uri("https://localhost:6001"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
