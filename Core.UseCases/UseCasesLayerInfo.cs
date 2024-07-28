using System.Reflection;
using P3Model.Annotations.Domain;
using P3Model.Annotations.Technology.CleanArchitecture;

[assembly: DomainModel]
[assembly: UseCasesLayer]

namespace Core.UseCases;

public class UseCasesLayerInfo
{
    public static Assembly Assembly => typeof(UseCasesLayerInfo).Assembly;
}