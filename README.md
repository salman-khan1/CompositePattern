# Composite Design Pattern

The **Composite Pattern** is a **Structural Design Pattern** that lets you treat:

* A single object
* A group of objects

in the **same way**.

> **Core idea:** A container and the objects inside it share a common interface.

---

## 📌 Simple Definition

> The Composite Pattern allows individual objects and collections of objects to be treated uniformly through a common interface.

---

## 🌳 Real-World Example: File System

Consider a computer's file system:

```text
Documents
├── Resume.pdf
├── Notes.txt
└── Projects
    ├── Design.pdf
    └── Report.docx
```

There are two types of objects:

### File — Leaf

A file is an individual object and cannot contain other files.

```text
Resume.pdf
```

### Folder — Composite

A folder is a container that can contain:

* Files
* Other folders

```text
Documents
├── Resume.pdf
└── Projects
    └── Design.pdf
```

The important part is that **both File and Folder can implement the same interface**.

---

# ❌ Problem Without Composite

Without Composite, you may need different methods for different object types:

```csharp
file.Open();
folder.OpenFolder();
```

And client code may become full of type checking:

```csharp
if (item is File)
{
    // Handle file
}
else if (item is Folder)
{
    // Handle folder
}
```

This makes the code harder to maintain and extend.

---

# ✅ Solution

Create a common interface:

```text
              IFileSystem
                  ▲
                  │
            ┌─────┴─────┐
            │           │
          File        Folder
         (Leaf)      (Composite)
```

Both objects expose the same operation:

```csharp
Display();
```

Now the client doesn't need to know whether it is dealing with a file or a folder.

---

# 🧩 Structure of Composite Pattern

The Composite Pattern usually contains three important parts:

| Component     | Responsibility                               |
| ------------- | -------------------------------------------- |
| **Component** | Defines the common interface                 |
| **Leaf**      | Represents an individual object              |
| **Composite** | Represents a container containing Components |

### Example

```text
IFileSystem       → Component
      │
      ├── File    → Leaf
      │
      └── Folder  → Composite
```

---

# 💻 C# Example

## 1. Component

Define the common interface:

```csharp
public interface IFileSystem
{
    void Display();
}
```

Both files and folders will implement this interface.

---

## 2. Leaf — File

A `File` is an individual object.

```csharp
public class File : IFileSystem
{
    private string _name;

    public File(string name)
    {
        _name = name;
    }

    public void Display()
    {
        Console.WriteLine("File: " + _name);
    }
}
```

A file cannot contain another file or folder.

Therefore, it is called a **Leaf**.

---

## 3. Composite — Folder

A folder can contain files and other folders.

```csharp
public class Folder : IFileSystem
{
    private string _name;

    private List<IFileSystem> _items = new List<IFileSystem>();

    public Folder(string name)
    {
        _name = name;
    }

    public void Add(IFileSystem item)
    {
        _items.Add(item);
    }

    public void Display()
    {
        Console.WriteLine("Folder: " + _name);

        foreach (var item in _items)
        {
            item.Display();
        }
    }
}
```

The important part is:

```csharp
List<IFileSystem>
```

Because the list uses the common interface, a folder can contain:

```text
File
Folder
File
Folder
...
```

---

# 4. Client Code

```csharp
class Program
{
    static void Main()
    {
        File file1 = new File("Resume.pdf");
        File file2 = new File("Notes.txt");

        Folder documents = new Folder("Documents");

        documents.Add(file1);
        documents.Add(file2);

        documents.Display();
    }
}
```

### Output

```text
Folder: Documents
File: Resume.pdf
File: Notes.txt
```

---

# 🌲 Nested Composite Objects

The real power of Composite appears when composites can contain other composites.

For example:

```text
Documents
│
├── Resume.pdf
├── Notes.txt
│
└── Projects
    │
    ├── Design.pdf
    └── Report.docx
```

We can represent this using:

```csharp
Folder documents = new Folder("Documents");

Folder projects = new Folder("Projects");

File design = new File("Design.pdf");
File report = new File("Report.docx");

projects.Add(design);
projects.Add(report);

documents.Add(projects);

documents.Display();
```

### Output

```text
Folder: Documents
Folder: Projects
File: Design.pdf
File: Report.docx
```

Notice this:

```csharp
documents.Add(projects);
```

A `Folder` was added to another `Folder`.

That's the key feature of the **Composite Pattern**.

---

# 🔑 Why Does This Work?

Because both `File` and `Folder` implement:

```csharp
IFileSystem
```

Therefore:

```csharp
documents.Add(projects);
```

works because `projects` is an `IFileSystem`.

And:

```csharp
documents.Add(file);
```

also works because `file` is an `IFileSystem`.

The client only needs to understand:

```text
IFileSystem
```

It doesn't need to know the concrete type.

This is **polymorphism** doing the work.

---

# 🔥 Before vs After

## Without Composite

