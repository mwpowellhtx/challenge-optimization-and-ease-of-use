#include "force.h"

#include <algorithm>

namespace challenges {

    namespace parade {

        int force::_count = 0;

        force::force(int range)
            : cloneable(),
            _id(_count++),
            _range(range) {
        }

        force::force(force const & other)
            : cloneable(other),
            _id(other._id),
            _range(other._range) {
        }

        force::~force() {
        }

        int force::id() const {
            return _id;
        }

        int force::range() const {
            return std::abs(_range);
        }

        bool force::has_range() const {
            return _range >= 0;
        }

        force::derived_type force::clone() const {
            return derived_type(*this);
        }
    }
}
