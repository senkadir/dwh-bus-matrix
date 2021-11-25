using Dwh.Common.Core;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwh.Core.Services
{
    public interface IMatrixService : IServiceBase
    {
        Task<List<ViewMatrixModel>> GetAsync();

        Task<ViewMatrixModel> GetAsync(GetMatrixQuery query);

        Task CreateAsync(CreateMatrixCommand command);

        Task CreateAsync(CreateMatrixItemCommand command);

    }
}
