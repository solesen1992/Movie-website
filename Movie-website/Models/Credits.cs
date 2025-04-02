/*
     * Credits
     * 
     * This model represents both the cast and crew information 
     * returned by The Movie Database API when fetching details 
     * about a movie or a series.
     * 
     * It combines cast and crew into one object because they 
     * always appear together in the "credits" JSON property.
     * 
     * This makes it easier to map and use in the Service Layer 
     * and View without extra unnecessary response classes.
     */

namespace Movie_website.Models
{
    public class Credits
    {
        // List of actors in the movie or series
        public List<CastMember> Cast { get; set; }

        // List of crew members (e.g. Director, Producer, etc.)
        public List<CrewMember> Crew { get; set; }
    }
}
