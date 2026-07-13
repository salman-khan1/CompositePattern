using CompositePattern;

class Program
{
    static void Main() 
    {
        FileItem resume = new FileItem("Resume.pdf");
        FileItem notes = new FileItem("Notes.txt");

        Folder documents = new Folder("Documents");
        documents.Add(resume);
        documents.Add(notes);

        documents.Display();
    }
}