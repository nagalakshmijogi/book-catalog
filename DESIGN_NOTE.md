Design Note – Book Catalog Platform
# Week 1
## What I Built:

I built a REST API for managing a book catalog using ASP.NET Core.
The API supports the basic CRUD operations:
Create a book
Get all books
Get a book by ID
Update a book
Delete a book
I also added input validation, Swagger documentation, logging, and appropriate HTTP status codes.

## How the Solution Is Structured:

For Week 1, I kept the project structure simple:
BookCatalog.api
├── Controllers
│   └── BooksController.cs
├── Models
│   └── Book.cs
└── Program.cs
BooksController handles the API requests and CRUD operations, while Book represents the book data.
I used an in-memory collection for storage because a real database was out of scope for Week 1.

## Key Decisions:

I chose a simple project structure so I could focus on understanding ASP.NET Core fundamentals and building the required API functionality.
I used in-memory storage because it was sufficient for the first stage and kept the implementation simple.
I enabled Swagger so that the API could be easily understood and tested.
I used ASP.NET Core's built-in logging to record important operations and errors.

## What I Would Improve With More Time:

As the project grows, I would separate the application into different layers to keep responsibilities clearer.
I would also introduce DTOs, a data-access abstraction, a real database, and more unit tests.
These improvements are planned for later weeks of the internship.

## What I Found Hard:

The main areas I found challenging during Week 1 were understanding ASP.NET Core logging.
I also needed some time to understand how Swagger works and how it can be used to test the API.
Working through these areas helped me understand the basic structure and behaviour of an ASP.NET Core REST API.
# Week 2 — Design Note

## How I split the layers and why

In Week 2, I restructured the Book Catalog API into four projects: `BookCatalog.api`, `BookCatalog.BusinessLogicLayer`, `BookCatalog.Common`, and `BookCatalog.DataLayer`.
`BookCatalog.api` contains the controllers and is responsible for handling HTTP requests and responses. The controller does not access the repository directly. It calls `IBookService`.
`BookCatalog.BusinessLogicLayer` contains `BookService` and the current `InMemoryBookRepository` implementation. `BookService` is responsible for working through the `IBookRepository` abstraction rather than directly creating or depending on a concrete repository.
`BookCatalog.Common` contains the interfaces and models/DTOs shared between the projects. This includes `IBookService`, `IBookRepository`, request DTOs, response DTOs, `BookFilter`, and `PagedResult`.
`BookCatalog.DataLayer` is present as a separate project for the data-access part of the solution, while the current in-memory repository implementation remains under the BusinessLogicLayer according to the structure I chose for this version.
I separated the solution this way because I wanted the API, business logic, contracts, and implementation details to have clearer responsibilities. The main goal was to make future changes easier without requiring changes throughout the whole application.
The important dependency is that `BookService` depends on `IBookRepository`, not directly on `InMemoryBookRepository`. This means the business logic does not need to know how the data is stored.

## Data access abstraction

I introduced `IBookRepository` as the abstraction between the business logic and the data storage implementation.
The interface defines operations such as getting books, creating, updating, and deleting books, as well as filtering and pagination.
`BookService` uses the interface:
`IBookRepository`
instead of directly using:
`InMemoryBookRepository`
The current repository stores books in a `List<Book>`. The repository is responsible for the storage-related operations and maps the internal `Book` model to `BookResponse` when returning data.
The main reason for this abstraction is to make the storage replaceable. For example, in Week 3 the in-memory implementation can be replaced with a database implementation without requiring the controller or business logic to know the details of the database.

## DTOs and API contracts

I separated the API contracts from the internal `Book` model.
The API uses `CreateBookRequest` and `UpdateBookRequest` for incoming data and `BookResponse` for outgoing book data. `BookFilter` is used for filtering and pagination, and `PagedResult<BookResponse>` provides the paginated response.
The internal `Book` entity is therefore not exposed directly by the controller.
This separation means that the internal model can change without necessarily changing what the API exposes to clients.

## How I decided what to test

I focused the unit tests on important application behavior rather than trying to test every line of code.
I tested the main business operations and important error paths, including situations where a book does not exist. I also tested validation because invalid requests should be rejected.
For Week 2, pagination and filtering were new requirements, so I included tests for these behaviors as well.
I used xUnit for unit testing and Moq for mocking dependencies. Mocking the repository allows the business logic to be tested without using an external database, file system, or network.
I also added tests for invalid pagination values such as `PageNumber = 0` and `PageSize = 0`.
I did not aim for 100% coverage just for the sake of the percentage. I focused on the important behavior, validation rules, and error paths. At the end of the changes, all 24 tests passed.

## What was painful to change from Week 1

The most difficult part was restructuring code that was already working.
In Week 1, the main goal was to make the API work, so some decisions were simpler and more tightly connected. In Week 2, introducing interfaces, DTOs, repository abstraction, pagination, filtering, and tests meant that changes sometimes had to be made in several places.
For example, when an operation could return `null`, the return type needed to be updated consistently in the repository interface, repository implementation, service interface, and service implementation.
Pagination also required changes to the request model, repository logic, response model, and tests.
This showed me that the original Week 1 design was sufficient for a small working API, but it was not as easy to change as the Week 2 structure. The difficulty of restructuring it showed me why separation of responsibilities and abstractions are important as a project grows.
At the same time, the restructuring made testing easier. Because `BookService` depends on `IBookRepository`, the repository can be mocked in unit tests.

## Pagination and filtering

The list endpoint now supports filtering by title, author, and published year.
Filtering is applied before pagination, so `TotalCount` and `TotalPages` represent the filtered results.
The client can specify `PageNumber` and `PageSize`. The response contains the current items, page number, page size, total count, and total pages.
I also added validation for pagination values so invalid values such as page number `0` or page size `0` are rejected.

## What I would improve

The current storage is still in memory because a real database is intentionally out of scope for Week 2.
In Week 3, I would move the storage to a real database implementation while keeping the repository abstraction so that the business logic does not need to depend on the database details.
I would also continue improving the project structure and mapping as the application becomes more complex.
The main improvement in Week 2 was making the existing application easier to understand, test, and change rather than simply adding more features.

