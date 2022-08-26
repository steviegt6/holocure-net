using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HoloCure.Reflection
{
    public static class Cache
    {
        #region Binding Flags

        /// <summary>
        ///     Encompasses <see cref="BindingFlags.Public" /> and <see cref="BindingFlags.NonPublic" />.
        /// </summary>
        public const BindingFlags PUBLICITY_FLAGS = BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        ///     Encompasses <see cref="BindingFlags.Static" /> and <see cref="BindingFlags.Instance" />.
        /// </summary>
        public const BindingFlags INSTANCE_FLAGS = BindingFlags.Static | BindingFlags.Instance;

        /// <summary>
        ///     Grants access to members regardless of visibility and static status.
        /// </summary>
        public const BindingFlags UNIVERSAL_FLAGS = PUBLICITY_FLAGS | INSTANCE_FLAGS;

        #endregion

        /// <summary>
        ///     The type of reflected member.
        /// </summary>
        public enum CacheType
        {
            Field,
            Method,
            Property,
            Constructor,
            Type
        }

        #region Cache Definition

        private static readonly Dictionary<CacheType, Dictionary<string, object?>> ReflectionCache = new()
        {
            [CacheType.Field] = new Dictionary<string, object?>(),
            [CacheType.Method] = new Dictionary<string, object?>(),
            [CacheType.Property] = new Dictionary<string, object?>(),
            [CacheType.Constructor] = new Dictionary<string, object?>(),
            [CacheType.Type] = new Dictionary<string, object?>()
        };

        private static T? RetrieveFromCache<T>(CacheType cacheType, string key, Func<T?> provider) {
            if (ReflectionCache[cacheType].ContainsKey(key)) return (T?) ReflectionCache[cacheType][key];

            T? value = provider();
            ReflectionCache[cacheType].Add(key, value);
            return value;
        }

        #endregion

        #region Name Suppliers

        public static string GetFieldName(Type type, string field) {
            return Naming.GetTypeName(type) + " " + field;
        }

        public static string GetPropertyName(Type type, string property) {
            return Naming.GetTypeName(type) + " " + property;
        }

        public static string GetMethodName(Type type, string method, Type[] signature, int genericCount) {
            return GetMethodSignature(Naming.GetTypeName(type) + "::" + method, signature, genericCount);
        }

        public static string GetConstructorName(Type type, Type[] signature) {
            return GetMethodSignature(Naming.GetTypeName(type) + "::" + ".ctor", signature, 0);
        }

        public static string GetMethodSignature(string name, Type[] signature, int genericCount) {
            StringBuilder builder = new();

            //builder.Append(Cecil.GetTypeName(returnType));
            //builder.Append(' ');
            builder.Append(name);

            if (genericCount != 0) {
                builder.Append('<');

                for (int i = 0; i < genericCount; i++) builder.Append($"T{i}");

                builder.Append('>');
            }

            builder.Append('(');

            for (int i = 0; i < signature.Length; i++) {
                if (i > 0) builder.Append(',');

                builder.Append(Naming.GetTypeName(signature[i]));
            }

            builder.Append(')');

            return builder.ToString();
        }

        #endregion

        #region Nullable Cache Retrieval

        public static Type? GetCachedTypeNullable(this Assembly assembly, string name) {
            return RetrieveFromCache(
                CacheType.Type,
                name,
                () => assembly.GetType(name)
            );
        }

        public static FieldInfo? GetCachedFieldNullable(this Type type, string name) {
            return RetrieveFromCache(
                CacheType.Field,
                GetFieldName(type, name),
                () => type.GetField(name, UNIVERSAL_FLAGS)
            );
        }

        public static PropertyInfo? GetCachedPropertyNullable(this Type type, string name) {
            return RetrieveFromCache(
                CacheType.Property,
                GetPropertyName(type, name),
                () => type.GetProperty(name, UNIVERSAL_FLAGS)
            );
        }

        public static MethodInfo? GetCachedMethodNullable(
            this Type type,
            string name,
            Type[]? signature = null,
            int genericCount = 0
        ) {
            bool hasSignature = signature is not null;
            bool hasGenerics = genericCount != 0;

            if (!hasSignature && !hasGenerics)
                return RetrieveFromCache(
                    CacheType.Method,
                    GetMethodName(type, name, Array.Empty<Type>(), genericCount),
                    () => type.GetMethod(name, UNIVERSAL_FLAGS)
                );

            if (hasSignature && !hasGenerics)
                return RetrieveFromCache(
                    CacheType.Method,
                    GetMethodName(type, name, signature!, genericCount),
                    () => type.GetMethod(name, UNIVERSAL_FLAGS, null, signature!, null)
                );

            if (!hasSignature && hasGenerics) throw new ArgumentNullException(nameof(signature), "Generic count cannot be used without signature.");

            return RetrieveFromCache(
                CacheType.Method,
                GetMethodName(type, name, signature!, genericCount),
                () => type.GetMethod(name, genericCount, UNIVERSAL_FLAGS, null, signature!, null)
            );
        }

        public static ConstructorInfo? GetCachedConstructorNullable(this Type type, Type[] signature) {
            return RetrieveFromCache(CacheType.Constructor,
                                     GetConstructorName(type, signature),
                                     () => type.GetConstructor(UNIVERSAL_FLAGS, null, signature, null)
            );
        }

        #endregion

        #region Non-null Cache Retrieval

        public static Type GetCachedType(this Assembly assembly, string name) {
            return GetCachedTypeNullable(assembly, name)!;
        }

        public static FieldInfo GetCachedField(this Type type, string name) {
            return GetCachedFieldNullable(type, name)!;
        }

        public static PropertyInfo GetCachedProperty(this Type type, string name) {
            return GetCachedPropertyNullable(type, name)!;
        }

        public static MethodInfo GetCachedMethod(
            this Type type,
            string name,
            Type[]? signature = null,
            int genericCount = 0
        ) {
            return GetCachedMethodNullable(type, name, signature, genericCount)!;
        }

        public static ConstructorInfo GetCachedConstructor(this Type type, Type[] signature) {
            return GetCachedConstructorNullable(type, signature)!;
        }

        #endregion
    }
}