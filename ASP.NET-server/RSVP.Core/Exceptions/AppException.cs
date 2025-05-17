namespace RSVP.Core.Exceptions
{
    /// <summary>
    /// 애플리케이션 전반에서 사용되는 사용자 정의 예외 클래스
    /// Custom exception class used throughout the application
    /// </summary>
    public class AppException : Exception
    {
     
        public string ErrorCode { get; }

        /// <summary>
 
        public object[] Parameters { get; }

  
        /// <param name="parameters">
        /// 오류 메시지에 삽입될 가변 개수의 파라미터들. params 로 선언하면 모든 타입 다 받을수 있고, 여러개를 받을수 있도록 객체 처리
        /// Variable number of parameters to be inserted into error message
        /// 
        /// 예시/Example:
        /// throw new AppException(
        ///     "예약 ID {0}의 시간 {1}에 이미 {2}명의 고객이 예약되어 있습니다.", 
        ///     ErrorCodes.ReservationLimitExceeded, 
        ///     123,                // 정수형 ID / Integer ID
        ///     new DateTime(2023, 5, 15, 14, 30, 0),  // 날짜/시간 / Date/time
        ///     5                   // 인원수 / Number of people
        /// );
        /// </param>
        public AppException(string message, string errorCode, params object[] parameters)
            : base(message)
        {
            ErrorCode = errorCode;
            Parameters = parameters;
        }
    }
} 