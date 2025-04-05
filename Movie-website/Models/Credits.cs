/*
 * Credits
 * 
 * Combines both the `Cast` and `Crew` classes, representing the complete list of cast and crew information 
 * for a movie or series. This class is mapped directly from the `credits` JSON property returned by the TMDb API.
 * 
 * This class simplifies data handling by combining both `Cast` and `Crew` into one object, eliminating the need for separate response classes.
 */

namespace Movie_website.Models
{
    public class Credits
    {
        // List of actors in the movie or series
        public List<Cast> Cast { get; set; }

        // List of crew members (e.g. Director, Producer, etc.)
        public List<Crew> Crew { get; set; }
    }
}
