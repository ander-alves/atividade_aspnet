
using blogPessoal.Validator;
using FluentValidation;
using GamesGen.Data;
using GamesGen.Model;
using GamesGen.Service;
using GamesGen.Service.Implements;
using GamesGen.Validator;
using Microsoft.EntityFrameworkCore;

namespace GamesGen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Controller Class
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // Add services to the container.

            builder.Services.AddControllers();

            //Conexão com o banco de dados.
            var conectionString = builder.Configuration
                .GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(conectionString));

            //Registrar a validacao das entidades
            builder.Services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
            builder.Services.AddTransient<IValidator<Produto>, ProdutoValidator>();


            //Registrar as classes de Servico
             builder.Services.AddScoped<ICategoriaService, CategoriaService>();
             builder.Services.AddScoped<IProdutoService, ProdutoService>();

            // builder.Services.AddScoped<ITemaService, TemaService>();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configuracao do CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            //Criar config necessaria para Gerar o Banco
            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContect = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContect.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Inicializa o Cors
            app.UseCors("MyPolice");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}