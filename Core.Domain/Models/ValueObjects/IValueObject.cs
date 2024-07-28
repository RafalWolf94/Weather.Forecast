using P3Model.Annotations.Domain.DDD;

namespace Core.Domain.Models.ValueObjects;

[DddValueObject]
public interface IValueObject<T>
{
    T Value { get; init; }
}