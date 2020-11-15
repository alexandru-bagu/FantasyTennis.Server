using Microsoft.Extensions.DependencyInjection;
using System;

public static class IServiceProviderExtensions
{
    public static TInstance Create<TInstance>(this IServiceProvider provider, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance<TInstance>(provider, parameters);
    }
    public static TInstance Create<TInstance>(this IServiceProvider provider, Type type, params object[] parameters)
    {
        return (TInstance)ActivatorUtilities.CreateInstance(provider, type, parameters);
    }
}
