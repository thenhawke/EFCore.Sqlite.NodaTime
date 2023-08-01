using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Sqlite.Query.ExpressionTranslators.Internal
{
    public class SqliteNodaTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public SqliteNodaTimeMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new SqliteNodaTimeDateDiffFunctionsTranslator(sqlExpressionFactory),
                new SqliteNodaTimeMethodCallTranslator(sqlExpressionFactory),
                new SqliteNodaTimeInstantTranslator(sqlExpressionFactory),
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
