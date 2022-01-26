using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class MyEvent
    {
        public MyEvent(bool a, bool g)
        {

        }
        public event EventHandler<FlagsEventArgs> myEvent;

        protected virtual void OnMyEvent(FlagsEventArgs args)
        {
            var t = myEvent;
            if (t != null)
                t(this, args);
        }

        public void MethodOfCallingTheEvent (string mes)
        {
            FlagsEventArgs e = new FlagsEventArgs(mes);

            OnMyEvent(e);
        }
    }
}
