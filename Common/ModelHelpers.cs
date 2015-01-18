using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;
using CourseCenter.Models;
using System.Data.Entity.Infrastructure;

namespace CourseCenter.Common
{
    /// <summary>
    /// mssql数据库操作类，包含操作数据的泛型方法；
    /// </summary>
    public class ModelHelpers
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private DBEntities db = null;

        #region 0.0 静态构造函数
        /// <summary>
        /// 静态构造函数，初始化DBEntities；
        /// </summary>
        public ModelHelpers()
        {
            if (db == null)
                db = new DBEntities();
        }
        #endregion

        #region 1.0 新增 实体 +int Add<T>(T model)
        /// <summary>
        /// 新增 
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Add<T>(T model) where T : class,new()
        {
            db.Set<T>().Add(model);
            //保存成功后，会将自增的id设置给 model的 主键属性，并返回受影响行数
            return db.SaveChanges();
        }
        #endregion

        #region 2.0 根据实体对象删除 +int Del<T>(T model)
        /// <summary>
        /// 根据实体对象的id 删除
        /// </summary>
        /// <param name="model">包含要删除id的对象</param>
        /// <returns></returns>
        public int Del<T>(T model) where T : class,new()
        {
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
            //保存成功后，会将自增的id设置给 model的 主键属性，并返回受影响行数
            return db.SaveChanges();
        }
        #endregion

        #region 3.0 根据条件删除 +int DelBy<T>(Expression<Func<T, bool>> delWhere)
        /// <summary>
        /// 3.0 根据条件删除
        /// </summary>
        /// <param name="delWhere">条件的lambda表达式</param>
        /// <returns></returns>
        public int DelBy<T>(Expression<Func<T, bool>> delWhere) where T : class,new()
        {
            //3.1查询要删除的数据
            //此处lambda表达式的含义： delegate(T u){ return uName=="chen";}
            List<T> listDeleting = db.Set<T>().Where(delWhere).ToList();
            //3.2将要删除的数据 用删除方法添加到 EF 容器中
            listDeleting.ForEach(u =>
            {
                db.Set<T>().Attach(u);//先附加到 EF容器
                db.Set<T>().Remove(u);//标识为 删除 状态
            });
            //3.3一次性 生成sql语句到数据库执行删除
            //保存成功后，会将自增的id设置给 model的 主键属性，并返回受影响行数
            return db.SaveChanges();
        }
        #endregion

        //修改要尤其要注意：如果Model中添加了验证，在修改时传入的实体必须要符合验证，
        //例如，即使不用修改flag，但也要传入flag的值。解决方法可以关闭EF验证，可参考网络。

        #region 4.0 修改（以字段为单位） +int Modify<T>(T model, params string[] proNames)
        /// <summary>
        /// 4.0 修改，如：
        /// T u = new T() { uId = 1, uLoginName = "asdfasdf" };
        /// this.Modify(u, "uLoginName");方法调用时，proNames传入model对象的非ID属性；
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns>返回修改的行数</returns>
        public int Modify<T>(T model, params string[] proNames) where T : class,new()
        {
            //4.1将 对象 添加到 EF中
            DbEntityEntry entry = db.Entry<T>(model);
            //4.2先设置 对象的包装 状态为 Unchanged
            entry.State = System.Data.EntityState.Unchanged;
            //4.3循环 被修改的属性名 数组
            foreach (string proName in proNames)
            {
                //4.4将每个 被修改的属性的状态 设置为已修改状态;后面生成update语句时，就只为已修改的属性 更新
                entry.Property(proName).IsModified = true;
            }
            //4.4一次性 生成sql语句到数据库执行
            return db.SaveChanges();
        }
        #endregion

