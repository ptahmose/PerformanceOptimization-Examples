// Sum-Native.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "SumNaive.h"
#include "SumAvx2.h"

#include <chrono>

int main()
{
    constexpr  int repeatCount = 2;
    constexpr  uint64_t n = 4000000000;

    uint64_t sumNaive = 0;
    auto start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < repeatCount; ++i)
    {
        sumNaive = SumNaive(n);
    }

    auto end = std::chrono::high_resolution_clock::now();
    std::chrono::duration<double> elapsed_seconds = end - start;
    std::cout << "SumNaive(" << n << ") = " << sumNaive << "  - elapsed time: " << elapsed_seconds.count() << " sec" << std::endl;

    start = std::chrono::high_resolution_clock::now();
    for (int i = 0; i < repeatCount; ++i)
    {
        sumNaive = SumAvx2(n);
    }

    end = std::chrono::high_resolution_clock::now();
    elapsed_seconds = end - start;
    std::cout << "SumAvx2(" << n << ") = " << sumNaive << "  - elapsed time: " << elapsed_seconds.count() << " sec" << std::endl;
}
