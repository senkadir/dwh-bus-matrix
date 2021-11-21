using Matrix.Common.Core;
using Matrix.Domain.Commands;
using Matrix.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Core.Services
{
    public interface IFactService : IServiceBase
    {
        Task CreateAsync(CreateFactCommand command);

        Task<IEnumerable<ViewFactModel>> GetAsync();
    }
}
