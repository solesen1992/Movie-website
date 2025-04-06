using Microsoft.AspNetCore.Http;
using System.Text.Json;

/*
 * SessionExtensions
 * 
 * This helper class makes it easy to save and load data in the session.
 * 
 * In ASP.NET Core, Session can normally only store simple data like strings or integers.
 * But what if you want to store more advanced data — like a list of movie IDs? 
 * To do that, we need to "convert" the object into a string using JSON (serialization).
 * Later, we can "convert it back" into its original form (deserialization).
 * 
 * It has two main methods:
 * - Set<T>: Saves any object into session as a JSON string.
 * - Get<T>: Loads the object back from session and turns it back into the original type.
 */

namespace Movie_website.Extensions
{
    // This is a static helper class to make it easy to save and load objects in Session
    public static class SessionExtensions
    {
        /*
         * Set<T> method
         * 
         * This method saves an object in the session.
         * 
         * How it works:
         * - Takes the object (for example: a List<int> of movie IDs)
         * - Turns it into a JSON string
         * - Stores that string in session under the given key (like "wishlist")
         * 
         * <T> means this method works for ANY type: a string, a number, a list, or even a full model like Movie.
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
         * This method reads an object from the session.
         * 
         * How it works:
         * - Looks for a JSON string in session using the key (like "wishlist")
         * - If it finds something, it turns the string back into the original object (like List<int>)
         * - If there's nothing saved, it returns null or an empty value
         * 
         * <T> again means this works for ANY type, and you tell it what to expect.
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
