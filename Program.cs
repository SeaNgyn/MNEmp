using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebFormL1.BusinessLogic;
using WebFormL1.DataAccess.Data;
using WebFormL1.Filters;
using WebFormL1.Interface;
using WebFormL1.Service;
using WebFormL1.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddScoped<SessionAuthorizeFilter>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProvinceErrorViewModel>();
builder.Services.AddValidatorsFromAssemblyContaining<DistrictErrorViewModel>();
builder.Services.AddValidatorsFromAssemblyContaining<CommuneErrorViewModel>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBaseService<WebFormL1.Models.Employee>, BaseService<WebFormL1.Models.Employee>>();

builder.Services.AddScoped<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<ICommuneService, CommuneService>();
builder.Services.AddScoped<IDegreeService, DegreeService>();
builder.Services.AddScoped<RepositoryService>();
builder.Services.AddScoped<IProvinceBL,ProvinceBL>();
builder.Services.AddScoped<IDistrictBL,DistrictBL>();
builder.Services.AddScoped<IEmployeeBL,EmployeeBL>();
builder.Services.AddScoped<ICommuneBL,CommuneBL>();
builder.Services.AddScoped<IDegreeBL,DegreeBL>();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");

app.Run();
