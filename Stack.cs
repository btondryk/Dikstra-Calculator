namespace StackLibrary

{
    /// <summary>
    /// create a generic stack library called MyStack which uses an array of type T and two private integers.
    /// </summary>
    public class MyStack<T> : IEnumerable<T>
    {
        private T[] numbers;
        private int current;
        private int capacity;

        /// <summary>
        /// the constructor initializes the type T array to be the size of the capacity
        /// and initializes the ints created in the class. It takes one argument to be passed through
        /// current is set to -1 because nothing has been added to the array/stack
        /// </summary>
        /// <param name="capacity">length of the array</param>
        public MyStack(int capacity = 64)
        {
            numbers = new T[capacity];
            current = -1;
            this.capacity = capacity;
        }

        /// <summary>
        /// Pushes an item on the stack. The most recent item pushed will be at the top of the stack
        /// </summary>
        /// <param name="item">the T element being added</param>
        public void Push(T item)
        {
            if (Count() >= numbers.Length)
            {
                int newCapacity = Count() * 2;
                numbers = ResizeArray(newCapacity);
                capacity = newCapacity;
            }
            current += 1;
            numbers[current] = item;
        }

        /// <summary>
        /// Resizes the array making it shorter or longer depending on the count and capacity
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>newArray which is basically just an adjusted array to the numbers array</returns>
        private T[] ResizeArray(int capacity)
        {
            T[] newArray = new T[capacity];
            for (int i = 0; i < Count(); i++)
            {
                newArray[i] = numbers[i];
            }
            return newArray;
        }

        /// <summary>
        /// pops or removes the most current item from the stack 
        /// </summary>
        /// <returns>
        /// the item that was just removed
        /// </returns>
        public T Pop()
        {

            if (Count() < 0.25 * numbers.Length)
            {
                int newCapacity = (Count() * 2);
                numbers = ResizeArray(newCapacity);
                capacity = newCapacity;
            }

            T item = numbers[current];
            current--;
            return item;



        }
        /// <summary>
        /// Imagine having a deck of cards face down in front of you. the Peek() method allows you to look at the top card without removing it from the stack
        /// made with generative ai. URL: https://chat.openai.com/ , Prompt: How would you implement Peek.stack when dealing with stacks
        /// </summary>
        /// <returns>
        /// returns item that is on top
        /// </returns>
        /// <exception cref="InvalidOperationException">The exception is triggered if the stack is empty and the method
        /// is called</exception>
        public T Peek()//ai gave me an example of how to write this, but this whole method is all my own work
        {
            if (current >= 0)
            {
                return numbers[current];
            }
            else
            {
                throw new InvalidOperationException("Stack is empty");
            }
        }
        /// <summary>
        /// if the stack is empty this returns true, if the stack is not empty it returns false
        /// </summary>
        /// <returns>true or false</returns>
        public bool isEmpty()
        {
            return current == -1; //remember from the constructor stack is empty if current == -1.
        }
        /// <summary>
        /// returns the current number of items on the stack
        /// </summary>
        /// <returns>count</returns>
        public int Count()
        {
            return current + 1; //because arrays start at the 0th position
        }

        /// <summary>
        /// our Enumerator allows us to enumerate each element of the stack in LIFO order using the foreach loop
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// same as the previous docstring. These methods work together to enumerate the stack in lifo order
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = current; i > -1; i--)
            {
                yield return numbers[i];
            }
        }

        /// <summary>
        /// ToString shows the count as the numerator and the capacity of the array as the denominator
        /// it describes the first three elements and the last three elements
        /// AI was used. Prompt: Hello, Can you please create a ToString Method that prints the first three elements
        /// and the last three elements of the stack formated as 
        /// "element1", "element2", "element3", ... , "elementn-2", "elementn-1", "elementn"
        /// where elementn is the last element in the array with an assigned value (if there are less than 6 elements just print all)
        /// for the library pasted below *pasted library*. 
        /// </summary>
        /// <returns>a descriptive string that provides information about the count of the stack and the capacity</returns>
        public override string ToString()
        {

            string elements = "";

            for (int i = 0; i <= Math.Min(current, 2); i++)
            {
                elements += numbers[i].ToString();
                if (i < Math.Min(current, 2)) elements += ", ";
            }

            if (Count() > 6)
            {
                elements += " ... ";
            }

            for (int i = Math.Max(3, Count() - 3); i <= current; i++)
            {
                elements += numbers[i].ToString();
                if (i < current) elements += ", ";
            }

            return $"MyStack<{typeof(T).Name}> is {Count()}/{capacity} full: {elements}";
        }
    }

}