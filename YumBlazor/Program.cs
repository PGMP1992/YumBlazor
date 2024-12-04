using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Stripe;
using System.Globalization;
using YumBlazor.Components;
using YumBlazor.Components.Account;
using YumBlazor.Data;
using YumBlazor.Repos.Implementation;
using YumBlazor.Repos.Interfaces;
using YumBlazor.Services;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("se-SE");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("se-SE");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//DbSets ---------------------
builder.Services.AddScoped<ICategoryRepos, CategoryRepos>();
builder.Services.AddScoped<IProductRepos, ProductRepos>();
builder.Services.AddScoped<IShoppingCartRepos, ShoppingCartRepos>();
builder.Services.AddScoped<IOrderRepos, OrderRepos>();
// Utilities - Services 
builder.Services.AddScoped<ICommon, Common>();
builder.Services.AddSingleton<SharedStateService>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddRadzenComponents();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    // .AddFacebook(options =>
    // {
    //     options.AppId = "1160092848406864";
    //     options.AppSecret = "a5313a1337e6d9f137b37ed2a4f7cc45";
    // })
    //.AddMicrosoftAccount(options =>
    //{
    //    options.ClientId = "0b170a00-4d7f-4eaf-8235-523dddedfec7";
    //    options.ClientSecret = "2Na8Q~y8yNpt9iB0kZTyJj22fojeyzsrCbQPma0D";
    //})
    //.AddGoogle(options =>
    //{
    //    options.ClientId = "602908572279-8dcb674plmqg25evmb0c20fo2naru06u.apps.googleusercontent.com";
    //    options.ClientSecret = "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";
    //})
.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Added
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeApiKey").Value;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
