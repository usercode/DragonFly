// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly.AspNetCore;

public static class BackgroundTaskContextExtensions
{
    public static async Task ProcessQueryAsync<T, TQuery>(this BackgroundTaskContext<TQuery> ctx, Func<TQuery, Task<Result<QueryResult<T>>>> queryAction, Func<T, Task> itemAction, int chunkSize = 100)
        where T : notnull
        where TQuery : QueryBase
    {
        ctx.Input.Skip = 0;
        ctx.Input.Take = chunkSize;

        int counter = 0;
        int counterSucceed = 0;
        int counterFailed = 0;

        while (true)
        {
            if (ctx.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            QueryResult<T> result = await queryAction(ctx.Input).ConfigureAwait(false);

            if (result.Items.Count == 0)
            {
                break;
            }

            foreach (T item in result.Items)
            {
                if (ctx.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await ctx.UpdateStatusAsync(item.ToString(), counter, result.TotalCount).ConfigureAwait(false);

                try
                {
                    await itemAction(item);

                    counterSucceed++;
                }
                catch
                {
                    counterFailed++;
                }
                finally
                {
                    counter++;
                }
            }

            ctx.Input.Skip += ctx.Input.Take;
        }

        await ctx.UpdateStatusAsync($"Succeed: {counterSucceed} / Failed: {counterFailed}", progressValue: counter).ConfigureAwait(false);
    }
}
