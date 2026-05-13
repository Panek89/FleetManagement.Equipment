using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FleetManagement.Equipment.Functions;

public class DbHealthCheckTrigger
{
  private const string DbHealthCheckUrlSetting = "EQUIPMENT_DB_HEALTHCHECK_URL";
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly ILogger _logger;
  private readonly string _targetUrl;

  public DbHealthCheckTrigger(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
  {
    _httpClientFactory = httpClientFactory;
    _logger = loggerFactory.CreateLogger<DbHealthCheckTrigger>();
    _targetUrl = Environment.GetEnvironmentVariable(DbHealthCheckUrlSetting)
      ?? throw new InvalidOperationException($"Missing required app setting '{DbHealthCheckUrlSetting}'.");
  }

  [Function("DbHealthCheckTrigger")]
  public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
  {
    var currentTime = DateTime.Now;
    _logger.LogInformation("HTTP Health Check started for: {url} at {time}", _targetUrl, currentTime.ToString("yyyy-MM-dd HH:mm:ss"));

    try
    {
      using var client = _httpClientFactory.CreateClient();
      var response = await client.GetAsync(_targetUrl);

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
      _logger.LogError(ex, "Health Check CRITICAL ERROR when calling {url}", _targetUrl);
    }
  }
}
