﻿using System;
using System.Threading.Tasks;
using Optional;

namespace MyProject.Business.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T, TException> SomeWhen<T, TException>(
            this T value,
            Func<T, bool> predicate,
            Func<T, TException> exceptionFactory)
        {
            var result = predicate(value);

            return result ?
                value.Some<T, TException>() :
                Option.None<T, TException>(exceptionFactory(value));
        }

        public static async Task<Option<T>> FilterAsync<T>(this Option<T> option, Func<T, Task<bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var value in option)
            {
                var predicateTask = predicate(value);

                if (predicateTask == null)
                    throw new InvalidOperationException("Predicate must not return a null task.");

                var condition = await predicateTask.ConfigureAwait(continueOnCapturedContext: false);
                return option.Filter(condition);
            }

            return Option.None<T>();
        }
    }
}
