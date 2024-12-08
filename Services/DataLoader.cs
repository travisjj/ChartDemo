using System.Text.Json;

namespace ChartDemo.Services
{

    /// <summary>
    /// A generic class for loading data from JSON files, either from the app package or app data directory.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to load. Must be a reference type with a parameterless constructor.
    /// </typeparam>
    public class DataLoader<T> : IDataLoader<T> where T : class, new()
    {
        /// <summary>
        /// Asynchronously loads a JSON file from the app package and deserializes it into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="FileName">The name of the JSON file to load.</param>
        /// <returns>
        /// A <see cref="LoadResult{T}"/> containing the deserialized data, a success flag, and a message.
        /// </returns>
        public async Task<LoadResult<T>> LoadJsonFromPackageAsync(string FileName)
        {
            var result = new LoadResult<T>();

            // Check if the file exists in the app package.
            bool FileExists = await FileSystem.AppPackageFileExistsAsync(FileName);
            if (FileExists)
            {
                try
                {
                    // Open the file and deserialize its content.
                    using var stream = await FileSystem.OpenAppPackageFileAsync(FileName);
                    T? data = await JsonSerializer.DeserializeAsync<T>(stream);
                    if (data is not null)
                    {
                        result.Data = data; // Set the deserialized data.
                        result.Message = "Load complete";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "No load data was present";
                    }
                }
                catch (Exception ex) // Handle deserialization exceptions.
                {
                    result.Message = $"Parsing failed for file {FileName} with message {ex.Message}";
                }
            }
            else
            {
                result.Message = $"File not found: {FileName}";
            }

            return result;
        }

        /// <summary>
        /// Asynchronously loads a JSON file from the app data directory and deserializes it into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="FileName">The name of the JSON file to load.</param>
        /// <returns>
        /// A <see cref="LoadResult{T}"/> containing the deserialized data, a success flag, and a message.
        /// </returns>
        public async Task<LoadResult<T>> LoadJsonFromAppDataAsync(string FileName)
        {
            var result = new LoadResult<T>();

            // Build the file path in the app data directory.
            string filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

            // Check if the file exists in the app data directory.
            bool FileExists = File.Exists(filePath);
            if (FileExists)
            {
                try
                {
                    // Read the file content and deserialize it.
                    string jsonString = await File.ReadAllTextAsync(filePath);
                    T? data = JsonSerializer.Deserialize<T>(jsonString);

                    if (data is not null)
                    {
                        result.Data = data; // Set the deserialized data.
                        result.Message = "Load complete";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "No load data was present";
                    }
                }
                catch (Exception ex) // Handle deserialization exceptions.
                {
                    result.Message = $"Parsing failed for file {FileName} with message {ex.Message}";
                }
            }
            else
            {
                result.Message = $"File not found: {FileName}";
            }

            return result;
        }
    }

    /// <summary>
    /// Represents the result of a load operation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the loaded data. Must be a reference type with a parameterless constructor.
    /// </typeparam>
    public class LoadResult<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets a value indicating whether the load operation was successful.
        /// </summary>
        public bool Success = false;

        /// <summary>
        /// Gets or sets the deserialized data of type <typeparamref name="T"/>.
        /// </summary>
        public T Data = new T();

        /// <summary>
        /// Gets or sets a message describing the result of the load operation (e.g., success or error details).
        /// </summary>
        public string Message = "";
    }
}
