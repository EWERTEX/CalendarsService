using CalendarsService.Models;

namespace CalendarsService.Repositories;

public class CalendarRepository(ApplicationContext context) : IDisposable
{
    public IEnumerable<Calendar> GetAll()
    {
        return context.Calendars;
    }

    public Calendar? Get(DateOnly date)
    {
        return context.Calendars.Find(date);
    }

    public Calendar Create(Calendar calendar)
    {
        context.Calendars.Add(calendar);

        context.SaveChanges();
        return calendar;
    }

    public Calendar? Update(Calendar calendar)
    {
        var updatingCalendar = context.Calendars.FirstOrDefault(c => c.Date == calendar.Date);

        if (updatingCalendar == null)
            return updatingCalendar;

        updatingCalendar.TypeOfDay = calendar.TypeOfDay;

        context.SaveChanges();
        return calendar;
    }

    public Calendar? Delete(DateOnly date)
    {
        var calendar = context.Calendars.Find(date);

        if (calendar != null)
            context.Calendars.Remove(calendar);

        context.SaveChanges();
        return calendar;
    }

    //Реализация интерфейса IDisposable для очистки неуправляемых данных, таких как подключение к БД
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}