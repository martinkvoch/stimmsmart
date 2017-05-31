using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MDM.Classes
{
    /// <summary>
    /// Třída implementující frontu požadavků na zpracování kartou LAN
    /// </summary>
    /// <typeparam name="T">Třída reprezentující vstupní UDP paket pro komunikaci s LAN</typeparam>
    public class LANQueue<T>
    {
        private Queue<T> queue = new Queue<T>();
        private volatile bool busy = false;

        private static void doEvents()
        {
            Application.DoEvents();
        }

        /// <summary>
        /// Vrací počet požadavků ve frontě
        /// </summary>
        public int Count {
            get
            {
                if(queue == null) queue = new Queue<T>();
                return queue.Count;
            }
        }

        /// <summary>
        /// Vloží do fronty nový požadavek
        /// </summary>
        /// <param name="item">požadavek ve formě UDP paketu pro LAN</param>
        public void Push(T item)
        {
            if(Count == 0) busy = false;
            while(busy) doEvents();
            busy = true;
            try
            {
                queue.Enqueue(item);
            }
            catch
            {
                busy = false;
            }
            finally
            {
                busy = false;
            }
        }

        /// <summary>
        /// Předá požadavek na vrcholu fronty
        /// </summary>
        /// <returns>Vrací UDP paket pro LAN</returns>
        public T Pull()
        {
            T res = default(T);

            if(Count == 0) busy = false;
            else
            {
                while(busy) doEvents();
                if(Count > 0)
                {
                    busy = true;
                    try
                    {
                        res = queue.Dequeue();
                    }
                    catch
                    {
                        busy = false;
                    }
                    finally
                    {
                        busy = false;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Vrátí požadavek na vrcholu fronty bez vyjmutí z fronty
        /// </summary>
        /// <returns>Vrací UDP paket pro LAN</returns>
        public T Top()
        {
            T res = default(T);

            if(Count == 0) busy = false;
            else
            {
                while(busy) doEvents();
                if(Count > 0)
                {
                    busy = true;
                    try
                    {
                        res = queue.Peek();
                    }
                    catch
                    {
                        busy = false;
                    }
                    finally
                    {
                        busy = false;
                    }
                }
            }
            return res;
        }
    }
}