        #region 4.1 批量修改（以字段为单位） +int Modify<T>
        /// <summary>
        /// 4.1批量修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public int ModifyBy<T>(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames) where T : class,new()
        {
            //4.1.0查询要修改的数据
            List<T> listModifing = db.Set<T>().Where(whereLambda).ToList();

            //4.1.1获取 实体类 类型对象
            Type t = typeof(T); // model.GetType();
            //4.1.2获取所有的 实体类BindingFlags.Instance  公有BindingFlags.Public 属性
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            //创建 实体属性 字典集合
            Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
            //4.1.3将 实体属性 中要修改的属性名 添加到 字典集合中 键：属性名  值：属性对象
            proInfos.ForEach(p =>
            {
                if (modifiedProNames.Contains(p.Name))
                {
                    dictPros.Add(p.Name, p);
                }
            });

            //4.1.4循环 要修改的属性名
            foreach (string proName in modifiedProNames)
            {
                //判断 要修改的属性名是否在 实体类的属性集合中存在,
                //这一步可以省去，这里是做冗余判断，确保不会出错。
                if (dictPros.ContainsKey(proName))
                {
                    //如果存在，则取出要修改的 属性对象
                    PropertyInfo proInfo = dictPros[proName];
                    //取出 要修改的值
                    object newValue = proInfo.GetValue(model, null); //object newValue = model.uName;

                    //批量设置 要修改 对象的 属性
                    foreach (T tmodel in listModifing)
                    {
                        //为 要修改的对象 的 要修改的属性 设置新的值
                        proInfo.SetValue(tmodel, newValue, null); //tmodel.uName = newValue;
                    }
                }
            }
            //4.1.5一次性 生成sql语句到数据库执行
            return db.SaveChanges();
        }
        #endregion

        #region 4.2 修改 （根据model） +int Modify<T>(T model)
        /// <summary>
        /// 4.2 修改 需要传入model不为空的所有属性。
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <returns></returns>
        public int Modify<T>(T model) where T : class,new()
        {
            db.Entry<T>(model).State = System.Data.EntityState.Modified;
            return db.SaveChanges();
        }
        #endregion

        #region 5.0 根据条件查询 +List<T> GetListBy<T>(Expression<Func<T,bool>> whereLambda)
        /// <summary>
        /// 5.0 根据条件查询 +List<T> GetListBy(Expression<Func<T,bool>> whereLambda)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<T> GetListBy<T>(Expression<Func<T, bool>> whereLambda) where T : class,new()
        {
            return db.Set<T>().AsNoTracking().Where(whereLambda).ToList();
        }
        #endregion

        #region 5.1 根据条件 排序 和查询 + List<T>  GetListBy<T, TKey>
        /// <summary>
        /// 5.1 根据条件 排序 和查询
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetListBy<T, TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda) where T : class,new()
        {
            //根据第一个返回bool类型的lambda表达式 查询出集合。再根据TKey进行排序； 
            return db.Set<T>().AsNoTracking().Where(whereLambda).OrderBy(orderLambda).ToList();
        }
        #endregion

        #region 5.2 查询所有 + GetAll<T>
        /// <summary>
        /// 获得某一张表的所有数据
        /// </summary>
        public List<T> GetAll<T>() where T : class,new()
        {
            return db.Set<T>().AsNoTracking().ToList();
        }
        #endregion

        #region 5.3 根据条件查询单个model + GetModelBy<T>
        /// <summary>
        /// 根据条件查询单个model，一般是根据ID
        /// </summary>
        public object GetModelBy<T>(Expression<Func<T, bool>> whereLambda) where T : class,new()
        {
            return db.Set<T>().AsNoTracking().Where(whereLambda).FirstOrDefault();
        }
        #endregion

        #region 6.0 分页查询 + List<T> GetPagedList<T, TKey>
        /// <summary>
        /// 6.0 分页查询 + List<T> GetPagedList<TKey>
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件 lambda表达式</param>
        /// <param name="orderBy">排序 lambda表达式</param>
        /// <returns></returns>
        public List<T> GetPagedList<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy) where T : class,new()
        {
            // 分页 一定注意： Skip 之前一定要 OrderBy;
            return db.Set<T>().AsNoTracking().Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 7.0 复杂查询语句 +SqlQuery<T>(T model)
        /// <summary>
        /// 当对数据库进行复杂的sql语句查询语句的时候，调用的方法
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="paras">查询参数</param>
        public List<T> SqlQuery<T>(string strSql, params  SqlParameter[] paras)
        {
            return db.Database.SqlQuery<T>(strSql, paras).ToList();
        }
        #endregion

        #region 8.0 复杂非查询语句 +SqlNoquery
        /// <summary>
        /// 当对数据库进行复杂的sql非查询语句的时候，调用的方法
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="paras">非查询参数</param>
        public void SqlNoquery(string strSql, params SqlParameter[] paras)
        {
            db.Database.ExecuteSqlCommand(strSql, paras);
        }
        #endregion

    }   
}