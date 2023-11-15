namespace QueueLibrary

{/// <summary>
/// make a generic queue
/// </summary>
/// <typeparam name="T">generic type for the underlying data.</typeparam>
    public class MyQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// internal node for a linked list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class MyNode<T>
        {
            public T data;
            public MyNode<T> next;
            public MyNode<T> prev;
            /// <summary>
            /// constructor for the node
            /// </summary>
            /// <param name="value">the default parameter for any of the generic data types</param>
            public MyNode(T value = default)
            {
                data = value;
                next = null;
                prev = null;

            }
        }
        private MyNode<T> head;
        private MyNode<T> tail;
        //private MyNode<T> tempPoint;
        private int count;
        /// <summary>
        /// constructor for the Queue
        /// </summary>
        public MyQueue()
        {
            head = null;
            tail = null;
            //tempPoint = null;
            count = 0;
        }
        
        /// <summary>
        /// add a node to the queue
        /// </summary>
        /// <param name="item">a data value of type T</param>
        public void Enqueue(T item)
        {

            if (count > 0)
            {
                //create node
                MyNode<T> node = new MyNode<T>(item);
                //link the new node to the existing queue
                tail.next = node;
                //update tail pointer
                tail = node;
                //step 4 increment the count
                count++;

            }
            else
            {
                MyNode<T> node = new MyNode<T>(item);
                tail = node;
                head = node;
                count++;

            }
        }
        /// <summary>
        /// remove the first element from the Queue
        /// </summary>
        /// <exception cref="InvalidOperationException">This exception is thrown if the queue is empty and the
        /// user tries to dequeue</exception>
        public T DeQueue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is empty");

            }

            T tempPoint = head.data;
            count--;
            head = head.next;
            return tempPoint;
        }
        /// <summary>
        /// test if the queue is empty
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsEmpty()
        {
            return count == 0;
        }
        /// <summary>
        /// this function calls the enumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator
           System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// same as the previous docstring. These methods work together to enumerate the queue in fifo order
        /// using a foreach loop can call the method
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            MyNode<T> curr = head;
            while (curr != null)
            {
                yield return curr.data;
                curr = curr.next;
            }

        }
        /// <summary>
        /// gets the count of the amount of nodes on the queue
        /// </summary>
        public int Count
        { get { return count; } }

        /// <summary>
        /// clears the queue
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary>
        /// ToString prints the first three elements of the queue and the last three elements of the queue. I 
        /// copied from my stack ToString which was made by AI. It took me a lot longer than it took the AI,
        /// but I eventually got it after I decided to add elements one at a time
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string elements = "";

            MyNode<T> current = head;

            if (current != null)
            {
                elements += current.data;
                current = current.next;
            }

            if (current != null)
            {
                elements += ", " + current.data;
                current = current.next;
            }
            if (current != null)
            {
                elements += ", " + current.data;
                current = current.next;
            }
           
            if (Count > 6)
            {
                elements += " , ... ";

                // Move to the third-to-last element
                int stepsToThirdToLast = Count - 6; //because current already equals the third element of the queue
                for (int i = 0; i < stepsToThirdToLast; i++)
                {
                    current = current.next;
                }
            }

            // Include the last two elements before tail
            while (current != null)
            {
                elements += ", " + current.data;
                current = current.next;
            }

            return $"MyQueue<{typeof(T).Name}> is {Count} elements long: {elements}";
        }

        // these next three functions were all created by AI as the directions ask.
        // URL: https://chat.openai.com/
        // prompt:You must use AI to create three new functions that are not yet included in our Queue API
        // using these rules:Use comments to cite the use of Generative AI. Have the AI generate docstrings for every
        // function in your class. Copy-paste the docstrings into your class. Add a comment at the top of your source code
        // indicating that the docstrings were AI generated.
        // HERE is my library below - one of the methods or functions should be ToArray. *pasted library*
        // the comments are my code and its me explaining what the ai is doing

        /// <summary>
        /// Converts the elements of the queue into an array.
        /// </summary>
        /// <returns>An array containing the elements in the same order as they appear in the queue.</returns>
        public T[] ToArray()
        {
            T[] array = new T[count]; //created an array the same size as the count of the nodes
            MyNode<T> current = head; //creates a node that starts at head
            int index = 0; //creates an integer starting at beginning of the array

            while (current != null) //loops through until current = null
            {
                array[index] = current.data; // first element of the index is current
                current = current.next; //increments current to the next node
                index++; //increments the index to the next node
            }

            return array; // returns
        }
        /// <summary>
        /// Retrieves the element at the beginning of the queue without removing it.
        /// </summary>
        /// <returns>The element at the front of the queue.</returns>
        public T Peek() //same as peek function for stack. I think this is pretty simple to understand
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return head.data;
        }

        /// <summary>
        /// Determines whether the queue contains a specific element.
        /// </summary>
        /// <param name="item">The element to locate in the queue.</param>
        /// <returns>true if the element is found in the queue; otherwise, false.</returns>
        public bool Contains(T item) //this method is pretty cool. It searches the queue for a specific element
                                     //or item as referenced above
        {
            MyNode<T> current = head; //creates a node called current and points it to head. 

            while (current != null) //iterates through all the nodes until current is pointing to a null item
            {
                if (EqualityComparer<T>.Default.Equals(current.data, item)) //compares the item entered in the parameters, to any current.data found
                {
                    return true;
                }

                current = current.next; //increments current by 1
            }

            return false; //possible improvements would be to add in a ToUpper or ToLower if you are not looking for
            // an exact match via capitalization in strings. 
        }

    }
}
