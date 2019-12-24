#pragma once
#include <iostream>
#include <iomanip>
#include <string>
#include <cstdint>

// Dissector for 32bit float
class FloatDissecter
{
public:
	typedef float FLOAT_TYPE;
	typedef uint32_t UINT_TYPE;
	typedef int32_t INT_TYPE;

	enum class Sign
	{
		Positive,
		Negative
	};

public:
	static constexpr const UINT_TYPE SignBit = 0x80000000; // MSB is sign bit
	static constexpr const UINT_TYPE ExponentBits = 0x7F800000; // 8 bits of exponent
	static constexpr const UINT_TYPE MantissaBits = 0x7FFFFF; // 23 bits of mantissa
	static constexpr const UINT_TYPE NumMantissaBits = 23U;
	static constexpr const UINT_TYPE NumExponentBits = 8U;
	static constexpr const UINT_TYPE ExponentBias = 127U;
private:
	static constexpr const UINT_TYPE U_ZERO = 0U;
	static constexpr const INT_TYPE S_ZERO = 0;
	static constexpr const UINT_TYPE U_ONE = 1U;

	static constexpr const UINT_TYPE MaxRawExponent = 255U;

	union UnionType
	{
		FLOAT_TYPE f_val;
		UINT_TYPE ui_val;
	};


public:
	FloatDissecter(FLOAT_TYPE val) : m_FValue(val) {}

	FLOAT_TYPE GetFloatPoint() const
	{
		return m_FValue;
	}

	void SetFloatPoint(FLOAT_TYPE val)
	{
		m_FValue = val;
	}

	static void Get(FLOAT_TYPE src, Sign& sign, UINT_TYPE& raw_exponent, INT_TYPE& adjusted_exponent, UINT_TYPE& mantissa)
	{
		UnionType u;
		u.f_val = src;
		sign = (u.ui_val & SignBit) ? Sign::Negative : Sign::Positive;
		raw_exponent = (u.ui_val & ExponentBits) >> NumMantissaBits;
		adjusted_exponent = (INT_TYPE)(raw_exponent) - (INT_TYPE)(ExponentBias);
		mantissa = (u.ui_val & MantissaBits);
	}

	static void Set(FLOAT_TYPE& dest, Sign sign, INT_TYPE adjusted_exponent, UINT_TYPE mantissa)
	{
		UINT_TYPE sign_value = U_ZERO;
		if (sign == Sign::Negative)
			sign_value |= SignBit;

		UINT_TYPE raw_exponent = (adjusted_exponent + ExponentBias);
		UnionType u;
		u.ui_val = sign_value | (raw_exponent << NumMantissaBits) | (mantissa & MantissaBits);

		dest = u.f_val;
	}

	Sign GetSign() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return sign;
	}

	void SetSign(Sign new_sign)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		Set(m_FValue, new_sign, adjusted_exponent, mantissa);
	}

	UINT_TYPE GetRawExponent() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return raw_exponent;
	}

	void SetRawExponent(UINT_TYPE new_raw_exponent)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);
		INT_TYPE new_adjusted_exponent = (INT_TYPE)(new_raw_exponent) -(INT_TYPE)(ExponentBias);

		Set(m_FValue, sign, new_adjusted_exponent, mantissa);
	}

	INT_TYPE GetAdjustedExponent() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return adjusted_exponent;
	}

	void SetAdjustedExponent(INT_TYPE new_adjusted_exponent)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		Set(m_FValue, sign, new_adjusted_exponent, mantissa);
	}

	UINT_TYPE GetMantissa() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return mantissa;
	}

	void SetMantissa(UINT_TYPE new_mantissa)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		Set(m_FValue, sign, adjusted_exponent, new_mantissa);
	}

	bool IsInfinity() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == MaxRawExponent) && (mantissa == U_ZERO);
	}

	void SetInfinity(Sign new_sign)
	{
		INT_TYPE adjusted_exponent = (INT_TYPE)MaxRawExponent - (INT_TYPE)ExponentBias;
		UINT_TYPE mantissa = U_ZERO;

		Set(m_FValue, new_sign, adjusted_exponent, mantissa);
	}

	bool IsPositiveInfinity() const 
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == MaxRawExponent) && (sign == Sign::Positive) && (mantissa == U_ZERO);
	}

	bool IsNegativeInfinity() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == MaxRawExponent) && (sign == Sign::Negative) && (mantissa == U_ZERO);
	}

	bool IsSubnormal() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == U_ZERO) && (mantissa > U_ZERO);
	}

	bool IsZero() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == U_ZERO) && (mantissa == U_ZERO); // zero can be positive or negative depending on the sign.
	}

	void SetZero(Sign new_sign)
	{
		INT_TYPE adjusted_exponent = -(INT_TYPE)ExponentBias;
		UINT_TYPE mantissa = U_ZERO;

		Set(m_FValue, new_sign, adjusted_exponent, mantissa);
	}

	bool IsNaN() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		return (raw_exponent == MaxRawExponent) && (mantissa > U_ZERO);
	}

	void SetNaN(Sign new_sign, UINT_TYPE non_zero_mantissa)
	{
		INT_TYPE adjusted_exponent = INT_TYPE(MaxRawExponent - ExponentBias);
		if (non_zero_mantissa == U_ZERO)
			non_zero_mantissa = U_ONE;
		UINT_TYPE mantissa = non_zero_mantissa & MantissaBits;

		Set(m_FValue, new_sign, adjusted_exponent, mantissa);
	}

	std::string Convert2Binary(UINT_TYPE value, INT_TYPE num_of_bits)
	{
		std::string str = "";
		UINT_TYPE mask = (U_ONE << num_of_bits);
		for (INT_TYPE i = 0; i < num_of_bits; ++i)
		{
			mask >>= 1;
			if (value & mask)
				str += "1";
			else
				str += "0";
		}
		return str;
	}

	static std::string GetSignString(Sign sign)
	{
		if (sign == Sign::Negative)
			return "Negative";

		return "Positive";
	}

	void Display()
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE adjusted_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;

		Get(m_FValue, sign, raw_exponent, adjusted_exponent, mantissa);

		std::cout << "Sign:" << GetSignString(sign) << ", ";
		std::cout << "Adjusted Exponent:" << adjusted_exponent << ", ";
		std::cout << "Raw Exponent:" << Convert2Binary(raw_exponent, NumExponentBits) << ", ";
		std::cout << "Mantissa:" << Convert2Binary(mantissa, NumMantissaBits) << ", ";
		std::cout << "Float Point Value:" << std::setprecision(9) << m_FValue << "\n";
	}

private:
	FLOAT_TYPE m_FValue;
};