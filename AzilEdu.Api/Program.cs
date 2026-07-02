using AzilEdu.Api.Data;
using Microsoft.EntityFrameworkCore;
using AzilEdu.Shared.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AzilEduDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AzilEduDbContext>();

    await db.Database.MigrateAsync();

    if (!await db.Animals.AnyAsync())
    {
        db.Animals.AddRange(
            new Animal
            {
                Name = "Luna",
                Species = "Pas",
                Breed = "Labrador",
                Gender = "Ženka",
                Age = 3,
                ArrivalDate = new DateTime(2025, 10, 12),
                IsAdopted = false,
                ImageUrl = "/images/animals/luna.webp",
                Description = "Mirna i druželjubiva kujica koja voli šetnje."
            },
            new Animal
            {
                Name = "Maza",
                Species = "Mačka",
                Breed = "Domaća kratkodlaka",
                Gender = "Ženka",
                Age = 2,
                ArrivalDate = new DateTime(2025, 11, 5),
                IsAdopted = true,
                ImageUrl = "/images/animals/maza.webp",
                Description = "Zaigrana mačka naviknuta na boravak u zatvorenom prostoru."
            },
            new Animal
            {
                Name = "Rex",
                Species = "Pas",
                Breed = "Njemački ovčar",
                Gender = "Mužjak",
                Age = 5,
                ArrivalDate = new DateTime(2026, 1, 20),
                IsAdopted = false,
                ImageUrl = "/images/animals/rex.webp",
                Description = "Aktivan pas koji traži iskusnijeg vlasnika."
            },
            new Animal
            {
                Name = "Nala",
                Species = "Mačka",
                Breed = "Maine Coon mješanac",
                Gender = "Ženka",
                Age = null,
                ArrivalDate = new DateTime(2026, 2, 3),
                IsAdopted = false,
                ImageUrl = "/images/animals/nala.webp",
                Description = "Mlada mačka pronađena bez poznate povijesti."
            },
            new Animal
            {
                Name = "Tobi",
                Species = "Pas",
                Breed = "Mješanac",
                Gender = "Mužjak",
                Age = 1,
                ArrivalDate = null,
                IsAdopted = false,
                ImageUrl = "/images/animals/tobi.webp",
                Description = "Vesel pas kojem datum dolaska još nije potvrđen."
            },
            new Animal
            {
                Name = "Bruno",
                Species = "Pas",
                Breed = "Bigl",
                Gender = "Mužjak",
                Age = 4,
                ArrivalDate = new DateTime(2025, 9, 18),
                IsAdopted = true,
                ImageUrl = "/images/animals/bruno.webp",
                Description = "Udomljen pas koji ostaje u evidenciji azila."
            }
        );

        await db.SaveChangesAsync();
    }
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AzilEduDbContext>();

    await db.Database.MigrateAsync();

    if (!await db.Animals.AnyAsync())
    {
        // ... postojeći seed za Animals iz lekcije ostaje ovdje nepromijenjen ...
    }

    if (!await db.HousingUnits.AnyAsync())
    {
        db.HousingUnits.AddRange(
            new HousingUnit
            {
                Name = "Boks A1",
                Type = "Boks za pse",
                Capacity = 2,
                Occupied = 2,
                LastCleanedAt = new DateTime(2026, 6, 28),
                IsActive = true,
                ImageUrl = "/images/housing/boks-a1.webp",
                Note = "Puni kapacitet, dva psa srednje veličine."
            },
            new HousingUnit
            {
                Name = "Boks A2",
                Type = "Boks za pse",
                Capacity = 2,
                Occupied = 1,
                LastCleanedAt = new DateTime(2026, 6, 30),
                IsActive = true,
                ImageUrl = "/images/housing/boks-a2.webp",
                Note = "Ima slobodno mjesto za još jednog psa."
            },
            new HousingUnit
            {
                Name = "Prostor za mačke B1",
                Type = "Prostor za mačke",
                Capacity = 6,
                Occupied = 4,
                LastCleanedAt = new DateTime(2026, 6, 29),
                IsActive = true,
                ImageUrl = "/images/housing/macke-b1.webp",
                Note = "Zajednički prostor sa penjalicama."
            },
            new HousingUnit
            {
                Name = "Karantena K1",
                Type = "Karantena",
                Capacity = 3,
                Occupied = 1,
                LastCleanedAt = new DateTime(2026, 6, 30),
                IsActive = true,
                ImageUrl = "/images/housing/karantena-k1.webp",
                Note = "Novopridošla životinja na promatranju."
            },
            new HousingUnit
            {
                Name = "Boks A3",
                Type = "Boks za pse",
                Capacity = 2,
                Occupied = 0,
                LastCleanedAt = null,
                IsActive = true,
                ImageUrl = "/images/housing/boks-a3.webp",
                Note = "Prazan boks, datum zadnjeg čišćenja nije evidentiran."
            },
            new HousingUnit
            {
                Name = "Stari boks C1",
                Type = "Boks za pse",
                Capacity = 2,
                Occupied = 0,
                LastCleanedAt = new DateTime(2025, 12, 10),
                IsActive = false,
                ImageUrl = "/images/housing/boks-c1.webp",
                Note = "Trenutno izvan uporabe zbog obnove."
            }
        );

        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzilEdu API v1");
    c.RoutePrefix = "swagger"; // 🔥 OVO osigurava /swagger
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
