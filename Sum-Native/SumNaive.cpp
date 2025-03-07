#include "SumNaive.h"
#include <cstdint>

using namespace std;

uint64_t SumNaive(std::uint64_t n)
{
    std::uint64_t sum = 0;
    for (std::uint64_t i = 0; i <= n; ++i)
    {
        sum += i;
    }

    return sum;
}