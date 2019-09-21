using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meta.PoorMansDi
{
    public class SimpleDi : IBinder, IResolver, IInjector
    {
        private readonly Dictionary<Type, Binding> bindings = new Dictionary<Type, Binding>();
        private readonly Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        public SimpleDi()
        {
            Injector = this;
            Bind<IBinder>().ToSingle(this);
            Bind<IResolver>().ToSingle(this);
            Bind<IInjector>().ToSingle(this);
        }

        public static IInjector Injector { get; private set; }

        public IBinding Bind<T>()
        {
            var type = typeof(T);
            if (bindings.ContainsKey(type))
            {
                throw new Exception($"This type already binded to {bindings[type]}");
            }

            var binding = new Binding();
            bindings.Add(type, binding);
            return binding;
        }

        public void InjectInto(object o)
        {
            var type = o.GetType();
            if (type.IsSubclassOf(typeof(Component)))
            {
                InjectIntoUnityComponent(o as Component, type);
            }
        }

        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T) Resolve(type);
        }

        private object Resolve(Type type)
        {
            if (singletons.ContainsKey(type))
            {
                return singletons[type];
            }

            if (!bindings.ContainsKey(type)) throw new KeyNotFoundException($"No binding found for type {type}");
            var binding = bindings[type];
            if (binding.IsSingleton)
            {
                var singleton = CreateInstance(binding.Type);
                singletons.Add(type, singleton);
                return singleton;
            }

            return CreateInstance(type);

        }

        private object CreateInstance(Type type)
        {
            var constructors = type.GetConstructors();

            var constructor = constructors[0];
            var maxParams = constructor.GetParameters().Length;
            for (var i = 1; i < constructors.Length; i++)
            {
                var currentCtor = constructors[i];
                var paramsCount = currentCtor.GetParameters().Length;
                if (paramsCount > maxParams)
                {
                    maxParams = paramsCount;
                    constructor = currentCtor;
                }
            }

            var parameters = constructor.GetParameters();
            var values = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++) values[i] = Resolve(parameters[i].ParameterType);

            var result = constructor.Invoke(values);
            return result;
        }

        private void InjectIntoUnityComponent(Component component, Type type)
        {
            var methodInfo = type.GetMethod("Init");
            if (methodInfo == null)
            {
                return;
            }
            var parameterInfos = methodInfo.GetParameters();
            var actualParams = parameterInfos.Select(info => Resolve(info.ParameterType)).ToArray();
            methodInfo.Invoke(component, actualParams);
        }
    }

    public interface IInjector
    {
        void InjectInto(object o);
    }

    public class Binding : IBinding
    {
        public bool IsSingleton { get; private set; }
        public bool IsValue { get; private set; }
        public Type Type { get; set; }

        public object Value { get; set; }

        public void To<T>()
        {
            Type = typeof(T);
        }

        public void ToSingle<T>()
        {
            Type = typeof(T);
            IsSingleton = true;
        }

        public void ToSingle(object o)
        {
            Value = o;
            IsValue = true;
        }
    }

    public interface IBinder
    {
        IBinding Bind<T>();
    }

    public interface IBinding
    {
        void To<T>();
        void ToSingle<T>();
        void ToSingle(object o);
    }

    public interface IResolver
    {
        T Resolve<T>();
    }
}