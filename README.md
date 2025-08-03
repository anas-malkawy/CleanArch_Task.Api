# CleanArch_Task.Api - Event Booking System

A multi-tenant event booking system built using Clean Architecture principles with ASP.NET Core 8, Entity Framework Core, and SQL Server.

##  Architecture Overview

This project follows **Clean Architecture** principles with clear separation of concerns across four layers:

```
┌─────────────────────────────────────────┐
│              API Layer                  │  ← Controllers, DI Configuration
├─────────────────────────────────────────┤
│           Application Layer             │  ← Business Logic, DTOs, Services
├─────────────────────────────────────────┤
│            Domain Layer                 │  ← Entities, Interfaces
├─────────────────────────────────────────┤
│          Infrastructure Layer           │  ← Data Access, DbContext, Repositories
└─────────────────────────────────────────┘
```

### Project Structure
- **CleanArch_Task.Api** - Web API layer with controllers and startup configuration
- **CleanArch_Task.Application** - Application services and DTOs
- **CleanArch_Task.Domain** - Domain entities and repository interfaces
- **CleanArch_Task.Infrastructure** - Data access implementation and database context

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or SQL Server Express)
- Visual Studio 2022 or VS Code

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CleanArch_Task.Api
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string** (if needed)
   
   Edit `appsettings.json` in the API project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=CleanTask;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   cd CleanArch_Task.Api
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   
   Navigate to: `https://localhost:7031/swagger` or `http://localhost:5041/swagger`

## 🏢 Multi-Tenant Architecture

### Current Implementation
The system is designed with multi-tenancy in mind through the data model:

- **Venues** - Each venue belongs to a specific tenant
- **Events** - Events are held at venues (indirect tenant association)




## 🔧 Dependency Injection Configuration

### DI Container Setup
Located in `Program.cs`, the DI configuration follows Clean Architecture principles:

```csharp
// Database Context Registration
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service Layer Registration
builder.Services.AddScoped<CleanArch_Task.Application.IService.IService, 
                          CleanArch_Task.Application.Service.Service>();

// Repository Layer Registration  
builder.Services.AddScoped<CleanArch_Task.Domain.IRepo.IRepo, 
                          CleanArch_Task.Infrastructure.Repo.Repo>();
```

### DI Design Choices & Rationale

#### 1. **Scoped Lifetime for Services**
```csharp
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<IRepo, Repo>();
```
**Why Scoped?**
- **Database Context Alignment**: Matches Entity Framework's DbContext lifetime
- **Request Isolation**: Each HTTP request gets its own service instances
- **Memory Efficiency**: Services are disposed after each request
- **Thread Safety**: Prevents cross-request data contamination

#### 2. **Interface-Based Registration**
```csharp
// ✅ Good: Dependency on abstraction
builder.Services.AddScoped<IService, Service>();

// ❌ Avoid: Direct dependency on implementation
builder.Services.AddScoped<Service>();
```
**Benefits:**
- **Loose Coupling**: Controllers depend on interfaces, not concrete implementations
- **Testability**: Easy to mock dependencies for unit testing
- **Flexibility**: Can swap implementations without changing dependent code
- **SOLID Principles**: Follows Dependency Inversion Principle

#### 3. **Layer-Specific Registration Pattern**
```csharp
// Application Layer
builder.Services.AddScoped<IService, Service>();

// Infrastructure Layer  
builder.Services.AddScoped<IRepo, Repo>();

// Database Layer
builder.Services.AddDbContext<BookingDbContext>();
```
**Advantages:**
- **Clear Boundaries**: Each layer registers its own dependencies
- **Maintainability**: Easy to locate and modify registrations
- **Separation of Concerns**: Infrastructure concerns separated from business logic

#### 4. **DbContext Registration Strategy**
```csharp
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(connectionString));
```
**Key Points:**
- **Automatic Scoped Lifetime**: DbContext is automatically registered as Scoped
- **Connection Management**: Framework handles connection lifecycle
- **Transaction Scope**: Natural unit of work per HTTP request

### Alternative DI Patterns (Future Considerations)

#### Generic Repository Pattern
```csharp
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
```

#### Factory Pattern for Multi-Tenancy
```csharp
builder.Services.AddScoped<ITenantServiceFactory, TenantServiceFactory>();
```

#### Decorator Pattern for Cross-Cutting Concerns
```csharp
builder.Services.AddScoped<IService, Service>();
builder.Services.Decorate<IService, LoggingServiceDecorator>();
builder.Services.Decorate<IService, CachingServiceDecorator>();
```

## 🌱 Seed Data System

### How Seed Data Works

The seed data is configured in `BookingDbContext.OnModelCreating()` and automatically applied during database migrations.

