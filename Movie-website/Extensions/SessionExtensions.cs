using Microsoft.AspNetCore.Http;
using System.Text.Json;

/*
 * SessionExtensions
 * 
 * This is a helper class that simplifies working with the session by enabling the saving and loading of objects.
 * Normally, the Session in ASP.NET Core can only store basic types such as strings, integers, etc. 
 * To store more complex data types like lists or custom objects, we need to serialize them into a string format.
 * This class provides extension methods to make this process seamless.
 * 
 * The main methods are:
 * - Set<T>: Saves any object in the session by serializing it into a JSON string.
 * - Get<T>: Retrieves an object from the session and deserializes it back into the original object type.
 */

namespace Movie_website.Extensions
{
    // This is a static helper class to make it easy to save and load objects in Session
    public static class SessionExtensions
    {
        /* 
         * Set<T> method
         * 
         * This method is an extension for ISession that allows saving an object into the session.
         * It serializes the object to a JSON string and saves it in the session using a specified key.
         * 
         * The <T> is a generic type parameter, allowing this method to work with any type of object. 
         * This means that we can save any object, whether it’s a simple string, an integer, or even a complex list or model, 
         * without needing multiple overloads for different types.
         */
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Serialize the object (e.g., list, model) into a JSON string
            string json = JsonSerializer.Serialize(value);

            // Store the JSON string in session using the provided key
            session.SetString(key, json);
        }

        /*
         * Get<T> method
         * 
         * This method loads an object from the session
         * 
         * This method is an extension for ISession that allows retrieving an object from the session.
         * It reads the JSON string from the session and deserializes it back into the original object type.
         * The <T> is the generic type parameter, which allows the method to return any object type.
         * 
         * Why do we use <T>? The <T> type allows this method to work flexibly with different types of objects.
         * You don't need separate methods for each type (e.g., GetString(), GetInt(), GetList()). 
         * Instead, you just call Get<T>(), specifying the type you expect to retrieve, and it will be handled automatically.
         * 
         * If no data is found in the session for the given key, it returns the default value (null or the default type value).
         */
        public static T Get<T>(this ISession session, string key)
        {
            // Attempt to retrieve the JSON string from the session
            string value = session.GetString(key);

            // If the value doesn't exist in the session (null), return the default value for the type T
            if (value == null)
            {
                return default;
            }

            // Deserialize the JSON string back into the original object and return it
            T result = JsonSerializer.Deserialize<T>(value);
            return result;
        }
    }
}
