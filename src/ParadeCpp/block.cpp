#include "block.h"

#include <common\cloneable.hpp>

namespace challenges {

    namespace parade {

        block::block(int level)
            : cloneable(),
            _level(level),
            _pforce(nullptr) {
        }

        block::block(block const & other)
            : cloneable(other),
            _level(other._level),
            _pforce(nullptr) {
        }

        block::~block() {
            _pforce = nullptr;
        }

        int block::level() const {
            return _level;
        }

        int block::patrolled_level() const {
            return is_patrolled() && _pforce->has_range() ? 0 : _level;
        }

        bool block::is_patrolled() const {
            return _pforce != nullptr;
        }
 
        void block::patrol(force * pforce) {
            _pforce = pforce;
        }

        block::derived_type block::clone() const {
            return derived_type(*this);
        }
    }
}