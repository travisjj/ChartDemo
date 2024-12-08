using ChartDemo.Models;

namespace ChartDemo.Services
{
    /// <summary>
    /// Defines methods for saving and loading chart state bookmarks.
    /// </summary>
    internal interface IBookmark
    {
        /// <summary>
        /// Loads the saved chart state from storage.
        /// </summary>
        /// <returns>
        /// A <see cref="LoadResult{ChartState}"/> containing the loaded chart state and the result of the operation.
        /// </returns>
        Task<LoadResult<ChartState>> LoadStateAsync();

        /// <summary>
        /// Saves the given chart state to storage.
        /// </summary>
        /// <param name="ChartState">The <see cref="ChartState"/> to save.</param>
        /// <returns>
        /// A <see cref="SaveResult"/> indicating the success or failure of the save operation.
        /// </returns>
        Task<SaveResult> SaveStateAsync(ChartState ChartState);
    }
}
