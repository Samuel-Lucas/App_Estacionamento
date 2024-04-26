using System.Security.Claims;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Domain.ViewModels;
using Estacionamento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly AppDbContext _context;

    public AuthenticationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Claim>> AuthenticateAsync(LoginViewModel model)
    {
        var userAccount = await _context.Pessoas!.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

        if (userAccount is null || VerifyPassword(model.Senha!, userAccount.Senha)!)
        {
            return null!;
        }

        return new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email!),
            new Claim(ClaimTypes.Role, userAccount.Role!)
        };
    }

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}