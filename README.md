# Twilio Champions BLR Meetup - Demo Function

Azure Function that SMSes you a random word every minute via Twilio

## 📝 Environment Variables

Go to Settings tab of your Cloud Function and add the following environment variables:

- `AccountSid`: Twilio Account SID
- `AuthToken`: Twilio Auth Token
- `TwilioNumber`: Twilio Phone Number to send the SMS from
- `MyNumber`: Your phone number to receive the SMS

> ℹ️ _The Twilio Account SID and Auth Token can be obtained from your Twilio console. You can purchase a Twilio phone number using [this guide](https://support.twilio.com/hc/en-us/articles/223135247-How-to-Search-for-and-Buy-a-Twilio-Phone-Number-from-Console)._
