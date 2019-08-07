using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RandomOrderCore.Common
{
    /// <summary>
    /// 对象拼接sql语句
    /// </summary>
    public class SqlBuilderHelper
    {
        private static ConcurrentDictionary<string, string> dicColmon = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Insert SQL语句
        /// </summary>
        /// <param name="obj">要转换的对象，不可空</param>
        /// <param name="tableName">要添加的表明，不可空</param>
        /// <returns>
        /// 空
        /// sql语句
        /// </returns>
        public static string InsertSql<T>(T t, string tableName) where T : class
        {
            if (t == null || string.IsNullOrEmpty(tableName))
            {
                return string.Empty;
            }
            string columns = GetColmons(t);
            if (string.IsNullOrEmpty(columns))
            {
                return string.Empty;
            }
            string values = GetValues(t);
            if (string.IsNullOrEmpty(values))
            {
                return string.Empty;
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert into " + tableName);
            sql.Append("(" + columns + ")");
            sql.Append(" values(" + values + ")");
            return sql.ToString();
        }

        /// <summary>
        /// BulkInsert SQL语句（批量添加）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objs">要转换的对象集合，不可空</param>
        /// <param name="tableName">>要添加的表明，不可空</param>
        /// <returns>
        /// 空
        /// sql语句
        /// </returns>
        public static string BulkInsertSql<T>(List<T> objs, string tableName) where T : class
        {
            if (objs == null || objs.Count == 0 || string.IsNullOrEmpty(tableName))
            {
                return string.Empty;
            }
            string columns = GetColmons(objs[0]);
            if (string.IsNullOrEmpty(columns))
            {
                return string.Empty;
            }
            string values = string.Join(",", objs.Select(p => string.Format("({0})", GetValues(p))).ToArray());
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert into " + tableName);
            sql.Append("(" + columns + ")");
            sql.Append(" values " + values + "");
            return sql.ToString();
        }

        /// <summary>
        /// 获得类型的列名
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetColmons<T>(T obj)
        {
            var key = $"SqlBuilderHelper_GetColmons_{typeof(T).Name}".ToLower();
            if (dicColmon.Keys.Contains(key))
            {
                return dicColmon[key];
            }
            else
            {
                if (obj == null)
                {
                    return string.Empty;
                }
                var list = obj.GetType().GetProperties().Select(p => ConvertLower(p.Name)).ToList();
                dicColmon[key] = string.Join(",", list);
                return dicColmon[key];
            }
        }

        /// <summary>
        /// 大驼峰格式转化
        /// 首字母小写，大写转小写，并在前面添加parttern
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        private static string ConvertLower(string str, string parttern = "_")
        {
            if (str == null)
            {
                return str;
            }
            if (str.Length == 1)
            {
                return str.Substring(0, 1).ToLower();
            }
            str = str.Substring(0, 1).ToLower() + str.Substring(1, str.Length - 1);
            str = Regex.Replace(str, @"([A-Z])", r => $"_{r.Value.ToLower()}");
            return str;
        }

        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetValues<T>(T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return string.Join(",", obj.GetType().GetProperties().Select(p => string.Format("'{0}'", p.GetValue(obj).ToString().Replace(@"\", @"\\").Replace("'", @"\'"))).ToArray());
        }
    }
}
