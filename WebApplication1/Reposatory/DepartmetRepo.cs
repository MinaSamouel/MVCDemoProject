using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface IDepartmetRepo
{
    List<Department> GetAll();
    Department GetById(int? id);
    void Add(Department department);
    void Update(Department department);
    void Delete(int? id);
}

public class DepartmetRepo : IDepartmetRepo
{
    private readonly ItiDemoContext _context;

    public DepartmetRepo(ItiDemoContext context)
    {
        _context = context;
    }

    public List<Department> GetAll()
    {
        return _context.Departments.ToList();
    }

    public Department GetById(int? id)
    {
        return  _context.Departments.FirstOrDefault(d => d.DepartmentId == id );
    }

    public  void Add(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChangesAsync();
    }

    public void Update(Department department)
    {
        _context.Entry(department).State = EntityState.Modified;
        _context.SaveChangesAsync();
    }

    public  void Delete(int? id)
    {
        var department = GetById(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            _context.SaveChangesAsync();
        }

    }
}