#### Seed Data Structure
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // 1. Seed Tenants (3 companies)
    modelBuilder.Entity<Tenant>().HasData(
        new Tenant { Id = 1, Email = "contact@eventcorp.com", Name = "EventCorp Solutions" },
        new Tenant { Id = 2, Email = "info@premierent.com", Name = "Premier Entertainment" },
        new Tenant { Id = 3, Email = "hello@globalevents.com", Name = "Global Events Management" }
    );

    // 2. Seed Venues (1 per tenant)
    modelBuilder.Entity<Venue>().HasData(/*...*/);

    // 3. Seed Events (2 per venue = 6 total)
    modelBuilder.Entity<Event>().HasData(/*...*/);

    // 4. Seed Customers (3 test users)
    modelBuilder.Entity<Customer>().HasData(/*...*/);

    // 5. Seed Tickets (50 per event = 300 total)
    modelBuilder.Entity<Ticket>().HasData(/*...*/);

    // 6. Seed Orders (5 sample orders)
    modelBuilder.Entity<Order>().HasData(/*...*/);
}
```

### Seed Data Breakdown

#### 🏢 **Tenants (3)**
- EventCorp Solutions
- Premier Entertainment  
- Global Events Management

#### 🏟️ **Venues (3 - 1 per tenant)**
- Grand Convention Center (EventCorp)
- Riverside Amphitheater (Premier Entertainment)
- Metropolitan Arena (Global Events)

#### 🎉 **Events (6 - 2 per venue)**
- **EventCorp Events:**
  - Tech Innovation Summit 2025
  - Business Leadership Forum
- **Premier Entertainment Events:**
  - Summer Music Festival
  - Comedy Night Spectacular
- **Global Events:**
  - International Trade Expo
  - Sports Championship Finals

#### 🎫 **Tickets (300 - 50 per event)**
Each event has three ticket tiers:
- **VIP Tickets (10)** - Highest price, VIP amenities
- **Premium Tickets (15)** - Mid-tier pricing and features
- **Standard Tickets (25)** - Basic admission

#### 👥 **Customers (3)**
- Ahmed Mohammed (ahmed.mohammed@email.com)
- Sarah Johnson (sarah.johnson@email.com)
- Omar Al-Rashid (omar.alrashid@email.com)

#### 📋 **Orders (5 sample orders)**
Pre-created orders showing different scenarios:
- Multiple quantities
- Different events
- Various price points
- Different customers

### Managing Seed Data

#### Regenerating Seed Data
To reset the database with fresh seed data:
```bash
# Remove database
dotnet ef database drop

# Recreate with seed data
dotnet ef database update
```

#### Adding Custom Seed Data
1. **Modify `BookingDbContext.OnModelCreating()`**
2. **Create new migration**:
   ```bash
   dotnet ef migrations add AddNewSeedData
   ```
3. **Apply migration**:
   ```bash
   dotnet ef database update
   ```

#### Seed Data Best Practices
- **Use static dates** for consistent seeding (avoid `DateTime.Now`)
- **Maintain referential integrity** (ensure foreign keys exist)
- **Use meaningful IDs** for easier testing and debugging
- **Keep data realistic** but minimal for development

## 📡 API Endpoints

### Events API
- `GET /api/Event` - Get all events
- `GET /api/Event/{id}` - Get event by ID
- `POST /api/Event` - Create new event
- `PUT /api/Event/{id}` - Update event
- `DELETE /api/Event/{id}` - Delete event

### Orders API
- `GET /api/Order` - Get all orders
- `GET /api/Order/{id}` - Get order by ID
- `POST /api/Order` - Create new order
- `PUT /api/Order/{id}` - Update order
- `DELETE /api/Order/{id}` - Delete order

### Sample API Requests

#### Create Event
```json
POST /api/Event
Content-Type: multipart/form-data

{
  "title": "New Tech Conference",
  "description": "Cutting-edge technology conference",
  "venueId": 1
}
```

#### Create Order
```json
POST /api/Order  
Content-Type: multipart/form-data

{
  "eventId": 1,
  "customerId": 1,
  "quantity": 2,
  "unitPrice": 150.00
}
```

## 🧪 Testing the Application

### Using Swagger UI
1. Navigate to `https://localhost:7031/swagger`
2. Explore available endpoints
3. Test CRUD operations with the seeded data

### Sample Test Scenarios
1. **List all events** - Should return 6 events across 3 tenants
2. **Create an order** - Use existing customer and event IDs
3. **Update an event** - Modify title or description

### Testing with HTTP Client
Use the provided `CleanArch_Task.Api.http` file with Visual Studio or REST clients.

## 🛠️ Development Guidelines

### Adding New Features
1. **Domain First**: Add entities to Domain layer
2. **Repository Pattern**: Extend IRepo interface and implementation
3. **Service Layer**: Add business logic to Application layer
4. **API Layer**: Create controllers for external access

### Code Organization
- Keep DTOs in Application layer
- Domain entities should be persistence-ignorant
- Controllers should be thin - delegate to services
- Use async/await for database operations


## 📁 Project Files Structure

```
CleanArch_Task.Api/
├── 📁 CleanArch_Task.Api/          # Web API Layer
│   ├── Controllers/
│   │   ├── EventController.cs      # Event management endpoints
│   │   └── OrderController.cs      # Order management endpoints
│   ├── Program.cs                  # DI configuration & startup
│   └── appsettings.json           # Configuration
│
├── 📁 CleanArch_Task.Application/  # Application Layer
│   ├── DTOs/
│   │   ├── EventDTO.cs            # Event data transfer object
│   │   └── OrderDTO.cs            # Order data transfer object
│   ├── IService/
│   │   └── IService.cs            # Service interface
│   └── Service/
│       └── Service.cs             # Business logic implementation
│
├── 📁 CleanArch_Task.Domain/       # Domain Layer
│   ├── Entities/
│   │   ├── Customer.cs            # Customer entity
│   │   ├── Event.cs               # Event entity
│   │   ├── Order.cs               # Order entity
│   │   ├── Tenant.cs              # Tenant entity
│   │   ├── Venue.cs               # Venue entity
│   │   └── Ticket.cs              # Ticket entity
│   └── IRepo/
│       └── IRepo.cs               # Repository interface
│
└── 📁 CleanArch_Task.Infrastructure/ # Infrastructure Layer
    ├── DBContext/
    │   └── BookingDbContext.cs    # EF Core context with seed data
    ├── Migrations/                # EF Core migrations
    └── Repo/
        └── Repo.cs                # Repository implementation
```

## 🤝 Contributing

1. Follow Clean Architecture principles
2. Maintain separation of concerns
3. Write unit tests for new features
4. Update documentation for significant changes
5. Use meaningful commit messages
