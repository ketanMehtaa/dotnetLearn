erDiagram

    USERS ||--o{ VEHICLES : owns
    USERS ||--o{ RIDES : creates
    USERS ||--o{ BOOKINGS : makes
    USERS ||--o{ REVIEWS : writes
    USERS ||--o{ REVIEWS : receives
    USERS ||--o| USER_PREFERENCES : has
    USERS ||--o{ PAYMENTS : makes
    USERS ||--o{ NOTIFICATIONS : receives
    
    VEHICLES ||--o{ RIDES : used_in
    
    RIDES ||--o{ BOOKINGS : has
    RIDES ||--o{ RIDE_WAYPOINTS : includes
    RIDES }o--|| CITIES : starts_from
    RIDES }o--|| CITIES : ends_at
    
    BOOKINGS ||--|| PAYMENTS : requires
    BOOKINGS ||--o{ REVIEWS : generates
    BOOKINGS }o--|| CITIES : pickup_at
    BOOKINGS }o--|| CITIES : dropoff_at
    
    CITIES ||--o{ RIDE_WAYPOINTS : includes

    USERS {
        uuid id PK
        string email UK
        string phone UK
        string password_hash
        string first_name
        string last_name
        date date_of_birth
        string gender
        string profile_photo_url
        string driving_license_number
        date license_valid_from
        decimal average_rating
        int total_rides_as_driver
        int total_rides_as_passenger
        boolean is_verified
        boolean is_active
        timestamp created_at
        timestamp updated_at
    }

    USER_PREFERENCES {
        uuid id PK
        uuid user_id FK
        boolean smoking_allowed
        boolean pets_allowed
        enum chat_level "silent, light, chatty"
        enum music_preference "none, low, medium, high"
        string music_genre
        boolean verified_riders_only
        enum luggage_size "small, medium, large"
        timestamp created_at
        timestamp updated_at
    }

    VEHICLES {
        uuid id PK
        uuid user_id FK
        string make
        string model
        int year
        string license_plate UK
        string color
        int total_seats
        enum vehicle_type "sedan, suv, hatchback, van"
        decimal comfort_level
        string insurance_number
        date insurance_expiry
        boolean is_verified
        boolean is_active
        timestamp created_at
        timestamp updated_at
    }

    CITIES {
        uuid id PK
        string name
        string state
        string country
        decimal latitude
        decimal longitude
        string timezone
        boolean is_active
        timestamp created_at
    }

    RIDES {
        uuid id PK
        uuid driver_id FK
        uuid vehicle_id FK
        uuid source_city_id FK
        uuid destination_city_id FK
        datetime departure_time
        datetime estimated_arrival_time
        int available_seats
        int total_seats
        decimal price_per_seat
        string pickup_location
        string dropoff_location
        decimal source_latitude
        decimal source_longitude
        decimal destination_latitude
        decimal destination_longitude
        enum status "scheduled, ongoing, completed, cancelled"
        boolean auto_approval
        boolean women_only
        string additional_notes
        enum luggage_allowed "small, medium, large"
        boolean is_recurring
        string recurring_pattern
        timestamp created_at
        timestamp updated_at
    }

    RIDE_WAYPOINTS {
        uuid id PK
        uuid ride_id FK
        uuid city_id FK
        int order_sequence
        decimal price_from_source
        datetime estimated_arrival
        string location_name
        decimal latitude
        decimal longitude
        timestamp created_at
    }

    BOOKINGS {
        uuid id PK
        uuid ride_id FK
        uuid passenger_id FK
        uuid pickup_city_id FK
        uuid dropoff_city_id FK
        int seats_booked
        decimal total_amount
        enum status "pending, confirmed, rejected, cancelled, completed"
        string pickup_location
        string dropoff_location
        string special_requests
        datetime booking_time
        datetime confirmation_time
        datetime cancellation_time
        string cancellation_reason
        timestamp created_at
        timestamp updated_at
    }

    PAYMENTS {
        uuid id PK
        uuid booking_id FK
        uuid user_id FK
        decimal amount
        enum payment_method "card, upi, wallet, cash"
        enum payment_type "booking, refund, commission"
        string transaction_id UK
        enum status "pending, completed, failed, refunded"
        string payment_gateway
        json payment_details
        datetime payment_time
        timestamp created_at
        timestamp updated_at
    }

    REVIEWS {
        uuid id PK
        uuid booking_id FK
        uuid reviewer_id FK
        uuid reviewee_id FK
        int rating
        string comment
        enum review_type "driver_to_passenger, passenger_to_driver"
        boolean is_verified
        datetime review_time
        timestamp created_at
        timestamp updated_at
    }

    NOTIFICATIONS {
        uuid id PK
        uuid user_id FK
        string title
        string message
        enum type "booking, payment, ride_update, review, system"
        boolean is_read
        json data
        datetime sent_at
        datetime read_at
        timestamp created_at
    }
