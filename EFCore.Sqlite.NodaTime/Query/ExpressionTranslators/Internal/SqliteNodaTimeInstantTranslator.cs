using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NodaTime;

namespace Microsoft.EntityFrameworkCore.Sqlite.Query.ExpressionTranslators.Internal
{
    public class SqliteNodaTimeInstantTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _toDateTimeUtc =
            typeof(Instant).GetRuntimeMethod(nameof(Instant.ToDateTimeUtc), Type.EmptyTypes)!;

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public SqliteNodaTimeInstantTranslator(ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public SqlExpression? Translate(
            SqlExpression? instance,
            MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            return method == _toDateTimeUtc
                ? _sqlExpressionFactory.Function(
            "datetime",
            new[] { instance! },
            nullable: true,
            argumentsPropagateNullability: new[] { true },
            typeof(DateTime))
                : (SqlExpression?)null;
        }
    }
}