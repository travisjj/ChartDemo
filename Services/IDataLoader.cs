namespace ChartDemo.Services
{
    /// <summary>
    /// Defines methods for loading data of type <typeparamref name="T"/> from various sources.
    /// </summary>
    /// <typeparam name="T">The type of data to be loaded. Must be a reference type with a parameterless constructor.</typeparam>
    internal interface IDataLoader<T> where T : class, new()
    {
        /// <summary>
        /// Loads JSON data of type <typeparamref name="T"/> from the application package.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        /// <returns>
        /// A <see cref="LoadResult{T}"/> containing the loaded data and the result of the operation.
        /// </returns>
        Task<LoadResult<T>> LoadJsonFromPackageAsync(string filename);

        /// <summary>
        /// Loads JSON data of type <typeparamref name="T"/> from the application data directory.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        /// <returns>
        /// A <see cref="LoadResult{T}"/> containing the loaded data and the result of the operation.
        /// </returns>
        Task<LoadResult<T>> LoadJsonFromAppDataAsync(string filename);
    }
}
