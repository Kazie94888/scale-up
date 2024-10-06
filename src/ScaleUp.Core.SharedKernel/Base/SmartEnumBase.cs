using Ardalis.SmartEnum;

namespace ScaleUp.Core.SharedKernel.Base;

public class SmartEnumBase<T>(string name, int value) : SmartEnum<T>(name, value) where T : SmartEnum<T, int>
{
    public static implicit operator string(SmartEnumBase<T> smartEnum) => smartEnum.Name;
    public static implicit operator int(SmartEnumBase<T> smartEnum) => smartEnum.Value;
}