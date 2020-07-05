using System;
namespace CAB301_Assignment1 {
    public class MemberCollection {
        Member[] collection = new Member[] { };
        public MemberCollection() {
        }

        /// <summary>
        /// Add a new member to the Member Collection.
        /// </summary>
        /// <param name="member"></param>
        public void Register(Member member) {
            Array.Resize(ref collection, collection.Length + 1);
            collection[collection.GetUpperBound(0)] = member;
        }

        // Check if a specific member is in the Member Collection or not.
        public bool IsRegistered(string firstname, string lastname) {
            foreach (Member member in collection) {
                if (member.firstname == firstname && member.lastname == lastname) {
                    return true;
                }
            }
            return false;
            }

        /// <summary>
        /// Find a member's phone number based on their first and last name.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        public void FindPhone(string firstname, string lastname) {
            foreach(Member member in collection) {
                if(member.username == lastname + firstname) {
                    Console.Write("{0} {1}'s phone number is: {2}",firstname, lastname, member.phone);
                }
            }
        }

        // Verify the input for username and password.
        public bool Verified(string username, int pass) {
            foreach (Member member in collection) {
                if (username == member.username && pass == member.password) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add a movie to a member's record.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <param name="storage"></param>
        public void Rent(string username, string title, MovieCollection storage) {
            foreach (Member member in collection) {
                if (member.username == username && storage.Find(title) != null) {
                    member.AddRecord(storage.Find(title));
                }
            }
        }

        /// <summary>
        /// Delete a movie from a member's record.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <param name="storage"></param>
        public void Return(string username, string title, MovieCollection storage) {
            foreach (Member member in collection) {
                if (member.username == username && storage.Find(title) != null) {
                    member.DeleteRecord(storage.Find(title));
                }
            }
        }

        /// <summary>
        /// Display a list of movies in a member's record.
        /// </summary>
        /// <param name="username"></param>
        public void List(string username) {
            foreach (Member member in collection) {
                if (member.username == username) {
                    Console.WriteLine("You are currently borrowing:");
                    foreach(Movie movie in member.record) {
                        Console.WriteLine(movie.title);
                    }
                }
            }
        }

        // Check if a movie is in member's record or not.
        public bool IsRented(string username, string title) {
            foreach (Member member in collection) {
                if (member.username == username) {
                    foreach (Movie movie in member.record) {
                        if (movie.title == title) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
