using System;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public abstract class Book : NamedObject, IBook
    {
        public abstract event GradeAddedDelegate GradeAdded;

        public Book(string name) : base(name)
        {}

        public abstract Statistics GetStatistics();
        public abstract void AddGrade(double grade);
    }
}