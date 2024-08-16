using CalendarsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarsService;

//Контекст приложения
public class ApplicationContext: DbContext
{
    //Календари
    public DbSet<Calendar> Calendars { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Задаётся строка подключения
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=calendarservicedb;Trusted_Connection=Yes;Integrated Security=true;encrypt=false;");
    }
}