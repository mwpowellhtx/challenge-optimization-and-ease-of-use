#pragma once

#include <string>
#include <cstdlib>

namespace challenges {

    int to_int(std::string const & s) {
        //there are better ways to parse but this will do the trick for example purposes
        return atoi(s.c_str());
    }
}
