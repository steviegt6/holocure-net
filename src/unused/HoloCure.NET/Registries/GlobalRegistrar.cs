using HoloCure.Registry;

namespace HoloCure.NET.Registries
{
    public static class GlobalRegistrar
    {
        #region Main Registrars

        public static readonly MutableRegistrar Registrar = new();

        // public static readonly MutableRegistrar<Screen> ScreenRegistrar = MakeMutableRegistrar<Screen>(new Identifier("screens"));

        #endregion

        #region Factories

        public static MutableRegistrar<T> MakeMutableRegistrar<T>(Identifier id)
            where T : notnull {
            MutableRegistrar<T> registrar = new();
            return RegisterRegistrar(id, registrar);
        }

        #endregion
        
        #region Static Registrar Impl

        public static T RegisterRegistrar<T>(Identifier id, T registrar)
            where T : class, IRegistrar {
            return (T) Registrar.Register(id, registrar);
        }

        public static T? GetRegistrar<T>(Identifier id)
            where T : class, IRegistrar {
            return (T?) Registrar.Get(id);
        }

        public static Identifier? GetRegistrarId<T>(T registrar)
            where T : class, IRegistrar {
            return Registrar.GetId(registrar);
        }

        #endregion
    }
}