using System;

namespace CAB301_Assignment1 {
    class Program {
        const int Back = 0;
        const int FirstOption = 1;
        const int SecondOption = 2;
        const int ThirdOption = 3;
        const int FourthOption = 4;
        const int FifthOption = 5;
        const string username_staff = "staff";
        const string password_staff = "today123";
        static void Main(string[] args) {
            MovieCollection tree = new MovieCollection();
            MemberCollection database = new MemberCollection();
            int option;
            int option1;
            int option2;
            bool set = true;
            do {
                // Display the main menu to get the user's input option
                MainMenu();
                option = GetOption();
                if (option == FirstOption) {
                    // Check the authentication of the user 
                    if (Authenticated()) {
                        do {
                            // Display the dedicated menu for staff
                            SubMenu1();
                            // Get the staff's input option
                            option1 = GetOption1();
                            // Call function that adds a movie with required details to the Movie Collection
                            if (option1 == FirstOption) {
                                AddMovie(tree);
                            }
                            // Call function that deletes a specific movie by name from the Movie Collection
                            if (option1 == SecondOption) {
                                Delete(tree);
                            }
                            // Call function that adds a member with required details to the Member Collection
                            if (option1 == ThirdOption) {
                                AddMember(database);
                            }
                            // Call function that finds a member's phone number
                            if (option1 == FourthOption) {
                                FindPhoneNumber(database);
                            }
                        } while (option1 != Back);
                    } else { Console.Write("Wrong username or password!!!\n\n"); }
                }

                if (option == SecondOption) {
                    var authentication = Verified(database);
                    // Check the authentication of the user 
                    if (authentication.Item1) {
                        do {
                            // Display the dedicated menu for member
                            SubMenu2();
                            // Get the member's input option
                            option2 = GetOption2();
                            // Call function that displays a list of movie in the Movie Collection
                            if (option2 == FirstOption) {
                                tree.Inorder();
                            }
                            // Call function that lets the member borrow a movie
                            if (option2 == SecondOption) {
                                Borrow(database, tree, authentication.Item2);
                            }
                            // Call function that lets the member return a movie
                            if (option2 == ThirdOption) {
                                Return(database, tree, authentication.Item2);
                            }
                            // Call function that displays a list of borrowed movie of the member
                            if (option2 == FourthOption) {
                                database.List(authentication.Item2);
                            }
                            // Call function that displays the most frequently borrowed movies
                            if (option2 == FifthOption) {
                                tree.Top10();
                            }
                        } while (option2 != Back);
                    } else { Console.Write("Wrong username or password!!!\n\n"); }
                }
                if (option == Back) {
                    set = false;
                    Quit();
                }
            } while (set == true || option != Back);

        }

        static void MainMenu() {
            Console.Write("Welcome to the Community Library\n" +
                          "===========Main Menu============\n" +
                          "1. Staff Login\n" +
                          "2. Member Login\n" +
                          "0. Exit\n" +
                          "================================\n\n" +
                          "Please make a selection (1-2, or 0 to exit): ");
        }

        /// <summary>
        /// Get user's input option, which must be a number from 0 to 2, in the main menu in order to transit to requested menu or exit.
        /// </summary>
        /// <returns> User's input for main menu. </returns>
        static int GetOption() {
            string input;
            int option;
            bool tester;
            do {
                input = Console.ReadLine();
                tester = int.TryParse(input, out option);
                if (!tester) {
                    Console.Write("You must enter a number!!!\n\n");
                    MainMenu();
                } else if ((option < Back) || (option > SecondOption)) {
                    Console.Write("Please enter a number from 1 to 0!!!\n\n");
                    MainMenu();
                }
            } while ((!tester) || (option < Back) || (option > SecondOption));
            return option;
        }

        /// <summary>
        /// Compare the input for the username and password with those in the system.
        /// </summary>
        /// <returns> A boolean value indicates whether the conditions were satisfied or not. </returns>
        static bool Authenticated() {
            Console.Write("Enter username: ");
            string username_input = Console.ReadLine();
            Console.Write("Enter password: ");
            string password_input = Console.ReadLine();
            if (username_input == username_staff && password_input == password_staff) {
                return true;
            }
            else return false;
        }

        // Staff's menu
        static void SubMenu1() {
            Console.Write("\n\n===========Staff Menu===========\n" +
                          "1. Add a new movie DVD\n" +
                          "2. Remove a movie DVD\n" +
                          "3. Register a new member\n" +
                          "4. Find a registered member's phone number\n" +
                          "0. Return to main menu\n" +
                          "================================\n" +
                          "Please make a selection (1-4 or 0 to return to main menu): ");
        }

        /// <summary>
        /// Get staff's input option, which must be a number from 0 to 4, in the staff menu in order to perform specific actions.
        /// </summary>
        /// <returns> Staff's input for staff menu. </returns>
        static int GetOption1() {
            string choice1;
            int option1;
            bool tester;
            do {
                choice1 = Console.ReadLine();
                tester = int.TryParse(choice1, out option1);
                if (!tester) {
                    Console.WriteLine("You must enter a number!!!");
                    SubMenu1();
                } else if ((option1 < Back) || (option1 > FourthOption)) {
                    Console.WriteLine("Please enter a number from 0 to 4!!!");
                    SubMenu1();
                }
            } while ((!tester) || (option1 < Back) || (option1 > FourthOption));
            return option1;
        }

