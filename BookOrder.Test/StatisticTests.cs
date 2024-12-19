using BookOrder.Business.Repo.Interfaces;
using BookOrder.Business.Repo;
using Microsoft.Extensions.DependencyInjection;
using BookOrder.Helper;

namespace BookOrder.Test
{
    public class StatisticTests
    {
        private IStatisticRepository _statisticRepository;

        ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<IBookOrderDbContext, BookOrderDbContext>(options =>
                options.UseInMemoryDatabase($"BookOrderDbContext_{Guid.NewGuid()}"));
            services.AddScoped<IStatisticRepository, StatisticRepository>();
            _serviceProvider = services.BuildServiceProvider();

            _statisticRepository = _serviceProvider.GetRequiredService<IStatisticRepository>();
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
        public async Task Test_Get_Statistic_Return_EmptyAsync()
        {
            var result = await _statisticRepository.GetStatisticAsync(Guid.NewGuid(), new());
            Assert.That(result.Data, Is.EqualTo(null));
            Assert.That(result.Message, Is.EqualTo(ResultMessages.STATISTIC_CANNOT_FIND_ANY_DATA));
        }

        public void Test_Get_Statistic_Return_NonEmpty()
        {
            Assert.Pass();
        }
    }
}