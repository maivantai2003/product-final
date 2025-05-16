using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.SeedData
{
    public class Seed
    {
        private readonly AppDbContext _context;
        public Seed(AppDbContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Books.Any())
            {
                var books = new List<Book>()
                {
                    new Book()
                    {
                        Title = "C# Programming Fundamentals",
                        Author = "John Smith",
                        Price = 300,
                        Available = true,
                        Publisher = "CodePress",
                        CreatedDate = new DateTime(2021, 10, 15),
                        Genre = new Genre { Name = "Programming", Description = "Fundamentals of C# programming", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Programming", Description = "Books on programming", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 300, Quantity = 2, Cart = new Cart { TotalPrice = 600, Status = "Delivered", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user1", Email = "user1@example.com", PhoneNumber = "456", Address = "654" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "ASP.NET Core for Beginners",
                        Author = "Jane Doe",
                        Price = 550,
                        Available = true,
                        Publisher = "WebDev Press",
                        CreatedDate = new DateTime(2022, 5, 10),
                        Genre = new Genre { Name = "Web Development", Description = "Introduction to ASP.NET Core", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Web Development", Description = "Books on web development", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 550, Quantity = 1, Cart = new Cart { TotalPrice = 550, Status = "Pending", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user2", Email = "user2@example.com", PhoneNumber = "789", Address = "987" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "React and Redux in Action",
                        Author = "Emily White",
                        Price = 650,
                        Available = true,
                        Publisher = "Frontend Press",
                        CreatedDate = new DateTime(2023, 3, 20),
                        Genre = new Genre { Name = "Frontend", Description = "Guide to React and Redux", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Frontend Development", Description = "Books on frontend technologies", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 650, Quantity = 1, Cart = new Cart { TotalPrice = 650, Status = "Shipped", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user4", Email = "user4@example.com", PhoneNumber = "987", Address = "123" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "Clean Architecture with .NET",
                        Author = "Robert Martin",
                        Price = 700,
                        Available = true,
                        Publisher = "Architecture Press",
                        CreatedDate = new DateTime(2022, 12, 1),
                        Genre = new Genre { Name = "Software Architecture", Description = "Principles of clean architecture", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Software Architecture", Description = "Books on software architecture", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 700, Quantity = 4, Cart = new Cart { TotalPrice = 2800, Status = "Delivered", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user5", Email = "user5@example.com", PhoneNumber = "321", Address = "432" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "SQL Server Optimization Techniques",
                        Author = "Thomas Lee",
                        Price = 500,
                        Available = true,
                        Publisher = "DB Tech",
                        CreatedDate = new DateTime(2023, 8, 15),
                        Genre = new Genre { Name = "Database", Description = "Optimizing SQL Server performance", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Database Management", Description = "Books on database management", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 500, Quantity = 2, Cart = new Cart { TotalPrice = 1000, Status = "Shipped", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user6", Email = "user6@example.com", PhoneNumber = "123", Address = "654" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "JavaScript: The Good Parts",
                        Author = "Douglas Crockford",
                        Price = 400,
                        Available = true,
                        Publisher = "O'Reilly Media",
                        CreatedDate = new DateTime(2021, 4, 1),
                        Genre = new Genre { Name = "Programming", Description = "Best practices in JavaScript", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Programming", Description = "Books on programming", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 400, Quantity = 1, Cart = new Cart { TotalPrice = 400, Status = "Pending", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user7", Email = "user7@example.com", PhoneNumber = "123", Address = "999" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "Mastering C# and .NET Framework",
                        Author = "Marilyn Devenny",
                        Price = 600,
                        Available = true,
                        Publisher = "Packt Publishing",
                        CreatedDate = new DateTime(2022, 2, 18),
                        Genre = new Genre { Name = "Programming", Description = "Advanced concepts in C# and .NET", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Programming", Description = "Books on programming", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 600, Quantity = 2, Cart = new Cart { TotalPrice = 1200, Status = "Shipped", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user8", Email = "user8@example.com", PhoneNumber = "456", Address = "888" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "The Pragmatic Programmer",
                        Author = "Andrew Hunt and David Thomas",
                        Price = 750,
                        Available = true,
                        Publisher = "Addison-Wesley",
                        CreatedDate = new DateTime(2023, 7, 30),
                        Genre = new Genre { Name = "Software Development", Description = "Principles and practices in programming", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Software Development", Description = "Books on software development", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 750, Quantity = 1, Cart = new Cart { TotalPrice = 750, Status = "Pending", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user9", Email = "user9@example.com", PhoneNumber = "321", Address = "777" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "Understanding Machine Learning",
                        Author = "Shai Shalev-Shwartz and Shai Ben-David",
                        Price = 800,
                        Available = true,
                        Publisher = "Cambridge University Press",
                        CreatedDate = new DateTime(2023, 1, 10),
                        Genre = new Genre { Name = "Machine Learning", Description = "Foundations and techniques in machine learning", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Machine Learning", Description = "Books on machine learning", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 800, Quantity = 3, Cart = new Cart { TotalPrice = 2400, Status = "Delivered", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user10", Email = "user10@example.com", PhoneNumber = "654", Address = "666" } } }
                        }
                    },
                    new Book()
                    {
                        Title = "Algorithms Unlocked",
                        Author = "Thomas H. Cormen",
                        Price = 500,
                        Available = true,
                        Publisher = "MIT Press",
                        CreatedDate = new DateTime(2023, 5, 5),
                        Genre = new Genre { Name = "Computer Science", Description = "An introduction to algorithms", IsActive = true },
                        BookCatalogs = new List<BookCatalog>()
                        {
                            new BookCatalog { Catalog = new Catalog() { Title = "Computer Science", Description = "Books on computer science", IsActive = true } }
                        },
                        CartDetails = new List<CartDetail>()
                        {
                            new CartDetail { Price = 500, Quantity = 2, Cart = new Cart { TotalPrice = 1000, Status = "Pending", CreatedDate = DateTime.Now, User = new AppUser() { UserName = "user11", Email = "user11@example.com", PhoneNumber = "987", Address = "555" } } }
                        }
                    }
                };

                _context.Books.AddRange(books);
                _context.SaveChanges();
            }
        }
    }
}
