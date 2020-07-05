using System;
using System.Collections.Generic;

namespace CAB301_Assignment1 {
    public class Member {
        public string firstname;
        public string lastname;
        public string address;
        public string phone;
        public string username;
        public int password;
        public List<Movie> record = new List<Movie>();
        public Member(string firstname, string lastname, string address, string phone, string username, int password) {
            this.firstname = firstname;
            this.lastname = lastname;
            this.address = address;
            this.phone = phone;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Add a movie to the member's record
        /// </summary>
        /// <param name="movie"></param>
        public void AddRecord(Movie movie) {
            record.Add(movie);
        }

        /// <summary>
        /// Delete a movie from the member's record
        /// </summary>
        /// <param name="movie"></param>
        public void DeleteRecord(Movie movie) {
            record.Remove(movie);
        }
    }
}
