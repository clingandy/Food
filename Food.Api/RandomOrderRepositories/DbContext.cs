using Microsoft.Extensions.Options;
using RandomOrderCore.Config;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class DbContext<T> where T : class, new()
    {
        static MySqlConnectionConfig _mySqlConnectionConfig;

        public static SqlSugarClient Db
        {
            get => new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _mySqlConnectionConfig.MasterConncetString,
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            });
        }

        public DbContext(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs)
        {
            // 选择数据库
            _mySqlConnectionConfig = mySqlConnectionConfigs.Value.FirstOrDefault(t => t.MySqlConfigName == "wecaht_food") ?? new MySqlConnectionConfig();
            // 调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine($"{sql}\r\n{Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))}");
                Console.WriteLine();
            };

        }

        #region 异步基础操作

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> InsertAsync(T model)
        {
            return Db.Insertable(model).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public Task<int> InsertBatchAsync(List<T> models)
        {
            return Db.Insertable(models).ExecuteCommandAsync();
        }

        /// <summary>
        /// 插入数据返回自增ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> InsertReturnIdAsync(T model)
        {
            return Db.Insertable(model).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 插入数据返回自增ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<long> InsertReturnLongIdAsync(T model)
        {
            return Db.Insertable(model).ExecuteReturnBigIdentityAsync();
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<List<TT>> FindBySqlAsync<TT>(string sql) where TT : class, new()
        {
            return Db.SqlQueryable<TT>(sql).ToListAsync();
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<List<T>> FindBySqlAsync(string sql)
        {
            return Db.SqlQueryable<T>(sql).ToListAsync();
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> FindByIdAsync(object id)
        {
            return Db.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderByStr">排序方式</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public Task<List<T>> FindByPageAsync(string orderByStr, ref int totalCount, int pageIndex, int pageSize)
        {
            return Db.Queryable<T>().OrderBy(orderByStr).ToPageListAsync(pageIndex, pageSize, totalCount);
        }

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
        public Task<List<T>> FindByPageAsync(Expression<Func<T, bool>> expression, string orderByStr, ref int totalCount, int pageIndex, int pageSize)
        {
            return Db.Queryable<T>().Where(expression).OrderBy(orderByStr).ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// 按条件查询数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public Task<int> FindCountByFuncAsync(Expression<Func<T, bool>> expression)
        {
            return Db.Queryable<T>().Where(expression).CountAsync();
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public Task<T> FindFirstByFuncAsync(Expression<Func<T, bool>> expression)
        {
            return Db.Queryable<T>().Where(expression).FirstAsync();
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public Task<List<T>> FindByFuncAsync(Expression<Func<T, bool>> expression)
        {
            return Db.Queryable<T>().Where(expression).ToListAsync();
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <param name="select">指定列</param>
        /// <returns></returns>
        public Task<List<T>> FindByFuncAsync(Expression<Func<T, bool>> expression, string select)
        {
            return Db.Queryable<T>().Where(expression).Select(select).ToListAsync();
        }

        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> UpdateAsync(T model)
        {
            return Db.Updateable(model).ExecuteCommandAsync();
        }

        /// <summary>
        /// 指定字段根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns">更新字段对象</param>
        /// <param name="expression">更新条件</param>
        /// <returns></returns>
        public Task<int> UpdateAsync(dynamic columns, Expression<Func<T, bool>> expression)
        {
            return Db.Updateable<T>(columns).Where(expression).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(object id)
        {
            return Db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<int> DeleteBatchAsync(object[] ids)
        {
            return Db.Deleteable<T>().In(ids).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据非主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">条件字段</param>
        /// <returns></returns>
        public Task<int> DeleteBatchBySelfAsync(Expression<Func<T, bool>> expression)
        {
            return Db.Deleteable<T>().Where(expression).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据非主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inField">条件字段</param>
        /// <param name="primaryKeyValues">包含的值</param>
        /// <returns></returns>
        public Task<int> DeleteBatchBySelfAsync(Expression<Func<T, object>> inField, List<object> primaryKeyValues)
        {
            return Db.Deleteable<T>().In(inField, primaryKeyValues).ExecuteCommandAsync();
        }

        #endregion



    }
}
