using System;
using System.Runtime.InteropServices;

// these types is only visible in this source file
using FLOAT_TYPE = System.Double;
using UINT_TYPE = System.UInt64;
using INT_TYPE = System.Int64;

namespace TestFloatPointViewerCSharp
{
    class DoubleDissecter
    {
        public enum Sign
        {
            Positive,
            Negative
        }

        private static readonly UINT_TYPE SignBit = 0x8000000000000000; // MSB is sign bit
        private static readonly UINT_TYPE ExponentBits = 0x7FF0000000000000; // 11 bits of exponent
        private static readonly UINT_TYPE MantissaBits = 0xFFFFFFFFFFFFF; // 52 bits of mantissa
        private static readonly byte ExponentShift = 52;
        private static readonly UINT_TYPE ExponentBias = 1023UL;
        private static readonly UINT_TYPE U_ZERO = 0UL;
        private static readonly INT_TYPE S_ZERO = 0L;
        private static readonly UINT_TYPE U_ONE = 1UL;
        private static readonly FLOAT_TYPE F_ZERO = 0.0;
        private FLOAT_TYPE m_FValue;


        [StructLayout(LayoutKind.Explicit)]
        struct UnionType
        {
            [FieldOffset(0)]
            public FLOAT_TYPE f_val;

            [FieldOffset(0)]
            public UINT_TYPE ui_val;
        }

        public DoubleDissecter(FLOAT_TYPE val) { m_FValue = val; }

        public FLOAT_TYPE GetFloatPoint()
        {
            return m_FValue;
        }

        public void SetFloatPoint(FLOAT_TYPE val)
        {
            m_FValue = val;
        }

        public static void Get(FLOAT_TYPE src, out Sign sign, out UINT_TYPE raw_exponent, out INT_TYPE computed_exponent, out UINT_TYPE mantissa)
        {
            UnionType u;
            u.ui_val = 0; // to silence the compiler of unassigned ui_val member
            u.f_val = src;
            sign = ((u.ui_val & SignBit) > 0) ? Sign.Negative : Sign.Positive;
            raw_exponent = (u.ui_val & ExponentBits) >> ExponentShift;
            computed_exponent = (INT_TYPE)raw_exponent - (INT_TYPE)ExponentBias;
            mantissa = (u.ui_val & MantissaBits);
        }

        public static void Set(out FLOAT_TYPE dest, Sign sign, INT_TYPE computed_exponent, UINT_TYPE mantissa)
        {
            UINT_TYPE sign_value = U_ZERO;
            if (sign == Sign.Negative)
                sign_value |= SignBit;

            UINT_TYPE raw_exponent = (UINT_TYPE)(computed_exponent + (INT_TYPE)(ExponentBias));
            UnionType u;
            u.f_val = F_ZERO; // to silence the compiler of unassigned ui_val member
            u.ui_val = sign_value | (raw_exponent << ExponentShift) | (mantissa & MantissaBits);

            dest = u.f_val;
        }

        public Sign GetSigned()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            return sign;
        }

        public void SetSigned(Sign new_sign)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            Set(out m_FValue, new_sign, computed_exponent, mantissa);
        }

        public UINT_TYPE GetRawExponent()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            return raw_exponent;
        }

        public void SetRawExponent(UINT_TYPE new_raw_exponent)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);
            INT_TYPE new_computed_exponent = (INT_TYPE)(new_raw_exponent) - (INT_TYPE)(ExponentBias);

            Set(out m_FValue, sign, new_computed_exponent, mantissa);
        }

        public INT_TYPE GetComputedExponent()
        {

            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            return computed_exponent;
        }

        public void SetComputedExponent(INT_TYPE new_computed_exponent)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            Set(out m_FValue, sign, new_computed_exponent, mantissa);
        }

        public UINT_TYPE GetMantissa()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            return mantissa;
        }

        public void SetMantissa(UINT_TYPE new_mantissa)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            Set(out m_FValue, sign, computed_exponent, new_mantissa);
        }

        private string Convert2Binary(UINT_TYPE value, byte num_of_bits)
        {
            string str = "";
            UINT_TYPE mask = (U_ONE << num_of_bits);
            for (int i = 0; i < num_of_bits; ++i)
            {
                mask >>= 1;
                if ((value & mask) > 0)
                    str += "1";
                else
                    str += "0";
            }
            return str;
        }

        public void Display()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE computed_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out computed_exponent, out mantissa);

            Console.WriteLine("Sign:{0}, Computed Exponent:{1}, Mantissa:{2}, Float Point Value:{3:G15}\n",
                sign, computed_exponent, Convert2Binary(mantissa, ExponentShift), m_FValue);
        }

    }
}
