using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using CalendarsService.Models.Enums;

namespace CalendarsService.Models;

//Модель календаря
public class Calendar
{
    [Key] public DateOnly Date { get; set; } //Дата как первичный ключ
    
    //День недели без setter'а и не хранится в БД, так как вытаскивается из поля Date
    public DayOfWeek DayOfWeek => Date.DayOfWeek; //День недели

    //Версия поля с settr"ом и хранением в БД
    // public DayOfWeek DayOfWeek
    // {
    //     get => Date.DayOfWeek;
    //
    //     set
    //     {
    //         if (value != DayOfWeek)
    //             DayOfWeek = value;
    //     }
    // }

    [MaxLength(11)] public TypeOfDay TypeOfDay { get; set; } //Тип дня
}