using Itog.Class;
using System;

namespace Itog.Windows.Pages
{
    internal class BookEditWindow
    {
        private BookRepository bookRepository;

        public BookEditWindow(BookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        internal bool ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}