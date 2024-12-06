using BookOrder.Business.Dto;
using BookOrder.Business.Repo.Interfaces;
using BookOrder.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo
{
    public class LoginRepository : ILoginRepository
    {
        BookOrderDbContext _dbContext;

        public LoginRepository(BookOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultDto<string>> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                                           .Where(w => w.Email == dto.Email &&
                                                       w.Password == dto.Password)
                                           .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
                return new ResultDto<string>(ResultMessages.LOGINUSER_CANNOT_FIND);

            string token = GenerateToken();
            return new ResultDto<string>(true, token, "");
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("altaygencaslan_thecodebaseio_casestudy_12345")); //_config["Jwt:Key"]
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("ValidIssuer",
              "ValidAudience",
              null,
              expires: DateTime.Now.AddHours(12),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
