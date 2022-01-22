using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHierarchy
{
    class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            if(NewMail != null)
            {
                NewMail(this, e);
            }
        }

        public void SimulateNewMail( string from, string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);
            OnNewMail(e);
        }
    }
}
