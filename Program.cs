using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;
using SigmaBackend.Services;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddSqlServer<SigmaContext>(builder.Configuration.GetConnectionString("cnSigma"));
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;//Asi evitamos la referencias ciclicas.
});

var myCorsRules = "CorsRules";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: myCorsRules, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

//To use whit local database
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// To use whit online database.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();*/



app.UseCors(myCorsRules);

app.UseAuthorization();

app.MapControllers();

app.Run();
