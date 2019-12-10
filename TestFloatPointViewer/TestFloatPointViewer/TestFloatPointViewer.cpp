#include <iostream>
#include <string>
#include "FloatDissecter.h"
#include "DoubleDissecter.h"


int main()
{
    FloatDissecter::FLOAT_TYPE value = 0.5f;
    FloatDissecter d(value);

    //d.Display();

    //d.SetSigned(DoubleDissecter::Sign::Negative);
    d.SetMantissa(1);

	d.Display();

    std::cout << "Hello World!\n";
}
