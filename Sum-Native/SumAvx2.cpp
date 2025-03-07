#include "SumAvx2.h"
#include <immintrin.h>

using namespace std;

namespace
{
    // Sum the 64-bit integers in a 256-bit register
    int64_t sum_four_64bit(__m256i sum)
    {
        // Split the 256-bit register into two 128-bit registers
        const __m128i low = _mm256_extracti128_si256(sum, 0); // Lower 128 bits (2x 64-bit)
        const __m128i high = _mm256_extracti128_si256(sum, 1); // Upper 128 bits (2x 64-bit)

        // Add the two 128-bit parts (pairwise addition of 64-bit integers)
        const __m128i sum128 = _mm_add_epi64(low, high);

        // Extract and sum the two remaining 64-bit values
        const int64_t s0 = _mm_extract_epi64(sum128, 0); // Lower 64 bits
        const int64_t s1 = _mm_extract_epi64(sum128, 1); // Upper 64 bits

        return s0 + s1;
    }
}

std::uint64_t SumAvx2(uint64_t n)
{
    __m256i summand = _mm256_set_epi64x(4, 4, 4, 4);
    __m256i sum = _mm256_setzero_si256();

    const uint64_t n_over_four = (n + 1) / 4;
    const uint64_t n_mod_four = (n + 1) % 4;
    __m256i value = _mm256_set_epi64x(3, 2, 1, 0);
    for (std::uint64_t i = 0; i < n_over_four; ++i)
    {
        sum = _mm256_add_epi64(sum, value);
        value = _mm256_add_epi64(value, summand);
    }

    uint64_t total_sum = sum_four_64bit(sum);
    for (std::uint64_t i = n + 1 - n_mod_four; i <= n; ++i)
    {
        total_sum += i;
    }

    _mm256_zeroupper();
    return total_sum;
}