using Drivex.Domain.Users;
using Drivex.Repositories.Base;
using Microsoft.Data.SqlClient;

namespace Drivex.Repositories.Users;

public class UserRepository : BaseSqlRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return QueryStoredProcedureSingleAsync(
            "sp_GetUserByEmail",
            MapUser,
            command => command.Parameters.Add(new SqlParameter("@Email", email)),
            cancellationToken);
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var created = await QueryStoredProcedureSingleAsync(
            "sp_CreateUser",
            MapUser,
            command =>
            {
                command.Parameters.Add(new SqlParameter("@Name", user.Name));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@PasswordHash", user.PasswordHash));
                command.Parameters.Add(new SqlParameter("@Phone", (object?)user.Phone ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Role", user.Role));
            },
            cancellationToken);

        return created ?? throw new InvalidOperationException("User could not be created.");
    }

    private static User MapUser(SqlDataReader reader)
    {
        return new User
        {
            Id = GetRequired<int>(reader, "Id"),
            Name = GetRequired<string>(reader, "Name"),
            Email = GetRequired<string>(reader, "Email"),
            PasswordHash = GetRequired<string>(reader, "PasswordHash"),
            Phone = GetOptional<string>(reader, "Phone"),
            Role = GetRequired<string>(reader, "Role")
        };
    }
}
