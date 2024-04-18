using Person.Visitor;

namespace Person.Element
{
    // this is the "VisitableObject"
    public interface IOrgElement
    {
        string Name { get; set; }
        void Accept(IVisitor visitor);
    }
}