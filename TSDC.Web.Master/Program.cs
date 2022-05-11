using AutoMapper;
using FluentValidation.AspNetCore;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using TSDC.SharedMvc.Master.Infrastructure;

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

builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly());

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

app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    routes.MapRoute("default", "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
