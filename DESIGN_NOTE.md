Design Note – Book Catalog Platform
Week 1
What I Built:

I built a REST API for managing a book catalog using ASP.NET Core.
The API supports the basic CRUD operations:
Create a book
Get all books
Get a book by ID
Update a book
Delete a book
I also added input validation, Swagger documentation, logging, and appropriate HTTP status codes.

How the Solution Is Structured:

For Week 1, I kept the project structure simple:
BookCatalog.api
├── Controllers
│   └── BooksController.cs
├── Models
│   └── Book.cs
└── Program.cs
BooksController handles the API requests and CRUD operations, while Book represents the book data.
I used an in-memory collection for storage because a real database was out of scope for Week 1.

Key Decisions:

I chose a simple project structure so I could focus on understanding ASP.NET Core fundamentals and building the required API functionality.
I used in-memory storage because it was sufficient for the first stage and kept the implementation simple.
I enabled Swagger so that the API could be easily understood and tested.
I used ASP.NET Core's built-in logging to record important operations and errors.

What I Would Improve With More Time:

As the project grows, I would separate the application into different layers to keep responsibilities clearer.
I would also introduce DTOs, a data-access abstraction, a real database, and more unit tests.
These improvements are planned for later weeks of the internship.

What I Found Hard:

The main areas I found challenging during Week 1 were understanding ASP.NET Core logging.
I also needed some time to understand how Swagger works and how it can be used to test the API.
Working through these areas helped me understand the basic structure and behaviour of an ASP.NET Core REST API.
