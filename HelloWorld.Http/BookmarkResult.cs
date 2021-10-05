using Elsa.Services;

namespace HelloWorld.Http
{
    public record BookmarkResult<TBookmark>: BookmarkResult
        where TBookmark: IBookmark
    {
        public BookmarkResult(TBookmark Bookmark, string? ActivityTypeName = null) : base(Bookmark, ActivityTypeName)
        {
        }
    }
}