using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRepositoriesBase<T> where T : class, new() 
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T model);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> InsertBatchAsync(List<T> models);

        /// <summary>
        /// 插入数据返回自增ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> InsertReturnIdAsync(T model);

        /// <summary>
        /// 插入数据返回自增ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<long> InsertReturnLongIdAsync(T model);

        ///// <summary>
        ///// 根据SQL查询数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //Task<List<TT>> FindBySqlAsync<TT>(string sql) where TT : class, new();

        ///// <summary>
        ///// 根据SQL查询数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //Task<List<T>> FindBySqlAsync(string sql);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(object id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderByStr">排序方式</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        Task<List<T>> FindByPageAsync(string orderByStr, ref int totalCount, int pageIndex, int pageSize);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="orderByStr">排序方式</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        Task<List<T>> FindByPageAsync(Expression<Func<T, bool>> expression, string orderByStr, ref int totalCount, int pageIndex, int pageSize);

        /// <summary>
        /// 按条件查询数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<int> FindCountByFuncAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<T> FindFirstByFuncAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<List<T>> FindByFuncAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="select">指定列</param>
        /// <returns></returns>
        Task<List<T>> FindByFuncAsync(Expression<Func<T, bool>> expression, string select);

        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T model);

        /// <summary>
        /// 指定字段根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns">更新字段对象</param>
        /// <param name="expression">更新条件</param>
        /// <returns></returns>
        Task<int> UpdateAsync(dynamic columns, Expression<Func<T, bool>> expression);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(object id);

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> DeleteBatchAsync(object[] ids);

        /// <summary>
        /// 根据非主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">条件字段</param>
        /// <returns></returns>
        Task<int> DeleteBatchBySelfAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 根据非主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inField">条件字段</param>
        /// <param name="primaryKeyValues">包含的值</param>
        /// <returns></returns>
        Task<int> DeleteBatchBySelfAsync(Expression<Func<T, object>> inField, List<object> primaryKeyValues);

    }
}
