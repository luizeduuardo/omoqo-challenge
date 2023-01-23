namespace Omoqo.Challenge.Api.Core.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(OmoqoContext context) : base(context) { }
}