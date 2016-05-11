using EduTestContract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTestClient.Services.Abstract
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
        Items<T> GetList(int page = 0, int perPage = 10);

        Task<bool> Add(T entity);

        void Update(T entity, int id);

        Task<bool> Remove(int id);
    }
}
