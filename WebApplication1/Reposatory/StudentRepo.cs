using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface IStudentRepo
{
    List<Student> GetAll();
    Student GetById(int? id);
    void Add(Student student);
    void Update(Student student);
    void Delete(int? id);
}

public class StudentRepo : IStudentRepo
{
    private readonly ItiDemoContext _context;

    public StudentRepo(ItiDemoContext context)
    {
        _context = context;
    }

    public List<Student> GetAll()
    {
        return  _context.Students.Include(s => s.Department).ToList();
    }

    public  Student GetById(int? id)
    {
        return _context.Students.Include(s =>s.Department).FirstOrDefault(s => s.StudentId == id);
    }

    public void Add(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public  void Update(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void Delete(int? id)
    {
        var student = GetById(id);
        if (student == null) return;
        _context.Students.Remove(student);
        _context.SaveChanges();
    }
}