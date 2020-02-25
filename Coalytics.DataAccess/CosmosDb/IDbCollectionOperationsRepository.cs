using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Coalytics.DataAccess.CosmosDb
{
    public interface IDbCollectionOperationsRepository<TModel, in TPk>
    {
        Task<IEnumerable<TModel>> GetItemsFromCollectionAsync();
        Task<TModel> GetItemFromCollectionAsync(TPk id);
        Task<TModel> AddDocumentIntoCollectionAsync(TModel item);
        Task<TModel> UpdateDocumentFromCollection(TPk id, TModel item);
        Task DeleteDocumentFromCollectionAsync(TPk id);
    }
}