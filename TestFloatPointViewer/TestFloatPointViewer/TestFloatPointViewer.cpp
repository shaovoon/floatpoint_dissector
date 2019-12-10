#include <iostream>
#include <string>
#include <cmath>
#include "FloatDissecter.h"
#include "DoubleDissecter.h"


int main()
{
    FloatDissecter::FLOAT_TYPE value = 0.0f;
    FloatDissecter d(value);

	d.SetNaN(FloatDissecter::Sign::Positive, 2);

	std::cout << "IsNaN:" << d.IsNaN() << "\n";
	std::cout << "IsZero:" << d.IsZero() << "\n";
	std::cout << "IsInfinity:" << d.IsInfinity() << "\n";
	std::cout << "IsPositiveInfinity:" << d.IsPositiveInfinity() << "\n";
	std::cout << "IsNegativeInfinity:" << d.IsNegativeInfinity() << "\n";
	std::cout << "IsNaN:" << std::isnan(d.GetFloatPoint()) << "\n";

    std::cout << "Done\n";
}
