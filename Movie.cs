using System;
namespace CAB301_Assignment1 {
    public class Movie {
        public enum Genre {
            Drama,
            Adventure,
            Family,
            Action,
            Sci_Fi,
            Comedy,
            Animated,
            Thriller,
            Other
        }

        public enum Classification {
            General,
            Parental_Guidance,
            Mature,
            Mature_Accompanied
        }

        public string title;
        public string[] starring;
        public string[] director;
        public int duration;
        public Genre genre;
        public Classification classification;
        public int release_date;
        public int copies;
        public int times_rented;
        public Movie(string title, string[] starring, string[] director, int duration, Genre genre, Classification classification, int release_date, int copies, int times_rented) {
            this.title = title;
            this.starring = starring;
            this.director = director;
            this.duration = duration;
            this.genre = genre;
            this.classification = classification;
            this.release_date = release_date;
            this.copies = copies;
            this.times_rented = times_rented;
        }
    }
}
