using Dwh.Common.Core;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwh.Core.Services
{
    public interface IFactService : IServiceBase
    {
        Task CreateAsync(CreateFactCommand command);

        Task EditAsync(EditFactCommand command);

        Task<IEnumerable<ViewFactModel>> GetAsync();

        Task<ViewFactModel> GetAsync(Guid id);
    }
}
