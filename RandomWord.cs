using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace My.Function
{
    public class RandomWord
    {
        private readonly ILogger _logger;

        public RandomWord(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RandomWord>();
        }

        // Function to SMS a random word every minute
        [Function("RandomWord")]
        public async Task Run([TimerTrigger("0 * * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
           
            var client = new HttpClient();
            var randomWordResponse = await client.GetAsync("https://random-word-api.herokuapp.com/word");
            var randomWordResponseString = await randomWordResponse.Content.ReadAsStringAsync();
            var randomWord = JsonConvert.DeserializeObject<List<string>>(randomWordResponseString);

            var accountSid = Environment.GetEnvironmentVariable("AccountSid");
            var authToken = Environment.GetEnvironmentVariable("AuthToken");
            var fromNumber = Environment.GetEnvironmentVariable("TwilioNumber");
            var toNumber = Environment.GetEnvironmentVariable("MyNumber");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new Twilio.Types.PhoneNumber(toNumber),
                from: new Twilio.Types.PhoneNumber(fromNumber),
                body: "Random Word of the Minute:\n\n" + randomWord[0]
            );

            _logger.LogInformation("Word sent: " + randomWord[0]);
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
