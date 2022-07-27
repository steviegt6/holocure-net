using System;

namespace HoloCure.Game.Utils
{
    public static class EnumHelpers
    {
        public static TEnum? AsEnum<TEnum>(this object obj)
            where TEnum : struct, Enum
        {
            bool tryToObject<TObj>(out TEnum? val)
            {
                if (obj is TObj conv)
                {
                    val = (TEnum)Enum.ToObject(typeof(TEnum), conv);
                    return true;
                }

                val = null;
                return false;
            }

            return obj is TEnum @enum ? @enum :
                tryToObject<int>(out TEnum? e) ? e :
                tryToObject<sbyte>(out e) ? e :
                tryToObject<short>(out e) ? e :
                tryToObject<long>(out e) ? e :
                tryToObject<uint>(out e) ? e :
                tryToObject<byte>(out e) ? e :
                tryToObject<ushort>(out e) ? e :
                tryToObject<ulong>(out e) ? e :
                tryToObject<char>(out e) ? e :
                tryToObject<bool>(out e) ? e : null;
        }
    }
}
