using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Identity.Contexts;

namespace Identity.Repositories;

public class UserRepository(ApplicationIdentityDbContext context) : RepositoryBase<User>(context), IUserRepository
{
}