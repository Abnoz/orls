using System.Reflection;
using Orleans.Configuration;
using OrleansDemo.Components;
using OrleansDemo.Services;
using OrleansDemo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Host
    .UseOrleansClient(client =>
    {
        client
            
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "zozzo";
            })
            .UseAdoNetClustering(options =>
            {
                options.ConnectionString = "Server=127.0.0.1,1433;Database=Orleansdb;User Id=sa;Password=P@ssw0rd;";
                options.Invariant = "System.Data.SqlClient";
            });
    })
    .ConfigureLogging(logging => logging.AddConsole());
// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();