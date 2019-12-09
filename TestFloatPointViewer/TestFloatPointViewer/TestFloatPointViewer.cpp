#include <iostream>
#include <string>
#include "FloatDissecter.h"
#include "DoubleDissecter.h"


int main()
{
    DoubleDissecter::FLOAT_TYPE value = 0.5f;
    DoubleDissecter d(value);

    d.Display();

    d.SetSigned(DoubleDissecter::Sign::Negative);

	d.Display();

    std::cout << "Hello World!\n";
}
