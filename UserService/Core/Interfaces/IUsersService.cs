﻿using Core.Entities;

namespace Core.Interfaces;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);

    public Task Create(User user, CancellationToken cancellationToken);
}