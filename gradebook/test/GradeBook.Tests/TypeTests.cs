using System;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {
        int count = 0;
        public delegate string WriteLogDelegate(string logMessage);
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;
            //log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello");

            Assert.Equal(3, count);
        }
        string IncrementCount(string message)
        {
            count++;
            return message;
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Asshole";

            string upper = MakeUppperCase(name);

            Assert.Equal("Asshole", name);
            Assert.Equal("ASSHOLE", upper);
        }
        private string MakeUppperCase(string p)
        {
            return p.ToUpper();
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }
        private int GetInt()
        {
            return 3;
        }
        private void SetInt(ref int x)
        {
            x = 42;
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            // arrange
            var book1 = GetBook("Book 1");

            // act
            SetName(book1, "New Name");

            // assert
            Assert.Equal("New Name",book1.Name);
        }
        void SetName(InMemoryBook book, string newName)
        {
            book.Name = newName;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            // arrange
            var book1 = GetBook("Book 1");

            // act
            GetBookSetName(book1, "New Name");

            // assert
            Assert.Equal("Book 1",book1.Name);
        }
        void GetBookSetName(InMemoryBook book, string newName)
        {
            book = new InMemoryBook(newName);
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            // arrange
            var book1 = GetBook("Book 1");

            // act
            GetBookSetNameByRef(ref book1, "New Name");

            // assert
            Assert.Equal("New Name",book1.Name);
        }
        void GetBookSetNameByRef(ref InMemoryBook book, string newName)
        {
            book = new InMemoryBook(newName);
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            // act

            // assert
            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 2",book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            // arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            // act

            // assert
            Assert.Same(book1, book2);
            Assert.True(object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}