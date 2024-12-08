using ChartDemo.Services;
using ChartDemo.Models;

namespace ChartDemo.ViewModels
{
    /// <summary>
    /// ViewModel for managing chart data and related operations.
    /// </summary>
    public class ChartDataVM
    {
        /// <summary>
        /// The result of loading employment data, including the loaded data, a success flag, and a message.
        /// </summary>
        public LoadResult<EmploymentData> LoadResult { get; set; }

        private EmploymentData employmentData { get { return this.LoadResult.Data; } }
        private IList<string> names { get { return this.employmentData.Names; } }
        private IList<IList<float>> values { get { return this.employmentData.Values; } }

        private readonly IDataLoader<EmploymentData> _dataLoader;
        private readonly Bookmark _bookmark;

        /// <summary>
        /// Initializes a new instance of <see cref="ChartDataVM"/> with specific data loader and bookmark dependencies.
        /// </summary>
        /// <param name="dataLoader">The data loader for loading employment data.</param>
        /// <param name="bookmark">The bookmark for saving and loading chart states.</param>
        private ChartDataVM(IDataLoader<EmploymentData> dataLoader, Bookmark bookmark)
        {
            this._dataLoader = dataLoader;
            this._bookmark = bookmark;
            this.LoadResult = new LoadResult<EmploymentData>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ChartDataVM"/> with default dependencies.
        /// </summary>
        public ChartDataVM()
        {
            this._dataLoader = new DataLoader<EmploymentData>();
            this._bookmark = new Bookmark();
            this.LoadResult = new LoadResult<EmploymentData>();
        }

        /// <summary>
        /// Prepares employment data by asynchronously loading it from a JSON file.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PrepareData()
        {
            this.LoadResult = await _dataLoader.LoadJsonFromPackageAsync("employmentdata.json");
        }

        /// <summary>
        /// Builds chart data for a specific employment category by name.
        /// </summary>
        /// <param name="name">The name of the employment category.</param>
        /// <returns>A <see cref="ChartData{T}"/> object populated with data for the specified category.</returns>
        public ChartData<float> BuildChartData(string name)
        {
            var data = new ChartData<float>(1);
            data.labels = employmentData.DateSet.Select(d => d.ToString()).ToArray();
            data.datasets[0] = new ChartDataSet<float>(name, employmentData.ValuesByHeader(name));

            return data;
        }

        /// <summary>
        /// Builds chart data for multiple employment categories by their names.
        /// </summary>
        /// <param name="names">A list of employment category names.</param>
        /// <returns>A <see cref="ChartData{T}"/> object populated with data for the specified categories.</returns>
        public ChartData<float> BuildChartData(List<string> names)
        {
            var data = new ChartData<float>(names.Count);
            data.labels = employmentData.DateSet.Select(d => d.ToString()).ToArray();
            for (int i = 0; i < names.Count; i++)
            {
                data.datasets[i] = new ChartDataSet<float>(names[i], employmentData.ValuesByHeader(names[i]));
            }
            return data;
        }

        /// <summary>
        /// Asynchronously loads the bookmark state for the chart.
        /// </summary>
        /// <returns>A task containing a <see cref="LoadResult{ChartState}"/> with the loaded bookmark state.</returns>
        public async Task<LoadResult<ChartState>> LoadBookmarkAsync()
        {
            return await _bookmark.LoadStateAsync();
        }

        /// <summary>
        /// Asynchronously saves the given chart state as a bookmark.
        /// </summary>
        /// <param name="chartState">The chart state to save.</param>
        /// <returns>A task containing a <see cref="SaveResult"/> indicating the result of the save operation.</returns>
        public async Task<SaveResult> SaveBookmarkAsync(ChartState chartState)
        {
            return await _bookmark.SaveStateAsync(chartState);
        }
    }

    /// <summary>
    /// Represents chart data, including labels and datasets.
    /// </summary>
    /// <typeparam name="T">The type of the data in the chart.</typeparam>
    public class ChartData<T>
    {
        /// <summary>
        /// The labels for the chart (e.g., time periods or categories).
        /// </summary>
        public string[]? labels;

        /// <summary>
        /// The datasets for the chart, each representing a series of data points.
        /// </summary>
        public ChartDataSet<T>[] datasets;

        /// <summary>
        /// Initializes a new instance of <see cref="ChartData{T}"/> with the specified number of datasets.
        /// </summary>
        /// <param name="SetCount">The number of datasets in the chart.</param>
        public ChartData(int SetCount)
        {
            var chartDataSet = new ChartDataSet<T>[SetCount];
            this.datasets = chartDataSet;
        }
    }

    /// <summary>
    /// Represents a single dataset in a chart.
    /// </summary>
    /// <typeparam name="T">The type of the data in the dataset.</typeparam>
    public class ChartDataSet<T>
    {
        /// <summary>
        /// The label for the dataset (e.g., a category name).
        /// </summary>
        public string label;

        /// <summary>
        /// The data points for the dataset.
        /// </summary>
        public T[] data;

        /// <summary>
        /// Initializes a new instance of <see cref="ChartDataSet{T}"/> with the specified label and data.
        /// </summary>
        /// <param name="label">The label for the dataset.</param>
        /// <param name="data">The data points for the dataset.</param>
        public ChartDataSet(string label, T[] data)
        {
            this.label = label;
            this.data = data;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ChartDataSet{T}"/> with the specified label and data from a list.
        /// </summary>
        /// <param name="label">The label for the dataset.</param>
        /// <param name="data">The data points for the dataset as a list.</param>
        public ChartDataSet(string label, List<T> data)
        {
            this.label = label;
            this.data = data.ToArray();
        }
    }
}
