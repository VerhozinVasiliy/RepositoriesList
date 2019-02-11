using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace LookingForRepos.ViewModel
{
    public abstract class ViewModelBase : NotificationObject
    {
        #region Constructor

        protected ViewModelBase()
        {
        }

        #endregion // Constructor

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members


        #endregion // INotifyPropertyChanged Members
    }
}
