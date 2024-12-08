using ChartDemo.Models;

namespace ChartDemo.Services
{



    /// <summary>
    /// Manages the loading and saving of bookmark data for chart states.
    /// </summary>
    public class Bookmark : IBookmark
    {
        /// <summary>
        /// The data loader used to load the chart state from storage.
        /// </summary>
        private readonly IDataLoader<ChartState> _dataLoader;

        /// <summary>
        /// The data saver used to save the chart state to storage.
        /// </summary>
        private readonly IDataSaver<ChartState> _dataSaver;

        /// <summary>
        /// The file name used to persist the chart state.
        /// </summary>
        private string FileName = "chartstate.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="Bookmark"/> class with custom data loader and saver.
        /// </summary>
        /// <param name="dataLoader">The data loader implementation for loading chart states.</param>
        /// <param name="dataSaver">The data saver implementation for saving chart states.</param>
        private Bookmark(IDataLoader<ChartState> dataLoader, IDataSaver<ChartState> dataSaver)
        {
            _dataLoader = dataLoader;
            _dataSaver = dataSaver;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bookmark"/> class with default data loader and saver implementations.
        /// </summary>
        public Bookmark()
        {
            this._dataLoader = new DataLoader<ChartState>();
            this._dataSaver = new DataSaver<ChartState>();
        }

        /// <summary>
        /// Loads the chart state from the application data directory.
        /// </summary>
        /// <returns>
        /// A <see cref="LoadResult{T}"/> containing the loaded chart state, success status, and a message.
        /// </returns>
        public async Task<LoadResult<ChartState>> LoadStateAsync()
        {
            return await _dataLoader.LoadJsonFromAppDataAsync(FileName);
        }

        /// <summary>
        /// Saves the specified chart state to the application data directory, overwriting any existing file.
        /// </summary>
        /// <param name="ChartState">The chart state to save.</param>
        /// <returns>
        /// A <see cref="SaveResult"/> containing the success status and a message indicating the result of the operation.
        /// </returns>
        public async Task<SaveResult> SaveStateAsync(ChartState ChartState)
        {
            return await _dataSaver.CreateOrOverwriteAsJsonAsync(ChartState, FileName);
        }
    }
}
