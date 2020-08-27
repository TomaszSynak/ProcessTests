namespace ProcessTests.Api.Components.Authenticate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public class UserProvider
    {
        private readonly IList<User> _users = new List<User>
        {
            new User("ProcessTestUser", "processtestuser@email.com", "processtestpass", Guid.Parse("50FF89B4-C390-4B97-8CF0-310BF8B3877D"))
        };

        public Task<User> GetUser(CredentialsDto credentialsDto, CancellationToken ct = default)
            => Task.FromResult(_users
                .SingleOrDefault(u =>
                    u.Email.Equals(credentialsDto.Email, StringComparison.InvariantCultureIgnoreCase) &&
                    u.Password.Equals(credentialsDto.Password, StringComparison.InvariantCultureIgnoreCase)));
    }
}
