using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RSVP.Core.Interfaces;

namespace RSVP.Infrastructure.Services
{
    public class GoogleAuthService
    {
        private readonly GoogleAuthorizationCodeFlow _flow;
        private readonly IConfiguration _configuration;
        private readonly string[] _scopes = new[]
        {
        CalendarService.Scope.Calendar,
        GmailService.Scope.GmailSend,

    };

        public GoogleAuthService(IConfiguration configuration)
        {
            // Console.WriteLine("=== GoogleAuthService 디버깅 ===");
            try
            {
                _configuration = configuration;
                // Console.WriteLine("1. Configuration 주입 성공");

                // // appsettings.json 파일 경로 확인
                // Console.WriteLine($"2. 현재 환경: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

                // // GoogleCredential 섹션 체크
                // var credSection = _configuration.GetSection("GoogleCredential");
                // Console.WriteLine($"3. GoogleCredential 섹션 존재: {credSection.Exists()} : {credSection.Value}");

                // var installedSection = _configuration.GetSection("GoogleCredential:installed");
                // Console.WriteLine($"4. installed 섹션 존재: {installedSection.Exists()}");

                // var clientId = _configuration["GoogleCredential:installed:client_id"];
                // var clientSecret = _configuration["GoogleCredential:installed:client_secret"];

                // Console.WriteLine($"5. client_id 값: {(string.IsNullOrEmpty(clientId) ? "❌ NULL/EMPTY" : "✅ 있음")}");
                // Console.WriteLine($"6. client_secret 값: {(string.IsNullOrEmpty(clientSecret) ? "❌ NULL/EMPTY" : "✅ 있음")}");

                // // GoogleToken 섹션 체크
                // var tokenSection = _configuration.GetSection("GoogleToken");
                // Console.WriteLine($"7. GoogleToken 섹션 존재: {tokenSection.Exists()}");

                // var accessToken = _configuration["GoogleToken:access_token"];
                // Console.WriteLine($"8. access_token 값: {(string.IsNullOrEmpty(accessToken) ? "❌ NULL/EMPTY" : "✅ 있음")}");

                // Console.WriteLine("=== 디버깅 끝 ===");

                // _configuration = configuration;
                // * In terms of the IConfiguration, you have to use ':' instead of '.'
                var credentialsSection = _configuration.GetSection("GoogleCredential:installed");

                // string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
                // string credentialsPath = Path.Combine(projectRoot, "google-credentials.json");
                // string tokenPath = Path.Combine(projectRoot, "GoogleTokens");



                // Console.WriteLine($"Credential JSON: {JsonConvert.SerializeObject(credentialJson, Formatting.Indented)}");

                // var credentialJson = File.ReadAllText(credentialsPath);
                // var json = JObject.Parse(credentialJson);
                // var installed = json["installed"];

                _flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = credentialsSection["client_id"],
                        ClientSecret = credentialsSection["client_secret"].ToString()
                    },
                    Scopes = _scopes,
                    // DataStore = new Google.Apis.Util.Store.FileDataStore("tokenPath", true)
                });

            }
            catch (Exception ex)
            {

                Console.WriteLine($"❌ Error occurred: {ex.Message}");
                Console.WriteLine($"❌ Stack trace: {ex.StackTrace}");
                throw;
            }


        }


        private async Task<UserCredential> GetCredentialAsync()
        {

            var tokenSection = _configuration.GetSection("GoogleToken");
            if (tokenSection.Exists())
            {
                var tokenResponse = new Google.Apis.Auth.OAuth2.Responses.TokenResponse
                {
                    AccessToken = tokenSection["access_token"],
                    RefreshToken = tokenSection["refresh_token"],
                    TokenType = tokenSection["token_type"] ?? "Bearer"
                };

                return new UserCredential(_flow, "user", tokenResponse);
            }

            throw new UnauthorizedAccessException("Google token is not set.");
        }

        public async Task<CalendarService> GetCalendarServiceAsync()
        {
            var credential = await GetCredentialAsync();
            return new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "RSVP System"
            });
        }

        public async Task<GmailService> GetGmailServiceAsync()
        {
            var credential = await GetCredentialAsync();
            return new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "RSVP System"
            });
        }
    }
}