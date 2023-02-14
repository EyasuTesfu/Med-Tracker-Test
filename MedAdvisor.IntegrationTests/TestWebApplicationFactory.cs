
using MedAdvisor.DataAccess.MySql.DataContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

        var con_string = "server=localhost;user = root;database=med-advisor;password=root";
        
        var options = new DbContextOptionsBuilder<AppDbContext>().UseMySql(con_string,
                            ServerVersion.AutoDetect(con_string)); 

        var dbContext = new AppDbContext(options.Options);
        dbContext.Database.Migrate();

        });
    }



}




//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;


//public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
//{
//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        builder.ConfigureServices(services =>
//        {
//            // add db contect 

//        });
//    }
//}