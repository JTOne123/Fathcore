﻿namespace Fathcore.Infrastructure
{
    /// <summary>
    /// A statically compiled "singleton" used to store objects throughout the 
    /// lifetime of the app domain. Not so much singleton in the pattern's 
    /// sense of the word as a standardized way to store single instances.
    /// </summary>
    /// <typeparam name="T">The type of object to store.</typeparam>
    /// <remarks>Access to the instance is not synchronized.</remarks>
    public class Singleton<T> : BaseSingleton
    {
        private static T s_instance;

        /// <summary>
        /// The singleton instance for the specified type T. Only one instance (at the time) of this object for each type of T.
        /// </summary>
        public static T Instance
        {
            get
            {
                var baseSingletonHasKey = AllSingletons.ContainsKey(typeof(T));

                if (s_instance == null && AllSingletons.TryGetValue(typeof(T), out var value))
                    s_instance = (T)value;

                if (!baseSingletonHasKey)
                    AllSingletons[typeof(T)] = s_instance;

                return s_instance;
            }
            set
            {
                s_instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
