namespace ChartDemo.Models
{
    /// <summary>
    /// Represents the state of a chart, including zoom levels and selected data.
    /// </summary>
    public class ChartState
    {
        /// <summary>
        /// The minimum value for the x-axis.
        /// </summary>
        public float xmin { get; set; }

        /// <summary>
        /// The maximum value for the x-axis.
        /// </summary>
        public float xmax { get; set; }

        /// <summary>
        /// The minimum value for the y-axis.
        /// </summary>
        public float ymin { get; set; }

        /// <summary>
        /// The maximum value for the y-axis.
        /// </summary>
        public float ymax { get; set; }

        /// <summary>
        /// The selected data labels.
        /// </summary>
        public string[]? Selections { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartState"/> class.
        /// </summary>
        public ChartState() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartState"/> class with the specified axis boundaries.
        /// </summary>
        /// <param name="xmin">The minimum x-axis value.</param>
        /// <param name="xmax">The maximum x-axis value.</param>
        /// <param name="ymin">The minimum y-axis value.</param>
        /// <param name="ymax">The maximum y-axis value.</param>
        public ChartState(float xmin, float xmax, float ymin, float ymax)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartState"/> class with the specified axis boundaries and data selections.
        /// </summary>
        /// <param name="xmin">The minimum x-axis value.</param>
        /// <param name="xmax">The maximum x-axis value.</param>
        /// <param name="ymin">The minimum y-axis value.</param>
        /// <param name="ymax">The maximum y-axis value.</param>
        /// <param name="Selections">The selected data labels.</param>
        public ChartState(float xmin, float xmax, float ymin, float ymax, string[] Selections)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            this.Selections = Selections;
        }
    }
}
