using BookOrder.Business.Repo.Interfaces;
using BookOrder.Business.Repo;
using Microsoft.Extensions.DependencyInjection;
using BookOrder.Helper;

namespace BookOrder.Test
{
    public class BookTests
    {
        private IBookRepository _bookRepository;

        ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<BookOrderDbContext>(options =>
                options.UseInMemoryDatabase($"BookOrderDbContext_{Guid.NewGuid()}"));
            services.AddScoped<IBookRepository, BookRepository>();
            _serviceProvider = services.BuildServiceProvider();

            _bookRepository = _serviceProvider.GetRequiredService<IBookRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is not null)
            {
                _serviceProvider.Dispose();
            }
        }

        [Test]
        public async Task Test_Create_Book_Return_TrueAsync()
        {
            var result = await _bookRepository.CreateAsync(new Business.Dto.BookCreateDto
            {
                Name = "Test Book",
                Author = "Test Author",
                Amount = 1,
                Stock = 1,
            }, new());

            Assert.That(result.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_Get_Book_Return_Empty()
        {
            var result = await _bookRepository.GetAllAsync(new Business.Dto.PaginationDto { }, new());
            Assert.That(result.Data, Is.Empty);
        }

        [Test]
        public async Task Test_Get_Book_Return_NonEmptyAsync()
        {
            var resultAdd = await _bookRepository.CreateAsync(new Business.Dto.BookCreateDto
            {
                Name = "Test Book",
                Author = "Test Author",
                Amount = 1,
                Stock = 1,
            }, new());

            Assert.That(resultAdd.IsSuccess, Is.EqualTo(true));

            var result = await _bookRepository.GetAllAsync(new Business.Dto.PaginationDto { }, new());
            Assert.That(result.Data, Is.Not.Empty);
        }
    }
}