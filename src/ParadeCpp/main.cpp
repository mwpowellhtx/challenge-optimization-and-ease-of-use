
#include "challenge.h"

#include <iostream>
#include <sstream>

//#include <common\strutil.hpp>
//#include <cassert>

//void test_strutil_split() {
//    using challenges::split;
//
//    split splitter(split::remove_empty_entries);
//
//    std::string test = "test, this one, out";
//
//    auto splitted = splitter(test, ", ");
//
//    assert(!splitted.size());
//}

#ifdef _DEVELOP

#define CRLF "\r\n"

std::vector<std::string> get_test_cases() {
    std::vector<std::string> result;

    // yields: 8
    result.push_back("7 1" CRLF "0 2 5 5 4 0 6" CRLF "3 1");

    // yields: 0
    result.push_back("7 1" CRLF "0 3 5 5 5 1 0" CRLF "3 2");

    // yields: 2
    result.push_back("10 2" CRLF "4 5 0 2 5 6 4 0 3 5" CRLF "3 1 2 2");

    // yields: 18 however the permutations are vast
    result.push_back("15 1" CRLF "1 2 4 3 8 0 0 1 2 4 6 5 4 0 0" CRLF "6 1");

    return result;
}

#endif

int main() {

    using challenges::parade::challenge;

#ifdef _DEVELOP

    for (auto const & tc : get_test_cases()) {
        std::stringstream iss(tc);
        challenge c(&iss, &std::cout);
    }

#else

    challenge c(&std::cin, &std::cout);

#endif

    return 0;
}
