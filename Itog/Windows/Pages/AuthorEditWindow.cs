using Itog.Class;
using System;

namespace Itog.Windows.Pages
{
    internal class AuthorEditWindow
    {
        private AuthorRepository authorRepository;

        public AuthorEditWindow(AuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        internal bool ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}