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

        public static readonly UINT_TYPE SignBit = 0x8000000000000000; // MSB is sign bit
        public static readonly UINT_TYPE ExponentBits = 0x7FF0000000000000; // 11 bits of exponent
        public static readonly UINT_TYPE MantissaBits = 0xFFFFFFFFFFFFF; // 52 bits of mantissa
        public static readonly byte NumMantissaBits = 52;
        public static readonly byte NumExponentBits = 11;
        public static readonly UINT_TYPE ExponentBias = 1023UL;
        private static readonly UINT_TYPE U_ZERO = 0UL;
        private static readonly INT_TYPE S_ZERO = 0L;
        private static readonly UINT_TYPE U_ONE = 1UL;
        private static readonly FLOAT_TYPE F_ZERO = 0.0;
        private static readonly UINT_TYPE MaxRawExponent = 2047U;

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

        public static void Get(FLOAT_TYPE src, out Sign sign, out UINT_TYPE raw_exponent, out INT_TYPE adjusted_exponent, out UINT_TYPE mantissa)
        {
            UnionType u;
            u.ui_val = 0; // to silence the compiler of unassigned ui_val member
            u.f_val = src;
            sign = ((u.ui_val & SignBit) > 0) ? Sign.Negative : Sign.Positive;
            raw_exponent = (u.ui_val & ExponentBits) >> NumMantissaBits;
            adjusted_exponent = (INT_TYPE)raw_exponent - (INT_TYPE)ExponentBias;
            mantissa = (u.ui_val & MantissaBits);
        }

        public static void Set(out FLOAT_TYPE dest, Sign sign, INT_TYPE adjusted_exponent, UINT_TYPE mantissa)
        {
            UINT_TYPE sign_value = U_ZERO;
            if (sign == Sign.Negative)
                sign_value |= SignBit;

            UINT_TYPE raw_exponent = (UINT_TYPE)(adjusted_exponent + (INT_TYPE)(ExponentBias));
            UnionType u;
            u.f_val = F_ZERO; // to silence the compiler of unassigned ui_val member
            u.ui_val = sign_value | (raw_exponent << NumMantissaBits) | (mantissa & MantissaBits);

            dest = u.f_val;
        }

        public Sign GetSign()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return sign;
        }

        public void SetSign(Sign new_sign)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            Set(out m_FValue, new_sign, adjusted_exponent, mantissa);
        }

        public UINT_TYPE GetRawExponent()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return raw_exponent;
        }

        public void SetRawExponent(UINT_TYPE new_raw_exponent)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);
            INT_TYPE new_adjusted_exponent = (INT_TYPE)(new_raw_exponent) - (INT_TYPE)(ExponentBias);

            Set(out m_FValue, sign, new_adjusted_exponent, mantissa);
        }

        public INT_TYPE GetAdjustedExponent()
        {

            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return adjusted_exponent;
        }

        public void SetAdjustedExponent(INT_TYPE new_adjusted_exponent)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            Set(out m_FValue, sign, new_adjusted_exponent, mantissa);
        }

        public UINT_TYPE GetMantissa()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return mantissa;
        }

        public void SetMantissa(UINT_TYPE new_mantissa)
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            Set(out m_FValue, sign, adjusted_exponent, new_mantissa);
        }

        public bool IsInfinity()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == MaxRawExponent) && (mantissa == U_ZERO);
        }

        public void SetInfinity(Sign new_sign)
        {
            INT_TYPE adjusted_exponent = (INT_TYPE)MaxRawExponent - (INT_TYPE)ExponentBias;
            UINT_TYPE mantissa = U_ZERO;

            Set(out m_FValue, new_sign, adjusted_exponent, mantissa);
        }

        public bool IsPositiveInfinity()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == MaxRawExponent) && (sign == Sign.Positive) && (mantissa == U_ZERO);
        }

        public bool IsNegativeInfinity()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == MaxRawExponent) && (sign == Sign.Negative) && (mantissa == U_ZERO);
        }

        public bool IsSubnormal()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == U_ZERO) && (mantissa > U_ZERO);
        }

        public bool IsZero()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == U_ZERO) && (mantissa == U_ZERO); // zero can be positive or negative depending on the sign.
        }

        public void SetZero(Sign new_sign)
        {
            INT_TYPE adjusted_exponent = -(INT_TYPE)ExponentBias;
            UINT_TYPE mantissa = U_ZERO;

            Set(out m_FValue, new_sign, adjusted_exponent, mantissa);
        }

        public bool IsNaN()
        {
            Sign sign = Sign.Positive;
            UINT_TYPE raw_exponent = U_ZERO;
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            return (raw_exponent == MaxRawExponent) && (mantissa > U_ZERO);
        }

        public void SetNaN(Sign new_sign, UINT_TYPE non_zero_mantissa)
        {
            INT_TYPE adjusted_exponent = (INT_TYPE)(MaxRawExponent - ExponentBias);
            if (non_zero_mantissa == U_ZERO)
                non_zero_mantissa = U_ONE;
            UINT_TYPE mantissa = non_zero_mantissa & MantissaBits;

            Set(out m_FValue, new_sign, adjusted_exponent, mantissa);
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
            INT_TYPE adjusted_exponent = S_ZERO;
            UINT_TYPE mantissa = U_ZERO;
            Get(m_FValue, out sign, out raw_exponent, out adjusted_exponent, out mantissa);

            Console.WriteLine("Sign:{0}, Adjusted Exponent:{1}, Raw Exponent:{2}, Mantissa:{3}, Float Point Value:{4:G15}\n",
                sign, adjusted_exponent, Convert2Binary(raw_exponent, NumExponentBits), Convert2Binary(mantissa, NumMantissaBits), m_FValue);
        }

    }
}
