using MessengerApp.Hubs;
using System.Reflection.Emit;

namespace MessengerApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSignalR();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


            // Server Side:

            //Think of the server as a house with rooms for different conversations.
            //The line app.MapHub<ChatHub>("/chat"); is like putting a sign on the door of a room labeled "Chat" in the house.
            // See the chat.js file >> var connection = hubBuilder.withUrl("/chat").build();

            app.MapHub<ChatHub>("/chat");

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Chat}/{id?}");


			



			app.Run();
		}
	}
}
