using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Service;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<StudentContext>(provider =>
{
    string filePath = "CSV-Files/Student.csv";

    return new StudentContext(filePath);
}
);
builder.Services.AddScoped<TeacherService>();

builder.Services.AddScoped<TeacherContext>(provider =>
{
    string filePath = "CSV-Files/Teacher.csv";

    return new TeacherContext(filePath);
}
);
builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<CourseContext>(provider =>
{
    string filePath = "CSV-Files/Course.csv";

    return new CourseContext(filePath);
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
//added stuff

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
