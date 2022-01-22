using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class MyEvent
    {
        public delegate void EventDelegate();
        public event EventDelegate myEvent = null;

        public void InvokeEvent()
        {
            myEvent.Invoke();
        }
    }
}
