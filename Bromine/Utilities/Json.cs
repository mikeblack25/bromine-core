using System.IO;

using Newtonsoft.Json;

namespace Bromine.Utilities
{
    /// <summary>
    /// Support for interacting with Json objects.
    /// </summary>
    public class Json
    {
        /// <summary>
        /// Save an object to a file.
        /// </summary>
        /// <param name="path">Path of the file to save.</param>
        /// <param name="saveObject">Serializable object to save.</param>
        /// <param name="formatting">Intent formatting or not?</param>
        public void SaveObject<T>(string path, T saveObject, Formatting formatting = Formatting.Indented)
        {
            try
            {
                var data = JsonConvert.SerializeObject(saveObject, formatting);

                using (var writer = new StreamWriter(path))
                {
                    writer.Write(data);
                }
            }
            catch
            {
                // TODO: Add log message.
            }
        }

        /// <summary>
        /// Read contents of a file into a serializable object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Path of the file to save.</param>
        /// <param name="type">Time of object to create.</param>
        /// <returns></returns>
        public T ReadObject<T>(string path, T type)
        {
            try
            {
                string data;

                using (var reader = new StreamReader(path))
                {
                    data = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                // TODO: Add log message.

                return (T) new object();
            }

        }
    }
}
