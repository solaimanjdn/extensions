﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Utilities;
using System.Linq.Expressions;
using Signum.Utilities.ExpressionTrees;

namespace Signum.Entities.Basics
{
    [Serializable]
    public class DateSpanEntity : EmbeddedEntity
    {
        public int Years { get; set; }

        public int Months { get; set; }

        public int Days { get; set; }

        public bool IsZero()
        {
            return Years == 0 && Months == 0 && Days == 0;
        }

        static Expression<Func<DateSpanEntity, DateTime, DateTime>> AddExpression =
             (ds, dt) => dt.AddYears(ds.Years).AddMonths(ds.Months).AddDays(ds.Days);
        [ExpressionField]
        public DateTime Add(DateTime date)
        {
            return AddExpression.Evaluate(this, date);
        }

        static Expression<Func<DateSpanEntity, DateTime, DateTime>> SubtractExpression =
           (ds, dt) => dt.AddYears(-ds.Years).AddMonths(-ds.Months).AddDays(-ds.Days);
        [ExpressionField]
        public DateTime Subtract(DateTime date)
        {
            return SubtractExpression.Evaluate(this, date);
        }

        public DateSpan ToDateSpan()
        {
            return new DateSpan(Years, Months, Days);
        }

        public override string ToString()
        {
            return ToDateSpan().ToString();
        }

        public DateSpanEntity Clone()
        {

            DateSpanEntity ds = new DateSpanEntity
            {
                Days = this.Days,
                Months = this.Months,
                Years = this.Years,
            };

            return ds;
        }
    }

    [Serializable]
    public class TimeSpanEntity : EmbeddedEntity
    {
        public int Days { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public bool IsZero()
        {
            return Days == 0 && Hours == 0 && Minutes == 0  && Seconds == 0;
        }

        static Expression<Func<TimeSpanEntity, DateTime, DateTime>> AddExpression =
             (ds, dt) => dt.AddDays(ds.Days).AddHours(ds.Hours).AddMinutes(ds.Minutes).AddMinutes(ds.Seconds);
        [ExpressionField]
        public DateTime Add(DateTime date)
        {
            return AddExpression.Evaluate(this, date);
        }

        static Expression<Func<TimeSpanEntity, DateTime, DateTime>> SubtractExpression =
           (ds, dt) => dt.AddDays(-ds.Days).AddHours(-ds.Hours).AddMinutes(-ds.Minutes).AddMinutes(-ds.Seconds);
        [ExpressionField]
        public DateTime Subtract(DateTime date)
        {
            return SubtractExpression.Evaluate(this, date);
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(Days, Hours, Minutes, Seconds);
        }

        public override string ToString()
        {
            return ToTimeSpan().ToString();
        }

        public TimeSpanEntity Clone()
        {
            TimeSpanEntity ds = new TimeSpanEntity
            {
                Days = this.Days,
                Hours = this.Hours,
                Minutes = this.Minutes,
                Seconds = this.Seconds,
            };

            return ds;
        }
    }
}
