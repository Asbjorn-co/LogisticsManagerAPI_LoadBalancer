using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogisticsManagerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LogisticsManagerController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<LogisticsManagerController> _logger;

    public LogisticsManagerController(ILogger<LogisticsManagerController> logger)
    {
        _logger = logger;
    }

    [HttpGet("version")]
    public async Task<Dictionary<string, string>> GetVersion()
    {
        var properties = new Dictionary<string, string>();
        var assembly = typeof(Program).Assembly;

        properties.Add("service", "LogisticManager");
        var ver = FileVersionInfo.GetVersionInfo(
        typeof(Program).Assembly.Location).ProductVersion ?? "N/A";
        properties.Add("version", ver);

        var hostName = System.Net.Dns.GetHostName();
        var ips = await System.Net.Dns.GetHostAddressesAsync(hostName);
        var ipa = ips.First().MapToIPv4().ToString() ?? "N/A";
        properties.Add("ip-address", ipa);

        return properties;
    }
}
