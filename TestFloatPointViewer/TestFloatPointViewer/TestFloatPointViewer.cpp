#include <iostream>
#include <string>
#include <cmath>
#include "FloatDissecter.h"
#include "DoubleDissecter.h"


int main()
{
    FloatDissecter::FLOAT_TYPE value = 0.0f;
    FloatDissecter d(-1.0/value);

    //d.Display();

    //d.SetSign(DoubleDissecter::Sign::Negative);
    //d.SetMantissa(1);

	//d.Display();

	std::cout << "IsNaN:" << d.IsNaN() << "\n";
	std::cout << "IsZero:" << d.IsZero() << "\n";
	std::cout << "IsInfinity:" << d.IsInfinity() << "\n";
	std::cout << "IsPositiveInfinity:" << d.IsPositiveInfinity() << "\n";
	std::cout << "IsNegativeInfinity:" << d.IsNegativeInfinity() << "\n";

    std::cout << "Done\n";
}
