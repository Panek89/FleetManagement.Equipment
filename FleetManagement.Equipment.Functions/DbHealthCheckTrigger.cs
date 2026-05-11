using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FleetManagement.Equipment.Functions;

public class DbHealthCheckTrigger
{
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly ILogger _logger;
  private const string TargetUrl = "";

  public DbHealthCheckTrigger(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
  {
    _httpClientFactory = httpClientFactory;
    _logger = loggerFactory.CreateLogger<DbHealthCheckTrigger>();
  }

  [Function("DbHealthCheckTrigger")]
  public async Task Run([TimerTrigger("0 */10 * * * *")] TimerInfo myTimer)
  {
    var currentTime = DateTime.Now;
    _logger.LogInformation("HTTP Health Check started for: {url} at {time}", TargetUrl, currentTime.ToString("yyyy-MM-dd HH:mm:ss"));

    try
    {
      using var client = _httpClientFactory.CreateClient();
      var response = await client.GetAsync(TargetUrl);

      if (response.IsSuccessStatusCode)
      {
        _logger.LogInformation("Health Check SUCCESS: Status {code} ({reason})", (int)response.StatusCode, response.ReasonPhrase);
      }
      else
      {
        _logger.LogWarning("Health Check FAILED: Status {code} ({reason})", (int)response.StatusCode, response.ReasonPhrase);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Health Check CRITICAL ERROR when calling {url}", TargetUrl);
    }
  }
}