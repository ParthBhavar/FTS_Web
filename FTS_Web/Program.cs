using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
//using System.Runtime.InteropServices;

namespace FTS_Web
{
    public class Program
    {
        //static Process xvfb;
        //const string xvfb_pid = "pid.xvfb.fr";
        public static void Main(string[] args)
        {
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                //LinuxStart();
                CreateHostBuilder(args).Build().Run();
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                //LinuxStop();
        }


        //private static void LinuxStop()
        //{
        //    xvfb.Kill();
        //    if (File.Exists(xvfb_pid))
        //        File.Delete(xvfb_pid);
        //}

        //public static void LinuxStart()
        //{
        //    if (File.Exists(xvfb_pid))
        //    {
        //        string pid = File.ReadAllText(xvfb_pid);
        //        try
        //        {
        //            xvfb = Process.GetProcessById(int.Parse(pid));
        //            xvfb.Kill();
        //            xvfb = null;
        //        }
        //        catch { }
        //        File.Delete(xvfb_pid);
        //    }
        //    string display = Environment.GetEnvironmentVariable("DISPLAY");
        //    if (String.IsNullOrEmpty(display))
        //    {
        //        Environment.SetEnvironmentVariable("DISPLAY", ":99");
        //        display = ":99";
        //    }
        //    ProcessStartInfo info = new ProcessStartInfo();
        //    info.FileName = "/usr/bin/Xvfb";
        //    info.Arguments = display + " -ac -screen 0 1024x768x16 +extension RANDR -dpi 96";
        //    info.CreateNoWindow = true;
        //    xvfb = new Process();
        //    xvfb.StartInfo = info;
        //    xvfb.Start();
        //    File.WriteAllText(xvfb_pid, xvfb.Id.ToString());
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.AddConsole();
            //})
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                //webBuilder.UseUrls("http://[::]:5000");
                //webBuilder.Build();
                .UseKestrel(options =>
                 {
                     options.Limits.MaxRequestBodySize = null;
                     //options.AllowSynchronousIO = true;
                 });

            });

    }
}


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();
