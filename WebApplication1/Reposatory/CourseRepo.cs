using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface ICourseRepo
{
    List<Course> GetAll();
    Course? GetById(int? id);
    void Add(Course course);
    void Update(Course course);
    void Delete(int? id);
}

public class CourseRepo : ICourseRepo
{
    private readonly ItiDemoContext _context;

    public CourseRepo(ItiDemoContext context)
    {
        _context = context;
    }

    public  List<Course> GetAll()
    {
        return  _context.Courses.ToList();
    }

    public  Course? GetById(int? id)
    {

        return  _context.Courses.FirstOrDefault(c => c.CourseId == id);
    }

    public  void Add(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
    }

    public void Update(Course course)
    {
        
        _context.Courses.Update(course);
        _context.SaveChanges();
        
    }

    public void Delete(int? id)
    {
        var course = GetById(id);
        if (course == null)
        {
        }

        _context.Courses.Remove(course);
        _context.SaveChanges();
    }


}

