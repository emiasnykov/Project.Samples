using System;

namespace ResWebApiTest.TestEngine.EventHandlers
{
    /// <summary>
    /// Exception event argument
    /// </summary>
    /// <param name="_KeyName">Key name used in Value methods</param>
    /// <param name="_KeyExists">true, If key exists in the Value method</param>
    /// <param name="_ValueObj">Object to be converted to the List. If the List is empty, raise the exception</param>
    /// <param name="_ItemExists">true, if the whole item list, described by any Key form Value method, exists</param>
    public class ExceptionOnValueFailureEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public DateTime CompletionTime { get; set; }

        // Arguments
        public string KeyName { get; set; }
        public bool KeyExists { get; set; }
        public object ValueObj { get; set; }
        public bool ItemExists { get; set; }
    }

    /// <summary>
    /// Excpetion event handler
    /// TODO - at this moment this is inactive
    /// </summary>

    public class ExceptionBusinnessLogic
    {
        // Declaring an event using built-in EventHandler
        public event EventHandler<ExceptionOnValueFailureEventArgs> ProcessCompleted;

        public void StartProcess()
        {
            var data = new ExceptionOnValueFailureEventArgs();

            try
            {
                Console.WriteLine("Process Exception Started!");

                // Exception data
                data.IsSuccessful = true;
                data.CompletionTime = DateTime.Now;
                data.KeyName = "";
                data.KeyExists = true;
                data.ValueObj = "";
                data.ItemExists = true;

                // Event process
                OnProcessCompleted(data);
            }
            catch (Exception)
            {
                data.IsSuccessful = false;
                data.CompletionTime = DateTime.Now;

                // Event process
                OnProcessCompleted(data);
            }
        }

        /// <summary>
        /// Run on process completed
        /// TODO - at this moment this is inactive
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProcessCompleted(ExceptionOnValueFailureEventArgs e)
        {
            ProcessCompleted?.Invoke(this, e);
        }
    }
}
