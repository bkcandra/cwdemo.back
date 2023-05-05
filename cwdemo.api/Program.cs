using AutoMapper;
using cwdemo.core.Common.Constants;
using cwdemo.core.Mapping;
using cwdemo.data.Entities;
using cwdemo.infrastructure;
using cwdemo.infrastructure.DependencyManagement;

namespace cwdemo.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            builder.Services.RegisterDependencies();

            var app = builder.Build();
            SeedDatabase();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


        public static void SeedDatabase()
        {

            Singleton<List<StoreEntity>>.Instance = new List<StoreEntity>() {
        new StoreEntity
        {
            Id = 1,
            Name = "Southbank Pizzeria",
            Active = true,
            Location = "Southbank"
        },
            new StoreEntity
        {
            Id = 2,
            Name = "Preston Pizzeria",
            Active = true,
            Location = "Preston"
        }};

            Singleton<List<CatalogEntity>>.Instance = new List<CatalogEntity>()
{
    new CatalogEntity
    {
        Id = 1,
        Name = "Capricciosa",
        Description = "Cheese, Ham, Mushrooms, Olives",
        Price = 20,
        Active = true,
        Type = (int)CatalogType.Pizza,
        StoreId = 2
    },
    new CatalogEntity
    {
         Id = 2,
        Name = "Mexicana",
        Description = "Cheese, Salami, Capsicum, Chilli",
        Price = 18,
        Active = true,
        Type = (int)CatalogType.Pizza,
        StoreId = 2
    },
    new CatalogEntity
    {
        Id = 3,
        Name = "Margherita",
        Description = "Cheese, Spinach, Ricotta, Cherry Tomatoes",
        Price = 22,
        Active = true,
        Type = (int)CatalogType.Pizza,
        StoreId = 2
    },
    new CatalogEntity
    {
        Id = 4,
        Name = "Capricciosa",
        Description = "Cheese, Ham, Mushrooms, Olives",
        Price = 25,
        Active = true,
        Type =  (int)CatalogType.Pizza,
        StoreId = 1
    },
    new CatalogEntity
    {
        Id = 5,
        Name = "Vegetarian",
        Description = "Cheese, Mushrooms, Capsicum, Onion, Olives",
        Price = 17,
        Active = true,
        Type =  (int)CatalogType.Pizza,
        StoreId = 1
    },
    new CatalogEntity
    {
        Id = 6,
        Name = "Cheese",
        Description = "",
        Price = 1,
        Active = true,
        Type =  (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {
        Id = 7,
        Name = "Cheese",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {Id = 8,
        Name = "Mushrooms",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {
        Id = 9,
        Name = "Capsicum",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {Id =10,
        Name = "Onion",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {Id =11,
        Name = "Olives",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 1
    },
    new CatalogEntity
    {Id =12,
        Name = "Cheese",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 2
    },
    new CatalogEntity
    {Id =13,
        Name = "Mushrooms",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 2
    },
    new CatalogEntity
    {Id =14,
        Name = "Capsicum",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 2
    },
    new CatalogEntity
    {Id =15,
        Name = "Onion",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 2
    },
    new CatalogEntity
    {Id =16,
        Name = "Olives",
        Description = "",
        Price = 1,
        Active = true,
        Type = (int)CatalogType.Toppings,
        StoreId = 2
    }


};

        }
    }
}
