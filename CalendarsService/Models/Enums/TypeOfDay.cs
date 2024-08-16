using System.ComponentModel.DataAnnotations;

namespace CalendarsService.Models.Enums;

//Перечисление для типа дня
public enum TypeOfDay
{
    //Задаётся отображаемое название через атрибут Display
    [Display(Name = "Рабочий")] Working = 0,
    [Display(Name = "Выходной")] Weekend = 1,
    [Display(Name = "Сокращённый")] Shortened = 2
}