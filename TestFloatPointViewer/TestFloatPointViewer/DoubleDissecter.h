#pragma once
#include <iostream>
#include <string>
#include <cstdint>

// Dissector for 64bit float
class DoubleDissecter
{
public:
	typedef double FLOAT_TYPE;
	typedef uint64_t UINT_TYPE;
	typedef int64_t INT_TYPE;

	enum class Sign
	{
		Positive,
		Negative
	};

private:
	static constexpr UINT_TYPE SignBit = 0x8000000000000000; // MSB is sign bit
	static constexpr UINT_TYPE ExponentBits = 0x7FF0000000000000; // 11 bits of exponent
	static constexpr UINT_TYPE MantissaBits = 0xFFFFFFFFFFFFF; // 52 bits of mantissa
	static constexpr UINT_TYPE ExponentShift = 52;
	static constexpr UINT_TYPE ExponentBias = 1023;
	static constexpr UINT_TYPE U_ZERO = 0UL;
	static constexpr INT_TYPE S_ZERO = 0L;
	union UnionType
	{
		FLOAT_TYPE f_val;
		UINT_TYPE ui_val;
	};


public:
	DoubleDissecter(FLOAT_TYPE val) : m_FValue(val) {}

	FLOAT_TYPE GetFloatPoint() const
	{
		return m_FValue;
	}

	void SetFloatPoint(FLOAT_TYPE val)
	{
		m_FValue = val;
	}

	static void Get(FLOAT_TYPE src, Sign& sign, UINT_TYPE& raw_exponent, INT_TYPE& computed_exponent, UINT_TYPE& mantissa)
	{
		UnionType u;
		u.f_val = src;
		sign = (u.ui_val & SignBit) ? Sign::Negative : Sign::Positive;
		raw_exponent = (u.ui_val & ExponentBits) >> ExponentShift;
		computed_exponent = raw_exponent - ExponentBias;
		mantissa = (u.ui_val & MantissaBits);
	}

	static void Set(FLOAT_TYPE& dest, Sign sign, INT_TYPE computed_exponent, UINT_TYPE mantissa)
	{
		UINT_TYPE sign_value = U_ZERO;
		if (sign == Sign::Negative)
			sign_value |= SignBit;

		UINT_TYPE raw_exponent = (computed_exponent + ExponentBias);
		UnionType u;
		u.ui_val = sign_value | (raw_exponent << ExponentShift) | (mantissa & MantissaBits);

		dest = u.f_val;
	}

	Sign GetSigned() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		return sign;
	}

	void SetSigned(Sign new_sign)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		Set(m_FValue, new_sign, computed_exponent, mantissa);
	}

	UINT_TYPE GetRawExponent() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		return raw_exponent;
	}

	void SetRawExponent(UINT_TYPE new_raw_exponent)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);
		UINT_TYPE new_computed_exponent = new_raw_exponent - ExponentBias;

		Set(m_FValue, sign, new_computed_exponent, mantissa);
	}

	INT_TYPE GetComputedExponent() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		return computed_exponent;
	}

	void SetComputedExponent(INT_TYPE new_computed_exponent)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		Set(m_FValue, sign, new_computed_exponent, mantissa);
	}

	UINT_TYPE GetMantissa() const
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		return mantissa;
	}

	void SetMantissa(UINT_TYPE new_mantissa)
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;
		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		Set(m_FValue, sign, computed_exponent, new_mantissa);
	}

	std::string Convert2Binary(UINT_TYPE value, int num_of_bits)
	{
		std::string str = "";
		for (int i = num_of_bits; i > 0; --i)
		{
			if (value & i)
				str += "1";
			else
				str += "0";
		}
		return str;
	}

	void Display()
	{
		Sign sign = Sign::Positive;
		UINT_TYPE raw_exponent = U_ZERO;
		INT_TYPE computed_exponent = S_ZERO;
		UINT_TYPE mantissa = U_ZERO;

		Get(m_FValue, sign, raw_exponent, computed_exponent, mantissa);

		std::cout << "Sign:" << (sign == Sign::Negative) << ", ";
		std::cout << "Computed Exponent:" << computed_exponent << ", ";
		std::cout << "Mantissa:" << Convert2Binary(mantissa, ExponentShift) << ", ";
		std::cout << "Float Point Value:" << m_FValue << "\n";
	}

private:
	FLOAT_TYPE m_FValue;
};