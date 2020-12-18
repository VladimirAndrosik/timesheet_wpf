using System;
using System.Collections.Generic;
using System.Linq;

namespace timesheet.core.Singleton
{
    public sealed class SingletonInstances
    {
        private static readonly List<object> Instances = new List<object>();
        public static object GetEmployeeService(Type T)
        {
            foreach (object obj in Instances.Where(obj => obj.GetType() == T))
            {
                return obj;
            }         

            // create an object of the type
            object newObject = Activator.CreateInstance(T);
            Instances.Add(newObject);
            return newObject;
        }
    }
}
