using Microsoft.VisualBasic;
using tk_web;
using tk_web.Domain.Models;
using tk_web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); //добавление контроллеров и представлений
builder.Services.AddMvc();


builder.Services.InitializeRepositories();
builder.Services.InitializeServices();
builder.Services.AddSingleton<ReportService>();


builder.Services.AddDbContext<TkEquipmentBdContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseDeveloperExceptionPage();

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles(); //подключаем поддержку статичных файлов (css, js и т.д.)

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Booking}/{action=ShowBookings}/{id?}");

app.Run();
