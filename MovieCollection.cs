using System;

namespace CAB301_Assignment1 {
    public class MovieCollection {
        class Node {
            public Movie data;
            public Node left, right;

            public Node(Movie movie) {
                data = movie;
                left = right = null;
            }
        }

        Node root;

        public MovieCollection() {
            root = null;
        }

        public void Delete(string title) {
            root = Delete_Recursive(root, Find(title));
        }

        // A recursive function to delete a movie from the Movie Collection.
        private Node Delete_Recursive(Node root, Movie data) {
            // If the root is empty.
            if (root == null) return root;

            if (root.data.title.CompareTo(data.title) > 0)
                root.left = Delete_Recursive(root.left, data);
            else if (root.data.title.CompareTo(data.title) < 0)
                root.right = Delete_Recursive(root.right, data);

            else {
                // If root node only has one leaf or no leaf.
                if (root.left == null)
                    return root.right;
                else if (root.right == null)
                    return root.left;

                // root will have the value of the node with the smallest data title on the right sub-tree.
                root.data = FindMin(root.right);

                // Delete the successor of the node with the smallest data title on the right sub-tree.
                root.right = Delete_Recursive(root.right, root.data);
            }
            return root;
        }

        private Movie FindMin(Node root) {
            Movie minv = root.data;
            // Check whether the left node is empty or not.
            while (root.left != null) {
                minv = root.left.data;
                root = root.left;
            }
            return minv;
        }

        public void Insert(Movie data) {
            root = Insert_Recursive(root, data);
        }

        // A recursive function to add a new movie to the Movie Collection.
        private Node Insert_Recursive(Node root, Movie data) {

            // Create a new node if the root is empty.
            if (root == null) {
                root = new Node(data);
            }

            if (root.data.title.CompareTo(data.title) > 0)
                root.left = Insert_Recursive(root.left, data);
            else if (root.data.title.CompareTo(data.title) < 0)
                root.right = Insert_Recursive(root.right, data);

            return root;
        }

        public void Inorder() {
            Inorder_Recursive(root);
        }

        private void Inorder_Recursive(Node root) {
            if (root != null) {
                Inorder_Recursive(root.left);
                Console.Write("Title: {0}\n" +
                              "Starring: {1}\n" +
                              "Director: {2}\n" +
                              "Genre: {3}\n" +
                              "Classification: {4}\n" +
                              "Duration: {5}\n" +
                              "Release Date: {6}\n" +
                              "Copies: {7}\n" +
                              "Times Rented: {8}\n\n", root.data.title, string.Join(", ", root.data.starring), string.Join(", ", root.data.director), root.data.genre, root.data.classification, root.data.duration, root.data.release_date, root.data.copies, root.data.times_rented);
                Inorder_Recursive(root.right);
            }
        }

        public Movie Find(string title) {
            return Find_Recursive(root, title);
        }

        // A recursive function to find a specific movie in the Movie Collection based on the provided title.
        private Movie Find_Recursive(Node root, string title) {
            if (root is null) {
                return null;
            }
            if (root.data.title == title)
                return root.data;
            if (root.data.title.CompareTo(title) > 0)
                return Find_Recursive(root.left, title);
            else
                return Find_Recursive(root.right, title);
        }

        // A function to adjust a movie's information when it is borrowed.
        public void BorrowDVD(string title) {
            Find(title).copies--;
            Find(title).times_rented++;
            Console.WriteLine("You borrowed {0}", title);
        }

        // A function to adjust a movie's information when it is returned.
        public void ReturnDVD(string title){
            Find(title).copies++;
            Console.WriteLine("Movie DVD returned");
        }

        // A recursive function to find the most frequently borrowed movie in the Movie Collection.
        Movie FindMaxRecursive(Node root) {
            if (root != null) {
                Movie main = root.data;
                Movie left = FindMaxRecursive(root.left);
                Movie right = FindMaxRecursive(root.right);
                if (left != null) {
                    if (left.times_rented > main.times_rented) {
                        main = left;
                    }
                }
                if (right != null) {
                    if (right.times_rented > main.times_rented) {
                        main = right;
                    }
                }
                return main;
            } else return null;
        }

        // A function to find 10 most frequently borrowed movies in the Movie Collection.
        public void Top10() {
            Movie[] top10 = new Movie[10];
            top10[0] = FindMaxRecursive(root);
            for (int i = 1; i < top10.Length - 1; i++) {
                if (top10[i - 1] != null) {
                    // Temporarily delete movies in the array.
                    Delete(top10[i - 1].title);
                    if (FindMaxRecursive(root) != null && FindMaxRecursive(root).times_rented > 0) {

                        top10[i] = FindMaxRecursive(root);
                    }
                }
            }

            // Add these movies back to the Movie Collection.
            foreach (Movie x in top10) {
                if (x != null) {
                    Console.WriteLine("{0} borrowed {1} times", x.title, x.times_rented);
                    Insert(x);
                }
            }
        }

        // A function to add copies the an existing movie in the Movie Collection.
        public void Add_copies(int number, string title) {
            Find(title).copies = Find(title).copies + number;
            Console.Write("Added {0} new copies of {1}", number, title);
        }
    }
}