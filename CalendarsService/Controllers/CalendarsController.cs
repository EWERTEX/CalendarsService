using CalendarsService.Models;
using CalendarsService.Models.Enums;
using CalendarsService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CalendarsService.Controllers;

//Создание контроллера для обработки запросов
[Route("api/[controller]")]
[ApiController]
public class CalendarsController(CalendarRepository calendarRepository, HttpClient httpClient) : ControllerBase
{
    //Обработчик GET запроса
    [HttpGet("{inputDate}")]
    //Дата как строка, чтобы принимать любые форматы даты, в том числе DD.MM.YYYY, как было указано в примере ТЗ
    //Но является нарушением стандарта ISO-8601
    public ActionResult<string> Get(string inputDate)
    {
        //Проверка на возможность приведения к дате
        if (!DateOnly.TryParse(inputDate, out var date))
            return BadRequest(); //Отправка кода 400 при некорректном запросе

        //Проверка на заполнение БД
        //Если ещё нет ни одной даты с годом из запроса, календарь заполняется на этот год
        if (calendarRepository.GetAll().All(c => c.Date.Year != date.Year))
            FillCalendar(date.Year);

        //Получение календаря и его отправка с кодом 200
        var calendar = calendarRepository.Get(date);

        return Ok(calendar?.TypeOfDay.GetDisplayName());
    }

    //Метод заполнения календаря
    [NonAction]
    private void FillCalendar(int year)
    {
        //Отправка запроса на сервис isdayoff и получение ответа
        var request = new Uri($"https://isdayoff.ru/api/getdata?year={year}&pre=1");
        var response = httpClient.GetStringAsync(request);
        if (string.IsNullOrEmpty(response.Result)) return;

        //Заполнение базы данных на запрошенный год
        var days = response.Result.Select(d => d.ToString()).ToArray();
        var currentDate = new DateOnly(year, 1, 1);
        foreach (var day in days)
        {
            var calendar = new Calendar { Date = currentDate, TypeOfDay = (TypeOfDay)Convert.ToInt32(day) };
            calendarRepository.Create(calendar);
            currentDate = currentDate.AddDays(1);
        }
    }
}