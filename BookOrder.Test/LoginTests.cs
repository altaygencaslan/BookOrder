using BookOrder.Business.Repo;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookOrder.Test
{
    public class LoginTests
    {
        private ServiceProvider _serviceProvider;

        private ILoginRepository _loginRepository;
        private ICustomerRepository _customerRepository;


        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<IBookOrderDbContext, BookOrderDbContext>(options =>
                options.UseInMemoryDatabase($"BookOrderDbContext_{Guid.NewGuid()}"));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            _serviceProvider = services.BuildServiceProvider();

            _customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();
            _loginRepository = _serviceProvider.GetRequiredService<ILoginRepository>();
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
        public async Task Test_Validation_Login_Return_AsFalseAsync()
        {
            var result = await _loginRepository.LoginAsync(new Business.Dto.LoginDto { }, new());
            Assert.That(result.Message, Is.EqualTo(ResultMessages.LOGINUSER_CANNOT_FIND));
        }

        [Test]
        public async Task Test_Validation_Login_Return_AsTrueAsync()
        {
            var resultAdd = await _customerRepository.CreateAsync(new Business.Dto.CustomerCreateDto { Name = "Altay", LastName = "Gençaslan", Email = "developer.gencaslan@gmail.com", Password = "123456" }, new());
            Assert.That(resultAdd.IsSuccess, Is.EqualTo(true));

            var result = await _loginRepository.LoginAsync(new Business.Dto.LoginDto { Email = "developer.gencaslan@gmail.com", Password = "123456" }, new());
            Assert.That(result.IsSuccess, Is.EqualTo(true));
        }
    }
}