        /// <summary>
        /// Get required information from staff's input to add a new movie to the Movie Collection or add a number of copies to an existing movie.
        /// </summary>
        /// <param name="collection"></param>
        static void AddMovie(MovieCollection collection) {
            string title = GetTitle();
            if (collection.Find(title) == null) {
                Console.Write("Enter the starring actor(s): ");
                string starring_input = Console.ReadLine();
                string[] starring = starring_input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                Console.Write("Enter the director(s): ");
                string director_input = Console.ReadLine();
                string[] director = director_input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                Movie.Genre genre = getGenre();
                Movie.Classification classification = getClassification();
                Console.Write("Enter the duration (minutes): ");
                int duration = GetValue();
                Console.Write("Enter the release year (year): ");
                int release_date = GetValue();
                Console.Write("Enter the number of copies available: ");
                int copies = GetValue();
                collection.Insert(new Movie(title, starring, director, duration, genre, classification, release_date, copies, 0));
            } else {
                Console.Write("Enter the number of copies you would like to add: ");
                int value = GetValue();
                collection.Add_copies(value, title);
            }
        }

        /// <summary>
        /// Get one specific genre based on staff's input option.
        /// </summary>
        /// <returns> Movie's genre </returns>
        static Movie.Genre getGenre() {
            Console.Write("\nSelect the genre:\n" +
                          "1. Drama\n" +
                          "2. Adventure\n" +
                          "3. Family\n" +
                          "4. Action\n" +
                          "5. Sci-fi\n" +
                          "6. Comedy\n" +
                          "7. Animated\n" +
                          "8. Thriller\n" +
                          "9. Other\n" +
                          "Make a selection(1-9): ");
            int value = GetValue();
            if (value == 1) {
                return Movie.Genre.Drama;
            }
            if (value == 2) {
                return Movie.Genre.Adventure;
            }
            if (value == 3) {
                return Movie.Genre.Family;
            }
            if (value == 4) {
                return Movie.Genre.Action;
            }
            if (value == 5) {
                return Movie.Genre.Sci_Fi;
            }
            if (value == 6) {
                return Movie.Genre.Comedy;
            }
            if (value == 7) {
                return Movie.Genre.Animated;
            }
            if (value == 8) {
                return Movie.Genre.Thriller;
            }
            if (value == 9) {
                return Movie.Genre.Other;
            } else { return default; }
        }

        /// <summary>
        /// Get one specific classification based on staff's input option.
        /// </summary>
        /// <returns> Movie's classification </returns>
        static Movie.Classification getClassification() {
            Console.Write("\nSelect the classification:\n" +
                          "1. General (G)\n" +
                          "2. Parental Guidance\n" +
                          "3. Mature (M15+)\n" +
                          "4. Mature Accompanied (MA15+)\n" +
                          "Make a selection(1-4): ");
            int value = GetValue();
            if (value == 1) {
                return Movie.Classification.General;
            }
            if (value == 2) {
                return Movie.Classification.Parental_Guidance;
            }
            if (value == 3) {
                return Movie.Classification.Mature;
            }
            if (value == 4) {
                return Movie.Classification.Mature_Accompanied;
            } else { return default;  }
        }

        /// <summary>
        /// Get required information from staff's input to add a new member to the Member Collection.
        /// </summary>
        /// <param name="database"></param>
        static void AddMember(MemberCollection database) {
            Console.Write("Enter member's firstname: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter member's lastname: ");
            string lastname = Console.ReadLine();
            if (!database.IsRegistered(firstname, lastname)) {
                Console.Write("Enter member's address: ");
                string address = Console.ReadLine();
                Console.Write("Enter member's phone number: ");
                string phone_number = Console.ReadLine();
                int password = GetPassword();
                database.Register(new Member(firstname, lastname, address, phone_number, (lastname + firstname), password));
                Console.Write("Successfully added {0} {1}", firstname, lastname);
            } else { Console.Write("{0} {1} has already registered.", firstname, lastname); }
        }

        /// <summary>
        /// Delete a specific movie from the Movie Collection based on the input title.
        /// </summary>
        /// <param name="collection"></param>
        static void Delete(MovieCollection collection) {
            string title = GetTitle();
            if (MovieExists(collection, title)) {
                collection.Delete(title);
                Console.Write("{0} was deleted", title);
            } else { Console.Write("Movie doesn't exist!!!"); }
        }

        /// <summary>
        /// Get a member's phone number based on firstname and lastname.
        /// </summary>
        /// <param name="database"></param>
        static void FindPhoneNumber(MemberCollection database) {
            Console.Write("Enter member's firstname: ");
            string firstname = Console.ReadLine();
            Console.Write("Enter member's lastname: ");
            string lastname = Console.ReadLine();
            if (database.IsRegistered(firstname, lastname)) {
                database.FindPhone(firstname, lastname);
            } else { Console.Write("User doesn't exist"); }
        }

