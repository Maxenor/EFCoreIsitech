# EFCoreIsitech Application Design Document

## Table of Contents
- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Database Design](#database-design)
- [Entity Framework Core Configuration](#entity-framework-core-configuration)
- [API Design](#api-design)
- [Coding Standards](#coding-standards)
- [Development Workflow](#development-workflow)

## Project Overview

EFCoreIsitech is a .NET 8 Web API application that demonstrates the use of Entity Framework Core with MySQL as its database provider. The application follows RESTful API principles and uses Fluent API for entity configuration.

### Technology Stack
- **Framework**: .NET 8
- **ORM**: Entity Framework Core
- **Database**: MySQL
- **API Documentation**: Swagger/OpenAPI
- **Containerization**: Docker

## Architecture

The application follows a clean, layered architecture:

1. **Presentation Layer**: API Controllers
2. **Business Logic Layer**: Services (to be implemented)
3. **Data Access Layer**: EF Core DbContext and Repositories (when needed)
4. **Domain Layer**: Entity Models

### Project Structure
```
EFCoreIsitech/
├── Controllers/            # API Controllers
├── Data/                   # Data access related code
│   ├── Configurations/     # Entity type configurations
│   ├── Repositories/       # Repository implementations
│   └── ApplicationDbContext.cs
├── Models/                 # Domain models and DTOs
├── Services/               # Business logic services
└── Program.cs              # Application entry point
```

## Database Design

### Entity Relationships

The application starts with a simple product catalog, with plans to expand to a more complex domain model:

- **Product**: Basic product information

As the application grows, we'll add more entities with relationships following these patterns:
- One-to-many relationships
- Many-to-many relationships
- One-to-one relationships

## Entity Framework Core Configuration

We use the Fluent API approach for configuring entities in EF Core rather than data annotations, as it provides more flexibility and separation of concerns.

### Fluent API Guidelines

1. **Entity Configurations**

   Entity configurations should be placed in separate configuration classes in the `Data/Configurations` folder, implementing `IEntityTypeConfiguration<TEntity>`:

   ```csharp
   public class ProductConfiguration : IEntityTypeConfiguration<Product>
   {
       public void Configure(EntityTypeBuilder<Product> builder)
       {
           builder.ToTable("Products");
           
           builder.HasKey(p => p.Id);
           
           builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
                
           builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);
                
           // Indexes
           builder.HasIndex(p => p.Name);
       }
   }
   ```

2. **DbContext Configuration**

   In the `OnModelCreating` method, use `ApplyConfiguration` to apply entity configurations:

   ```csharp
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);
       
       modelBuilder.ApplyConfiguration(new ProductConfiguration());
       // Apply other configurations...
       
       // Seed data can be defined in configurations or here
   }
   ```

3. **Relationship Configuration Examples**

   - **One-to-Many**:
     ```csharp
     builder.HasMany(e => e.Products)
         .WithOne(e => e.Category)
         .HasForeignKey(e => e.CategoryId)
         .OnDelete(DeleteBehavior.Restrict);
     ```

   - **Many-to-Many**:
     ```csharp
     builder.HasMany(e => e.Tags)
         .WithMany(e => e.Products)
         .UsingEntity<Dictionary<string, object>>(
             "ProductTag",
             j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
             j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
         );
     ```

4. **Data Seeding**:

   ```csharp
   builder.HasData(
       new Product { Id = 1, Name = "Product 1", Price = 10.99m },
       new Product { Id = 2, Name = "Product 2", Price = 19.99m }
   );
   ```

## API Design

### RESTful Endpoints

Follow these RESTful conventions for endpoints:

- **GET** `/api/resources` - Get all resources
- **GET** `/api/resources/{id}` - Get a specific resource
- **POST** `/api/resources` - Create a new resource
- **PUT** `/api/resources/{id}` - Update a resource
- **DELETE** `/api/resources/{id}` - Delete a resource

### Response Patterns

Standardize API responses using consistent patterns:

```csharp
// Success response
return Ok(new { 
    Success = true, 
    Data = result 
});

// Error response
return BadRequest(new { 
    Success = false, 
    Error = "Error message" 
});
```

## Coding Standards

### Naming Conventions

- **Classes**: PascalCase (e.g., `ProductService`)
- **Methods**: PascalCase (e.g., `GetProducts`)
- **Variables**: camelCase (e.g., `productList`)
- **Constants**: UPPER_CASE (e.g., `MAX_PRODUCTS`)
- **Interfaces**: Prefix with "I" (e.g., `IProductRepository`)
- **Database Tables**: Plural form (e.g., `Products`)

### Code Structure

- Keep methods focused and small
- Follow single responsibility principle
- Use dependency injection for dependencies
- Prefer async/await for I/O operations

### Comments and Documentation

- Use XML comments for public APIs
- Document non-obvious code sections
- Keep comments up-to-date with code changes

## Development Workflow

### Database Migrations

Always use migrations to manage database schema changes:

```bash
# Create a migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update
```

### Testing Strategy

- **Unit Tests**: Test individual components in isolation
- **Integration Tests**: Test components working together
- **API Tests**: Test API endpoints

### Deployment

The application is containerized using Docker:

```bash
# Build the image
docker build -t efcoreisitech .

# Run the container
docker-compose up
```

## Future Considerations

- Implementing authentication and authorization
- Adding caching layer
- Setting up continuous integration and delivery
- Performance optimization strategies