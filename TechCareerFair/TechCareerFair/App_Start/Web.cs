using System;
using System.Net;
using System.Threading.Tasks;

namespace TechCareerFair
{
    internal class Web
    {
        private NetworkCredential credentials;

        public Web(NetworkCredential credentials)
        {
            this.credentials = credentials;
        }

        internal Task DeliverAsync(SendGridMessage myMessage)
        {
            throw new NotImplementedException();
        }
    }
}