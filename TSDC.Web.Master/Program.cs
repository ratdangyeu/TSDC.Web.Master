using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using TSDC.ApiHelper.Models;
using TSDC.SharedMvc.Master.Infrastructure;
using TSDC.SharedMvc.Master.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(services => services.EnableEndpointRouting = false)
    .AddFluentValidation();
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MasterProfile());
});

IMapper mapper = new Mapper(mapperConfig);
builder.Services.AddSingleton(mapper);

builder.Services.Configure<ApiModel>(builder.Configuration.GetSection("Apis"));

builder.Services.AddTransient<IValidator<AuthenticateRequest>, AuthenticateRequestValidator>();
builder.Services.AddTransient<IValidator<UserModel>, UserValidator>();

builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
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

app.UseMvc(routes =>
{
    routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    routes.MapRoute("default", "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
