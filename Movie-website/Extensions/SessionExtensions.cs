using Microsoft.AspNetCore.Http;
using System.Text.Json;

/*
 * SessionExtensions
 * 
 * This is a helper class that makes it easier to save and load data in the Session.
 * 
 * Normally, Session can only store simple things like strings. But if you want to save a list or an object, you need to 
 * convert it to a string first.
 * 
 * This class does that work for you:
 * - Set<T>: Saves any object (like a list) in Session. Converts any object to a JSON string and saves it.
 * - Get<T>: Gets the object back from Session. Reads a JSON string from the session and converts it back to the original object.
 * 
 * Example:
 * - Save a wishlist in session: HttpContext.Session.Set("wishlist", wishlist);
 * - Get the wishlist back: var wishlist = HttpContext.Session.Get<List<int>>("wishlist");
 */

namespace Movie_website.Extensions
{
    // This is a static helper class to make it easy to save and load objects in Session
    public static class SessionExtensions
    {
        /*public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }*/

        // This method saves an object into the session
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Convert the object (for example, a list) to a JSON string
            string json = JsonSerializer.Serialize(value);

            // Save the JSON string in the session using the given key
            session.SetString(key, json);
        }

        // This method loads an object from the session
        public static T Get<T>(this ISession session, string key)
        {
            // Try to get the JSON string from the session using the key
            string value = session.GetString(key);

            // If nothing is found (the value is null), return the default value (for example: null)
            if (value == null)
            {
                return default;
            }

            // Convert the JSON string back to the original object and return it
            T result = JsonSerializer.Deserialize<T>(value);
            return result;
        }
    }
}
