namespace ChartDemo.Services
{
    /// <summary>
    /// Defines methods for saving data to storage.
    /// </summary>
    /// <typeparam name="T">The type of data to save.</typeparam>
    internal interface IDataSaver<T> where T : class, new()
    {
        /// <summary>
        /// Saves the specified object as JSON to a file, creating or overwriting the file.
        /// </summary>
        /// <param name="ObjectToSave">The object to serialize and save.</param>
        /// <param name="FileName">The name of the file to save the data in.</param>
        /// <returns>
        /// A <see cref="SaveResult"/> indicating the success or failure of the save operation.
        /// </returns>
        Task<SaveResult> CreateOrOverwriteAsJsonAsync(T ObjectToSave, string FileName);
    }
}
