using BookOrder.Business.Repo.Interfaces;
using BookOrder.Business.Repo;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace BookOrder.Test
{
    public class CustomerTests
    {
        private ICustomerRepository _customerRepository;

        ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<BookOrderDbContext>(options =>
                options.UseInMemoryDatabase($"BookOrderDbContext_{Guid.NewGuid()}"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            _serviceProvider = services.BuildServiceProvider();
            services.AddFluentValidationAutoValidation();
            _customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();
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
        public async Task Test_Create_Customer_Return_TrueAsync()
        {
            var result = await _customerRepository.CreateAsync(new Business.Dto.CustomerCreateDto
            {
                Name = "Test",
                LastName = "Test",
                Email = "Test",
                Password = "Test"
            }, new());

            Assert.That(result.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_Get_Customer_Return_EmptyAsync()
        {
            var result = await _customerRepository.GetAllAsync(new Business.Dto.PaginationDto { },new());
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Data, Is.Empty);
        }

        [Test]
        public async Task Test_Get_Customer_Return_NonEmptyAsync()
        {
            var resultAdd = await _customerRepository.CreateAsync(new Business.Dto.CustomerCreateDto
            {
                Name = "Test",
                LastName = "Test",
                Email = "Test",
                Password = "Test"
            }, new());
            Assert.That(resultAdd.IsSuccess, Is.EqualTo(true));

            var result = await _customerRepository.GetAllAsync(new Business.Dto.PaginationDto { }, new());
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Data, Is.Not.Empty);
        }
    }
}