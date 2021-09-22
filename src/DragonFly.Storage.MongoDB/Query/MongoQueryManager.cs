using DragonFly.Content;
using DragonFly.Storage.MongoDB.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query
{
    /// <summary>
    /// MongoQueryManager
    /// </summary>
    public class MongoQueryManager
    {
        private static MongoQueryManager? _default;

        public static MongoQueryManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new MongoQueryManager();

                    _default.Register<StringQuery, StringQueryAction>();
                    _default.Register<IntegerQuery, IntegerQueryAction>();
                    _default.Register<ReferenceQuery, ReferenceQueryAction>();
                }

                return _default;
            }
        }

        private IDictionary<Type, IQueryAction> _fields;

        public MongoQueryManager()
        {
            _fields = new Dictionary<Type, IQueryAction>();
        }

        public void Register(Type fieldType, IQueryAction queryConverter)
        {
            _fields.Add(fieldType, queryConverter);
        }

        public void Register<TQuery, TQueryConverter>()
            where TQuery : FieldQuery
            where TQueryConverter : QueryAction<TQuery>, new()
        {
            Register(typeof(TQuery), new TQueryConverter());
        }

        public IQueryAction GetByType(Type fieldType)
        {
            if (_fields.TryGetValue(fieldType, out IQueryAction? queryConverter))
            {
                return queryConverter;
            }

            throw new Exception($"Field serializer not found: {fieldType.Name}");
        }
    }
}
