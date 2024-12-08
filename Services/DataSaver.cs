using System.Text.Json;

namespace ChartDemo.Services
{

    /// <summary>
    /// A generic class for saving data to a file in JSON format.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to save. Must be a reference type with a parameterless constructor.
    /// </typeparam>
    public class DataSaver<T> : IDataSaver<T> where T : class, new()
    {
        /// <summary>
        /// Asynchronously creates or overwrites a file with the specified object serialized as JSON.
        /// </summary>
        /// <param name="ObjectToSave">The object to save to the file.</param>
        /// <param name="FileName">The name of the file where the data will be saved.</param>
        /// <returns>
        /// A <see cref="SaveResult"/> indicating the success or failure of the save operation.
        /// </returns>
        public async Task<SaveResult> CreateOrOverwriteAsJsonAsync(T ObjectToSave, string FileName)
        {
            // Initialize a result object to track the outcome of the save operation.
            var result = new SaveResult();

            // Combine the application's data directory path with the specified file name.
            string filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

            // Serialize the object to a JSON string.
            string json = JsonSerializer.Serialize(ObjectToSave);

            // Attempt to write the JSON string to the specified file.
            // This implementation ensures the file is always overwritten.
            try
            {
                await File.WriteAllTextAsync(filePath, json); // Write JSON to the file asynchronously.
                result.Message = "Save complete"; // Indicate success in the result message.
                result.Success = true; // Set the success flag to true.
            }
            catch (Exception ex) // Catch any exceptions that occur during the write process.
            {
                // Update the result object with an error message containing the exception details.
                result.Message = $"Writing failed for file {FileName} with message {ex.Message}";
            }

            // Return the result of the save operation.
            return result;
        }
    }

    /// <summary>
    /// Represents the result of a save operation.
    /// </summary>
    public class SaveResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the save operation was successful.
        /// </summary>
        public bool Success = false;

        /// <summary>
        /// Gets or sets a message describing the result of the save operation (e.g., success or error details).
        /// </summary>
        public string Message = "";
    }
}
