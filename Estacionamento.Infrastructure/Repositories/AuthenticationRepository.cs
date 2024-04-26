using System.Security.Claims;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Domain.ViewModels;
using Estacionamento.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthenticationRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
            new Claim(ClaimTypes.NameIdentifier, userAccount.IdPessoa),
            new Claim(ClaimTypes.Name, model.Email!),
            new Claim(ClaimTypes.Role, userAccount.Role!)
        };
    }

    public async Task<string> GetAuthenticatedIdPessoa()
    {
        var pessoaLogadaId = await Task.FromResult(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (pessoaLogadaId is null)
            return null!;
        
        return pessoaLogadaId;
    }

    private static bool VerifyPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}