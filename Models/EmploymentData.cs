namespace ChartDemo.Models
{

    /// <summary>
    /// Represents employment data, including column names and corresponding values.
    /// </summary>
    public class EmploymentData
    {
        /// <summary>
        /// Gets or sets the column names in the dataset (e.g., "year", "month", "state").
        /// </summary>
        public List<string> Names { get; set; }

        /// <summary>
        /// Gets or sets the dataset values, organized as rows of numeric data.
        /// </summary>
        public List<IList<float>> Values { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmploymentData"/> class.
        /// </summary>
        public EmploymentData()
        {
            this.Names = new();
            this.Values = new();
        }

        /// <summary>
        /// Retrieves an array of numeric values from the dataset for the specified column name.
        /// </summary>
        /// <param name="ColumnName">The name of the column to retrieve values for.</param>
        /// <returns>An array of floats corresponding to the specified column.</returns>
        public float[] ValuesByHeader(string ColumnName)
        {
            var values = new List<float>();
            int idx = this.Names.IndexOf(ColumnName);
            if (idx == -1) { return values.ToArray(); }

            for (int row = 0; row < this.Values.Count; row++)
            {
                IList<float> curr = this.Values[row];
                values.Add(curr[idx]);
            }
            return values.ToArray();
        }

        /// <summary>
        /// Retrieves a list of integer values (e.g., years or months) from the dataset for the specified column name.
        /// </summary>
        /// <param name="ColumnName">The name of the column to retrieve date-related values for.</param>
        /// <returns>A list of integers corresponding to the specified column.</returns>
        public List<int> DatesByHeader(string ColumnName)
        {
            var values = new List<int>();
            int idx = this.Names.IndexOf(ColumnName);
            if (idx == -1) { return values; }

            for (int row = 0; row < this.Values.Count; row++)
            {
                IList<float> curr = this.Values[row];
                values.Add((int)curr[idx]);
            }
            return values;
        }

        private IEnumerable<DateOnly>? _dateSet;

        /// <summary>
        /// Retrieves a set of dates constructed from "year" and "month" columns in the dataset.
        /// </summary>
        /// <remarks>
        /// Assumes "year" and "month" columns exist in the dataset. Each date is represented as the first day of the month.
        /// </remarks>
        public IEnumerable<DateOnly> DateSet
        {
            get
            {
                if (_dateSet is null)
                {
                    var years = DatesByHeader("year");
                    var months = DatesByHeader("month");

                    this._dateSet = Enumerable.Zip(years, months, (y, m) => new DateOnly(y, m, 1));
                }

                return _dateSet;
            }
        }
    }
}
