using System;
using System.Collections.Generic;

namespace HW08.Services
{
    public class Messenger
    {
        /// <summary>
        /// Gets the default messenger.
        /// </summary>
        /// <value> The default messenger. </value>
        public static Messenger Default
        {
            get
            {
                defaultMessenger = defaultMessenger ?? new Messenger();

                return defaultMessenger;
            }
        }

        /// <summary>
        /// The default messenger
        /// </summary>
        private static Messenger defaultMessenger;

        /// <summary>
        /// The actions hooked up to messages
        /// </summary>
        private Dictionary<Type, Delegate> actions = new Dictionary<Type, Delegate>();

        /// <summary>
        /// The functions hooked up to requests
        /// </summary>
        private Dictionary<Type, Delegate> functions = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Register a function for a request message.
        /// </summary>
        /// <typeparam name="T"> Type of message to receive. </typeparam>
        /// <typeparam name="R"> Type of the r. </typeparam>
        /// <param name="request"> The function that fills the request. </param>
        public void Register<T, R>(Func<T, R> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (functions.ContainsKey(typeof(T)))
            {
                functions[typeof(T)] = Delegate.Combine(functions[typeof(T)], request);
            }
            else
            {
                functions.Add(typeof(T), request);
            }
        }

        /// <summary>
        /// Register an action for a message.
        /// </summary>
        /// <typeparam name="T"> Type of message to receive. </typeparam>
        /// <param name="action"> The action that happens when the message is received. </param>
        public void Register<T>(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (actions.ContainsKey(typeof(T)))
            {
                actions[typeof(T)] = (Action<T>)Delegate.Combine(actions[typeof(T)], action);
            }
            else
            {
                actions.Add(typeof(T), action);
            }
        }

        /// <summary>
        /// Send a request.
        /// </summary>
        /// <typeparam name="T"> The type of the parameter of the request. </typeparam>
        /// <typeparam name="R"> The return type of the request. </typeparam>
        /// <param name="parameter"> The parameter. </param>
        /// <returns> The result of the request. </returns>
        public R Request<T, R>(T parameter)
        {
            if (functions.ContainsKey(typeof(T)))
            {
                var function = functions[typeof(T)] as Func<T, R>;
                if (function != null)
                {
                    return function(parameter);
                }
            }
            return default(R);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <typeparam name="T"> The type of message. </typeparam>
        /// <param name="message"> The message. </param>
        public void Send<T>(T message)
        {
            if (actions.ContainsKey(typeof(T)))
            {
                ((Action<T>)actions[typeof(T)])(message);
            }
        }

        /// <summary>
        /// Unregister a request.
        /// </summary>
        /// <typeparam name="T"> The type of request to unregister. </typeparam>
        /// <typeparam name="R"> The return type of the request. </typeparam>
        /// <param name="request"> The request to unregister. </param>
        public void Unregister<T, R>(Func<T, R> request)
        {
            if (functions.ContainsKey(typeof(T)))
            {
                functions[typeof(T)] = Delegate.Remove(functions[typeof(T)], request);
            }
        }

        /// <summary>
        /// Unregister a message.
        /// </summary>
        /// <typeparam name="T"> The type of message. </typeparam>
        /// <param name="action"> The action to unregister. </param>
        public void Unregister<T>(Action<T> action)
        {
            if (actions.ContainsKey(typeof(T)))
            {
                actions[typeof(T)] = (Action<T>)Delegate.Remove(actions[typeof(T)], action);
            }
        }
    }
}
