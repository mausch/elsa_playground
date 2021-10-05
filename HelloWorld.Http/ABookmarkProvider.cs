using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;

namespace HelloWorld.Http
{
    public abstract class ABookmarkProvider<TBookmark, TActivity> : BookmarkProvider<TBookmark, TActivity> 
        where TBookmark : IBookmark
        where TActivity : IActivity 
    {
        public sealed override bool SupportsActivity(BookmarkProviderContext<TActivity> context) => base.SupportsActivity(context);
        public sealed override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<TActivity> context) => base.GetBookmarks(context);
        public sealed override ValueTask<bool> SupportsActivityAsync(BookmarkProviderContext<TActivity> context, CancellationToken cancellationToken = new()) => base.SupportsActivityAsync(context, cancellationToken);

        protected abstract ValueTask<IReadOnlyList<BookmarkResult<TBookmark>>> GetBookmarksAsyncT(BookmarkProviderContext<TActivity> context, CancellationToken cancellationToken);

        public override async ValueTask<IEnumerable<BookmarkResult>> GetBookmarksAsync(BookmarkProviderContext<TActivity> context, CancellationToken cancellationToken)
        {
            return await GetBookmarksAsyncT(context, cancellationToken);
        }
    }
}