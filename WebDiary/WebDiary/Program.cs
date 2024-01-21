using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Services;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;
using WebDiary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebDiaryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")).EnableSensitiveDataLogging());

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddEntityFrameworkStores<WebDiaryContext>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(EventDTOMappingProfile), typeof(EventViewModelMappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Event/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Event}/{action=Index}/");

app.Run();