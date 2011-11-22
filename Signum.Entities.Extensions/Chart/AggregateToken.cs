﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Entities.DynamicQuery;
using Signum.Entities.Chart;
using Signum.Utilities;
using System.Linq.Expressions;

namespace Signum.Entities.Chart
{
    [Serializable]
    public class AggregateToken : QueryToken
    {
        public AggregateFunction AggregateFunction { get; private set; }

        public AggregateToken(QueryToken parent, AggregateFunction function)
            : base(parent)
        {
            if (function == AggregateFunction.Count)
            {
                if (parent != null)
                    throw new ArgumentException("parent should be null for Count function"); 
            }
            else
            {

                if (parent != null)
                    throw new ArgumentNullException("parent");
            }

            this.AggregateFunction = function;
        }

        public override string ToString()
        {
            return AggregateFunction.NiceToString();
        }

        public override string NiceName()
        {
            if(AggregateFunction == AggregateFunction.Count)
                return AggregateFunction.NiceToString();

            return "{0} of {1}".Formato(AggregateFunction.NiceToString(), Parent.ToString());  
        }

        public override string Format
        {
            get
            {
                if (AggregateFunction == AggregateFunction.Count)
                    return null;
                return Parent.Format;
            }
        }

        public override string Unit
        {
            get
            {
                if (AggregateFunction == AggregateFunction.Count)
                    return null;
                return Parent.Unit;
            }
        }

        public override Type Type
        {
            get
            {
                if (AggregateFunction == AggregateFunction.Count)
                    return typeof(int);

                var pType = Parent.Type;

                if (AggregateFunction == AggregateFunction.Average && 
                    (pType.UnNullify() == typeof(int) || 
                     pType.UnNullify() == typeof(long) ||
                     pType.UnNullify() == typeof(bool)))
                {
                    return pType.IsNullable() ? typeof(double) : typeof(double?);
                }

                return pType;
            }
        }

        public override string Key
        {
            get { return AggregateFunction.ToString(); }
        }

        protected override List<QueryToken> SubTokensInternal()
        {
            return new List<QueryToken>();
        }

        protected override Expression BuildExpressionInternal(BuildExpressionContext context)
        {
            throw new InvalidOperationException("AggregateToken does not support this method");
        }

        public override PropertyRoute GetPropertyRoute()
        {
            if (AggregateFunction == AggregateFunction.Count)
                return null;

            return Parent.GetPropertyRoute(); 
        }

        public override Implementations Implementations()
        {
            return null;
        }

        public override bool IsAllowed()
        {
            return Parent.IsAllowed();
        }

        public override QueryToken Clone()
        {
            if (AggregateFunction == AggregateFunction.Count)
                return new AggregateToken(null, AggregateFunction.Count);
            else
                return new AggregateToken(Parent.Clone(), AggregateFunction.Count);
        }
    }
}