        // Member's menu
        static void SubMenu2() {
            Console.Write("\n===========Member Menu===========\n" +
                          "1. Display all movies\n" +
                          "2. Borrow a movie DVD\n" +
                          "3. Return a movie DVD\n" +
                          "4. List current borrowed movie DVDs\n" +
                          "5. Display top 10 most popular movies\n" +
                          "0. Return to main menu\n" +
                          "=================================\n" +
                          "Please make a selection (1-5 or 0 to return to main menu): ");
        }

        /// <summary>
        /// Get member's input option, which must be a number from 0 to 5, in the member menu in order to perform specific actions.
        /// </summary>
        /// <returns> Member's input for member menu. </returns>
        static int GetOption2() {
            string choice2;
            int option2;
            bool tester;
            do {
                choice2 = Console.ReadLine();
                tester = int.TryParse(choice2, out option2);
                if (!tester) {
                    Console.WriteLine("You must enter a number!!!");
                    SubMenu2();
                } else if ((option2 < Back) || (option2 > FifthOption)) {
                    Console.WriteLine("Please enter a number from 0 to 5!!!");
                    SubMenu2();
                }
            } while ((!tester) || (option2 < Back) || (option2 > FifthOption));
            return option2;
        }

        /// <summary>
        /// Compare the input for the username and password with those in the Member Collection.
        /// </summary>
        /// <param name="database"></param>
        /// <returns> A pair of boolean value indicates whether the conditions were satisfied or not </returns>
        static (bool, string) Verified(MemberCollection database) {
            Console.Write("Enter username (LastnameFirstname): ");
            string username_input = Console.ReadLine();
            Console.Write("Enter password: ");
            int password_input = GetValue();
            if (database.Verified(username_input, password_input)) {
                return (true, username_input);
            } else return (false, "");
        }

        /// <summary>
        /// Add a movie to the member's record based on the movie's title.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="collection"></param>
        /// <param name="username"></param>
        static void Borrow(MemberCollection database, MovieCollection collection, string username) {
            string title = GetTitle();
            if (MovieExists(collection, title)) {
                if (!database.IsRented(username, title)) {
                    if (collection.Find(title).copies != 0) {
                        collection.BorrowDVD(title);
                        database.Rent(username, title, collection);
                    } else { Console.Write("This movie is not available!!!\n"); }
                } else { Console.Write("You've already borrowed this movie!!!\n"); }
            } else { Console.Write("Movie doesn't exist!!!\n"); }
        }

        /// <summary>
        /// Remove a movie from the member's record based on the movie's title.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="collection"></param>
        /// <param name="username"></param>
        static void Return(MemberCollection database, MovieCollection collection, string username) {
            string title = GetTitle();
            if (MovieExists(collection, title)) {
                if (database.IsRented(username, title)) {
                    collection.ReturnDVD(title);
                    database.Return(username, title, collection);
                } else { Console.Write("You've not borrowed this movie!!!\n"); }
            } else { Console.Write("Movie doesn't exist!!!\n"); }
        }

        /// <summary>
        /// Get user's input value and convert to integer type.
        /// </summary>
        /// <returns> A specific number </returns>
        static int GetValue() {
            string input;
            int value;
            bool tester;
            do {
                input = Console.ReadLine();
                tester = int.TryParse(input, out value);
                if (!tester) {
                    Console.WriteLine("You must enter a number!!!");
                }
                if (value < 0) {
                    Console.WriteLine("Please enter a positive number!!!");
                }
            } while ((!tester) || (value < 0));
            return value;
        }

        /// <summary>
        /// Get staff's input for password when registering a new member.
        /// </summary>
        /// <returns> A 4-digits number </returns>
        static int GetPassword() {
            string input;
            int value;
            bool tester;
            do {
                Console.Write("Enter member's password(4 digits): ");
                input = Console.ReadLine();
                tester = int.TryParse(input, out value);
                if (!tester) {
                    Console.WriteLine("You must enter a number!!!");
                } else if (input.Length != 4) {
                    Console.WriteLine("Password must be 4-digits!!!");
                }

            } while (!tester || input.Length != 4);
            return value;
        }

        /// <summary>
        /// Get user's input for a movie's title.
        /// </summary>
        /// <returns> A string represents a movie's title </returns>
        static string GetTitle() {
            string input;
            Console.Write("Enter the movie title: ");
            input = Console.ReadLine();
            return input;
        }

        // Check whether a movie is in the Movie Collection or not.
        static bool MovieExists(MovieCollection collection, string title) {
            if (collection.Find(title) != null) {
                return true;
            }
            return false;
        }

        static void Quit() {
            Console.WriteLine("\nThanks for using the application!");
            Console.Write("\nHit Enter to exit");
            Console.ReadKey();
        }
    }
}

