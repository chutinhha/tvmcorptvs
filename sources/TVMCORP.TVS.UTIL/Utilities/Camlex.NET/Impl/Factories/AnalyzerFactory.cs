﻿#region Copyright(c) Alexey Sadomov, Vladimir Timashkov. All Rights Reserved.
// -----------------------------------------------------------------------------
// Copyright(c) 2010 Alexey Sadomov, Vladimir Timashkov. All Rights Reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
//   1. No Trademark License - Microsoft Public License (Ms-PL) does not grant you rights to use
//      authors names, logos, or trademarks.
//   2. If you distribute any portion of the software, you must retain all copyright,
//      patent, trademark, and attribution notices that are present in the software.
//   3. If you distribute any portion of the software in source code form, you may do
//      so only under this license by including a complete copy of Microsoft Public License (Ms-PL)
//      with your distribution. If you distribute any portion of the software in compiled
//      or object code form, you may only do so under a license that complies with
//      Microsoft Public License (Ms-PL).
//   4. The names of the authors may not be used to endorse or promote products
//      derived from this software without specific prior written permission.
//
// The software is licensed "as-is." You bear the risk of using it. The authors
// give no express warranties, guarantees or conditions. You may have additional consumer
// rights under your local laws which this license cannot change. To the extent permitted
// under your local laws, the authors exclude the implied warranties of merchantability,
// fitness for a particular purpose and non-infringement.
// -----------------------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.AndAlso;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Array;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.BeginsWith;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Contains;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.DateRangesOverlap;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Eq;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Geq;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Gt;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.IsNotNull;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.IsNull;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Leq;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Lt;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.Neq;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Operations.OrElse;
using TVMCORP.TVS.UTIL.Utilities.Camlex.Interfaces;

namespace TVMCORP.TVS.UTIL.Utilities.Camlex.Impl.Factories
{
    internal class AnalyzerFactory : IAnalyzerFactory
    {
        private readonly IOperandBuilder operandBuilder;
        private readonly IOperationResultBuilder operationResultBuilder;

        public AnalyzerFactory(IOperandBuilder operandBuilder, IOperationResultBuilder operationResultBuilder)
        {
            this.operandBuilder = operandBuilder;
            this.operationResultBuilder = operationResultBuilder;
        }

        public IAnalyzer Create(LambdaExpression expr)
        {
            ExpressionType exprType = expr.Body.NodeType;

            if (exprType == ExpressionType.AndAlso)
            {
                return new AndAlsoAnalyzer(this.operationResultBuilder, this);
            }
            if (exprType == ExpressionType.OrElse)
            {
                return new OrElseAnalyzer(this.operationResultBuilder, this);
            }
            if (exprType == ExpressionType.NewArrayInit)
            {
                return new ArrayAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }
            if (exprType == ExpressionType.GreaterThanOrEqual)
            {
                return new GeqAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }
            if (exprType == ExpressionType.GreaterThan)
            {
                return new GtAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }
            if (exprType == ExpressionType.LessThanOrEqual)
            {
                return new LeqAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }
            if (exprType == ExpressionType.LessThan)
            {
                return new LtAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }

            // it is not enough to check ExpressionType for IsNull operation.
            // We need also to check that right operand is null
            IsNullAnalyzer isNullAnalyzer;
            if (this.isNullExpression(expr, out isNullAnalyzer))
            {
                return isNullAnalyzer;
            }
            // note that it is important to have check on IsNull before check on ExpressionType.Equal.
            // Because x["foo"] == null is also ExpressionType.Equal, but it should be translated
            // into <IsNull> instead of <Eq>
            if (exprType == ExpressionType.Equal)
            {
                return new EqAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }

            // it is not enough to check ExpressionType for IsNotNull operation.
            // We need also to check that right operand is null
            IsNotNullAnalyzer isNotNullAnalyzer;
            if (this.isNotNullExpression(expr, out isNotNullAnalyzer))
            {
                return isNotNullAnalyzer;
            }
            // note that it is important to have check on IsNotNull before check on ExpressionType.NotEqual.
            // Because x["foo"] != null is also ExpressionType.NotEqual, but it should be translated
            // into <IsNotNull> instead of <Neq>
            if (exprType == ExpressionType.NotEqual)
            {
                return new NeqAnalyzer(this.operationResultBuilder, this.operandBuilder);
            }

            var beginsWithAnalyzer = new BeginsWithAnalyzer(operationResultBuilder, operandBuilder);
            if (beginsWithAnalyzer.IsValid(expr)) return beginsWithAnalyzer;

            var containsAnalyzer = new ContainsAnalyzer(operationResultBuilder, operandBuilder);
            if (containsAnalyzer.IsValid(expr)) return containsAnalyzer;

            var dateRangesOverlapAnalyzer = new DateRangesOverlapAnalyzer(operationResultBuilder, operandBuilder);
            if (dateRangesOverlapAnalyzer.IsValid(expr)) return dateRangesOverlapAnalyzer;

            throw new NonSupportedExpressionTypeException(exprType);
        }

        private bool isNullExpression(LambdaExpression expr, out IsNullAnalyzer analyzer)
        {
            // the simplest way to check if this IsNotNull expression - is to reuse IsNotNullAnalyzer
            analyzer = new IsNullAnalyzer(this.operationResultBuilder, this.operandBuilder);
            return analyzer.IsValid(expr);
        }

        private bool isNotNullExpression(LambdaExpression expr, out IsNotNullAnalyzer analyzer)
        {
            // the simplest way to check if this IsNotNull expression - is to reuse IsNotNullAnalyzer
            analyzer = new IsNotNullAnalyzer(this.operationResultBuilder, this.operandBuilder);
            return analyzer.IsValid(expr);
        }
    }
}
