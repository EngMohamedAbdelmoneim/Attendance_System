using AttendanceSystem.Models;
using AttendanceSystem.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace AttendanceSystem
{
	public class Program
	{
		public static void Main(string[] args)
		{	
			var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AttendanceSystemContext>(
               a => a.UseSqlServer("Data Source=DESKTOP-JGVAK8U\\SQLEXPRESS;Initial Catalog=AttendanceSystem;Integrated Security=True;TrustServerCertificate=True;Encrypt=true"));
            // Add services to the container.

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(a =>
            {
				a.LoginPath = "/Account/Login";
				a.AccessDeniedPath = "/Account/Login";

            });
            builder.Services.AddControllersWithViews()	;

            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


            builder.Services.AddScoped<IServices<Student>, StudentService>();
            builder.Services.AddScoped<IServices<Department>, DepartmentService>();
            builder.Services.AddScoped<IServices<Hr>, HrService>();
            builder.Services.AddScoped<IServices<Intakes>, IntakeService>();
            builder.Services.AddScoped<IServices<Programs>, ProgramService>();
			builder.Services.AddScoped<IServices<Attendance>, AttendanceService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddSession();
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
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
