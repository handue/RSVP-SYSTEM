# API Documentation

## Base URL
```
http://localhost:3000/api (Planned)
```

## Authentication
Currently, the API does not require authentication.

## Endpoints

### Reservations

#### Create Reservation
```http
POST /reservation/calendar
```

Request Body:
```json
{
  "reservationData": {
    "email": "string",
    "name": "string",
    "phone": "string",
    "store": "string",
    "store_id": "string",
    "service": "string",
    "store_email": "string",
    "reservation_date": "string",
    "reservation_time": "string",
    "comments": "string",
    "agreedToTerms": boolean,
    "isAdvertisement": boolean
  }
}
```

Response:
```json
{
  "status": number,
  "data": {
    "message": "string"
  }
}
```

#### Send Email
```http
POST /reservation/send
```

Request Body:
```json
{
  "email": "string",
  "name": "string",
  "reservationDetails": object
}
```

Response:
```json
{
  "status": number
}
```

### Store Hours

#### Get Store Hours
```http
GET /store-hours
```

Response:
```json
{
  "storeHours": [
    {
      "id": "string",
      "storeId": "string",
      "day": "string",
      "open": "string",
      "close": "string",
      "isSpecial": boolean,
      "date": "string"
    }
  ]
}
```

#### Update Regular Hours
```http
PUT /store-hours/regular-hours/{storeId}
```

Request Body:
```json
{
  "day": "string",
  "hours": {
    "open": "string",
    "close": "string"
  }
}
```

#### Update Special Date
```http
PUT /store-hours/special-date/{storeId}
```

Request Body:
```json
{
  "date": "string",
  "hours": {
    "open": "string",
    "close": "string"
  }
}
```

#### Delete Special Date
```http
DELETE /store-hours/special-date/{storeId}/{date}
```

## Error Handling

All endpoints follow a consistent error response format:

```json
{
  "status": number,
  "message": "string"
}
```

Common status codes:
- 200: Success
- 400: Bad Request
- 404: Not Found
- 500: Internal Server Error 