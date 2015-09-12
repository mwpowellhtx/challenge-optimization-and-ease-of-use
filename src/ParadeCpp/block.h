#pragma once

#include <common\cloneable.hpp>

#include "force.h"

namespace challenges {

    namespace parade {

        class block : public cloneable<block> {

            int _level;

            force * _pforce;

        public:

            block(int level);

            block(block const & other);

            virtual ~block();

            int level() const;

            bool is_patrolled() const;

            int patrolled_level() const;

            void patrol(force * pforce);

            virtual derived_type clone() const;
        };
    }
}