```csharp
if (item is File)
{
    // Handle File
}
else if (item is Folder)
{
    // Handle Folder
}
```

The client needs to understand different object types.

---

## With Composite

```csharp
item.Display();
```

That's it.

The object itself knows how to perform the operation.

---

# 🏢 Another Example: Company Organization

Composite isn't limited to file systems.

Consider a company:

```text
CEO
│
├── Manager A
│   ├── Employee 1
│   └── Employee 2
│
└── Manager B
    └── Employee 3
```

We can have a common interface:

```csharp
public interface IEmployee
{
    decimal GetSalary();
}
```

An employee is a **Leaf**.

A manager is a **Composite** because a manager can contain employees or other managers.

```text
IEmployee
    │
    ├── Employee
    │
    └── Manager
```

The client can call:

```csharp
employee.GetSalary();
```

without caring whether `employee` is an individual employee or a manager containing multiple employees.

---

# 🌍 Real-World Use Cases

Composite is useful whenever you have a **tree-like hierarchy**.

### 📁 File Systems

```text
Folder
├── File
├── File
└── Folder
```

### 🌐 HTML DOM

```text
<div>
├── <p>
├── <span>
└── <div>
```

HTML elements can contain other elements.

### 🖥️ UI Components

```text
Panel
├── Button
├── Label
└── Panel
    ├── Button
    └── TextBox
```

A panel contains UI components, while individual controls are leaves.

### 🏢 Organization Structure

```text
Manager
├── Employee
├── Employee
└── Manager
    └── Employee
```

### 🍽️ Restaurant Menu

```text
Menu
├── MenuItem
├── MenuItem
└── SubMenu
    ├── MenuItem
    └── MenuItem
```

---

# 🎯 When Should You Use Composite?

Use the Composite Pattern when:

* You have a **tree structure**.
* Objects can contain other objects.
* You want to treat individual objects and groups uniformly.
* You want to avoid type-checking and excessive `if/else` logic.
* You want clients to work with a common abstraction.

### Typical Structure

```text
                Component
                    │
          ┌─────────┴─────────┐
          │                   │
        Leaf              Composite
                              │
                              └── Components
```

---

# ⭐ Advantages

### 1. Simplifies Client Code

The client works with the common interface:

```csharp
item.Display();
```

instead of checking object types.

### 2. Supports Tree Structures

Composite naturally represents hierarchical structures:

```text
Root
├── Child
├── Child
└── Parent
    ├── Child
    └── Child
```

### 3. Uses Polymorphism

The client doesn't need to know whether it has a leaf or composite.

### 4. Easy to Extend

New component types can be added by implementing the common interface.

---

# ⚠️ Possible Disadvantages

Composite isn't always the best choice.

### 1. Can Make the Design More General

The common interface may contain operations that don't make sense for every object.

For example:

```csharp
Add()
Remove()
```

make sense for a `Folder`, but not necessarily for a `File`.

### 2. Can Make Type Constraints Less Obvious

Because everything follows the same interface, the compiler may allow combinations that aren't meaningful for your domain.

Use Composite when the **part-whole relationship is natural**.

---

# 🧠 Easy Memory Trick

Think about a **family tree**:

```text
Grandfather
│
├── Father
│   ├── Son
│   └── Daughter
│
└── Uncle
    └── Cousin
```

Everyone is a `Person`.

Some people have children.

Some don't.

But the system can still treat everyone as a:

```text
Person
```

That's the essence of Composite.

> **Some objects contain other objects, while others don't — but they can all be treated through the same interface.**

---

# 🎤 Interview Definition

> **Composite Pattern is a structural design pattern that allows objects to be composed into tree structures and lets clients treat individual objects and groups of objects uniformly through a common interface.**

---

# ❓ Common Interview Question

### Q: Why do we use Composite Pattern?

**Answer:**

> We use the Composite Pattern when we need to represent part-whole hierarchies, such as files and folders, UI components, menus, or organizational structures. It allows individual objects and collections of objects to be treated uniformly through a common interface.

---

# ❓ How Do You Identify a Composite Pattern?

Look for these three things:

```text
1. Common Interface
       ↓
2. Leaf + Composite
       ↓
3. Composite contains Components
```

For example:

```text
IFileSystem
     │
 ┌───┴────┐
 │        │
File    Folder
         │
         └── List<IFileSystem>
```

If you see this structure, you are probably looking at a Composite Pattern.

---

# 📌 Key Takeaway

The entire pattern can be remembered with one sentence:

> **"Treat a single object and a group of objects the same way."**

Or even simpler:

```text
Single + Group
     ↓
Same Interface
     ↓
Uniform Treatment
```

### Example

```text
             IFileSystem
                  │
          ┌───────┴───────┐
          │               │
        File            Folder
        Leaf           Composite
                          │
                    ┌─────┴─────┐
                    │           │
                  File        Folder
```

That's the **Composite Design Pattern**.
