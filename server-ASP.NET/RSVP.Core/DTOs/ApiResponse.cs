using System.Text.Json.Serialization;
using RSVP.Core.Models;

namespace RSVP.Core.DTOs
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("error")]
        public ErrorResponse? Error { get; set; }

        public static ApiResponse<T> CreateSuccess(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        public static ApiResponse<T> CreateError(ErrorResponse error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                // Default = default value of type T
                // ex) int = 0, string = null, bool = false, etc.

                Data = default,
                Error = error
            };
        }
    }
}