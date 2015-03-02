namespace System
{
    #region Using Directives

    using System.IO;
    using System.Reflection;

    #endregion

    public static class PathExtensions
    {
        public static string ResolveBaseDirectory(this string path)
        {
            return Path.GetFullPath(Path.Combine(GetBaseDirectory(), path));
        }

        public static string GetBaseDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);

            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }
    }
}