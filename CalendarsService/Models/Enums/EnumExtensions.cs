using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CalendarsService.Models.Enums;

//Расширение Enum для вывода имя из атрибута Display
public static class EnumExtensions
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        var displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();

        if (string.IsNullOrEmpty(displayName))
        {
            displayName = enumValue.ToString();
        }

        return displayName;
    }
}