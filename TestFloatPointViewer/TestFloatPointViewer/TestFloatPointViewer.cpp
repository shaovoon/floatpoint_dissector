#include <iostream>
#include <string>
#include <cmath>
#include "FloatDissector.h"
#include "DoubleDissector.h"


int main()
{
    FloatDissector::FLOAT_TYPE value = 0.0f;
    FloatDissector d(value);

	d.SetNaN(FloatDissector::Sign::Positive, 2);

	std::cout << "IsNaN:" << d.IsNaN() << "\n";
	std::cout << "IsZero:" << d.IsZero() << "\n";
	std::cout << "IsInfinity:" << d.IsInfinity() << "\n";
	std::cout << "IsPositiveInfinity:" << d.IsPositiveInfinity() << "\n";
	std::cout << "IsNegativeInfinity:" << d.IsNegativeInfinity() << "\n";
	std::cout << "IsNaN:" << std::isnan(d.GetFloatPoint()) << "\n";

    std::cout << "Done\n";
}
