namespace RSVP.Core.Exceptions
{
    public static class ErrorCodes
    {
        // 공통 에러
        public const string ValidationError = "VALIDATION_ERROR";
        public const string NotFound = "NOT_FOUND";
        public const string DuplicateEntry = "DUPLICATE_ENTRY";
        public const string BusinessRuleViolation = "BUSINESS_RULE_VIOLATION";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Forbidden = "FORBIDDEN";

        // 예약 관련 에러
        public const string ReservationTimeSlotUnavailable = "RESERVATION_TIME_SLOT_UNAVAILABLE";
        public const string ReservationStoreClosed = "RESERVATION_STORE_CLOSED";
        public const string ReservationServiceUnavailable = "RESERVATION_SERVICE_UNAVAILABLE";
        public const string ReservationLimitExceeded = "RESERVATION_LIMIT_EXCEEDED";

        // 매장 관련 에러
        public const string StoreNotFound = "STORE_NOT_FOUND";
        public const string StoreHoursInvalid = "STORE_HOURS_INVALID";
        public const string StoreServiceNotFound = "STORE_SERVICE_NOT_FOUND";

        // 서비스 관련 에러
        public const string ServiceNotFound = "SERVICE_NOT_FOUND";
        public const string ServiceDurationInvalid = "SERVICE_DURATION_INVALID";
    }
} 