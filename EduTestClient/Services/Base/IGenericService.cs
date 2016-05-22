using System.Collections.Generic;
using System.Threading.Tasks;
using EduTestClient.Services.Entities;
using EduTestContract.Models;

namespace EduTestClient.Services.Base
{
    public interface IGenericService<T> where T : new()
    {
        Task<T> Get(int id);

        /// <summary>
        /// Returns a list of items
        /// </summary>
        /// <param name="page">Page, 0-indexed. Default 0.</param>
        /// <param name="perPage">Elements per page. Maximum 1000. Default 10.</param>
        /// <returns></returns>
        Task<Items<T>> GetList(int page = 0, int perPage = 10);

        Task<bool> Add(T entity, params KeyValuePair<string, object>[] addParams);

        void Update(T entity, int id);

        Task<bool> Remove(int id);

        void SetAuthData(AuthenticationResponse data);
    }
}
