using Microsoft.EntityFrameworkCore;
using CLDV6211_Part1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add ApplicationDbContext and connect it to SQL Server using the connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Seed sample data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Venues.Any())
    {
        var venue1 = new Venue
        {
            VenueName = "The Grand Hall",
            Location = "Sandton",
            Capacity = 500,
            ImageUrl = "https://via.placeholder.com/150"
        };

        var venue2 = new Venue
        {
            VenueName = "Skyline Rooftop",
            Location = "Cape Town",
            Capacity = 300,
            ImageUrl = "https://via.placeholder.com/150"
        };

        context.Venues.AddRange(venue1, venue2);
        context.SaveChanges();

        var event1 = new Event
        {
            EventName = "Tech Expo 2025",
            EventDate = new DateTime(2025, 8, 10),
            Description = "Technology trade event",
            VenueId = venue1.VenueId
        };

        var event2 = new Event
        {
            EventName = "Wedding Gala",
            EventDate = new DateTime(2025, 9, 15),
            Description = "Private wedding celebration",
            VenueId = venue2.VenueId
        };

        context.Events.AddRange(event1, event2);
        context.SaveChanges();

        var booking1 = new Booking
        {
            EventId = event1.EventId,
            VenueId = venue1.VenueId,
            BookingDate = new DateTime(2025, 7, 1)
        };

        var booking2 = new Booking
        {
            EventId = event2.EventId,
            VenueId = venue2.VenueId,
            BookingDate = new DateTime(2025, 8, 1)
        };

        context.Bookings.AddRange(booking1, booking2);
        context.SaveChanges();
    }
}

app.Run();
