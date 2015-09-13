#pragma once

#include <algorithm>
#include <exception>

namespace challenges {
    namespace parade {

        template<typename T = int>
        T factorial(T x) {
            static const T one = static_cast<T>(1);
            auto result = one;
            if (x < 0) throw std::exception();
            while (x) result *= std::max(x--, one);
            return result;
        }

        template<typename T = int>
        T calculate_permutation_count(T n, T r) {
            const T result = factorial<T>(n) / factorial<T>(static_cast<T>(n - r));
            return result;
        }
    }
}
